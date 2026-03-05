using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance { get; private set; }


    [HideInInspector] public GameObject activeMenu;

    [SerializeField] GameObject darkBackground;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;

    // For button
    public void SettingsMenu()
    {
        if (activeMenu)
        {
            activeMenu.SetActive(false);
        }
        settingsMenu.SetActive(true);
        activeMenu = settingsMenu;
    }

    // For button
    public void PauseMenu()
    {
        if (activeMenu)
        {
            activeMenu.SetActive(false);
        }
        pauseMenu.SetActive(true);
        activeMenu = pauseMenu;
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
            pauseMenu.SetActive(true);
            activeMenu = pauseMenu;
        }
    }


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
