using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public static MenuManager instance { get; private set; }


    [SerializeField] GameObject darkBackground;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject floorCompletionMenu;

    public GameObject activeMenu;
    [SerializeField] List<GameObject> menuOpenOrder;

    // For FloorCompletionMenu
    [SerializeField] TMP_Text pbTotalTime;
    [SerializeField] TMP_Text pbFloorTime;
    [SerializeField] TMP_Text currentTotalTime;
    [SerializeField] TMP_Text currentFloorTime;

    // Settings menu
    [SerializeField] TMP_Text masterVolumeText;
    [SerializeField] TMP_Text musicVolumeText;
    [SerializeField] TMP_Text sfxVolumeText;

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
    #region Functions Mostly For Buttons

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

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 0;
    }

    public void NextFloor()
    {
        Debug.Log("next floor button pressed");
        GameManager.instance.NextFloor();
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void Back()
    {
        // ^1 is the same as -1 except for some reason it doesn't like -1 so I used ^1
        GameObject lastMenu = menuOpenOrder[^1];
        Invoke(lastMenu.name, 0);
        menuOpenOrder.Remove(lastMenu);
    }

    #endregion

    public void FloorCompletionMenu()
    {
        if (activeMenu) activeMenu.SetActive(false);
       
        activeMenu = floorCompletionMenu;
        floorCompletionMenu.SetActive(true);
       
        // Debug.Log(GameManager.instance.floorTime);
        currentFloorTime.text = "Total: " + GameManager.instance.floorTime.ToString() + "s";
        currentTotalTime.text = "Floor: " + GameManager.instance.totalTime.ToString() + "s";
    }

    public void UpdateSettingsMenu()
    {
        masterVolumeText.text = Math.Round(AudioManager.instance.masterVolumeSlider.value * 100).ToString();
        musicVolumeText.text = Math.Round(AudioManager.instance.musicVolumeSlider.value * 100).ToString();
        sfxVolumeText.text = Math.Round(AudioManager.instance.sfxVolumeSlider.value * 100).ToString();
    }


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
