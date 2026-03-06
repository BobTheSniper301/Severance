using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public static MenuManager instance { get; private set; }


    [HideInInspector] public GameObject activeMenu;

    [SerializeField] GameObject darkBackground;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;

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
        }
    }


    #region Functions For Buttons

    public void SettingsMenu()
    {
        if (activeMenu)
        {
            activeMenu.SetActive(false);
        }
        settingsMenu.SetActive(true);
        activeMenu = settingsMenu;
    }

    public void PauseMenu()
    {
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

    #endregion

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
            PlayerScript.instance.canMove = false;
            Cursor.lockState = CursorLockMode.None;
            darkBackground.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PlayerScript.instance.canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            darkBackground.SetActive(false);
            Time.timeScale = 1;
        }

        PauseMenuCheck();
    }
}
