using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public static MenuManager instance { get; private set; }


    public GameObject activeMenu;

    [SerializeField] GameObject darkBackground;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] List<GameObject> menuOpenOrder;

    #region Function Calls
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        GameObject[] allMenus = GameObject.FindGameObjectsWithTag("Menu");
        foreach (GameObject menu in allMenus)
        {
            menu.SetActive(false);
        }

        darkBackground.SetActive(false);
    }

    void Update()
    {
        if (activeMenu)
        {
            PlayerScript.instance.playerMovementScript.canMove = false;
            Cursor.lockState = CursorLockMode.None;
            darkBackground.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PlayerScript.instance.playerMovementScript.canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            darkBackground.SetActive(false);
            Time.timeScale = 1;
        }

        PauseMenuCheck();
    }

    #endregion

// Back() will use some of these functions as well to open menus, and Back() can be called when pressing escape
    #region Functions For Buttons

    public void SettingsMenu()
    {
        menuOpenOrder.Add(activeMenu);
        activeMenu.SetActive(false);
        settingsMenu.SetActive(true);
        activeMenu = settingsMenu;
    }

    public void PauseMenu()
    {
        if (activeMenu == pauseMenu)
        {
            activeMenu.SetActive(false);
            activeMenu = null;
            return;
        }
        if (activeMenu)
        {
            activeMenu.SetActive(false);
        }
        pauseMenu.SetActive(true);
        activeMenu = pauseMenu;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 0;
    }

    public void Back()
    {
        // ^1 is the same as -1 except for some reason it doesn't like -1 so I used ^1
        GameObject lastMenu = menuOpenOrder[^1];
        Invoke(lastMenu.name, 0);
        menuOpenOrder.Remove(lastMenu);
    }

    #endregion

    public void PauseMenuCheck()
    {
        if (Input.GetKeyDown("escape") || Input.GetKeyDown("tab"))
        {
            if (activeMenu == pauseMenu)
            {
                pauseMenu.SetActive(false);
                activeMenu = null;
                return;
            }
            else if (!activeMenu)
            {
                pauseMenu.SetActive(true);
                activeMenu = pauseMenu;
            }
            else if (Input.GetKeyDown("escape")) Back();
        }
    }
}
