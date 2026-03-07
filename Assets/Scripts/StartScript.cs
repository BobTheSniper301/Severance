using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject startMenu;
    GameObject activeMenu;

    void Start()
    {
        activeMenu = startMenu;
    }

    #region For Buttons

    public void Play()
    {
        SceneManager.LoadScene("TestScene1");
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
        activeMenu.SetActive(false);
        activeMenu = settingsMenu;
    }

    public void RecordsMenu()
    {
        Debug.Log("Sorry, but this menu is not currently available");
    }

    public void Back()
    {
        activeMenu.SetActive(false);
        startMenu.SetActive(true);
        activeMenu = startMenu;
    }

    #endregion
}
