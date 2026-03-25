using UnityEngine;
using UnityEngine.SceneManagement;

// Ensure all other menus are off by default except for StartMenu
public class StartScript : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject recordsMenu;
    GameObject activeMenu;

    void Start()
    {
        activeMenu = startMenu;
        StartAudioManager.instance.GameOpen();
    }

    #region For Buttons

    public void Play()
    {
        StartAudioManager.instance.ButtonSFX();
        SceneManager.LoadScene("TestScene1");
        Time.timeScale = 1;
    }

    public void Quit()
    {
        SaveDataManager.instance.SaveAll();
        StartAudioManager.instance.ButtonSFX();
        Application.Quit();
    }

    public void SettingsMenu()
    {
        StartAudioManager.instance.ButtonSFX();
        settingsMenu.SetActive(true);
        activeMenu.SetActive(false);
        activeMenu = settingsMenu;
    }

    public void RecordsMenu()
    {
        StartAudioManager.instance.ButtonSFX();
        recordsMenu.SetActive(true);
        activeMenu.SetActive(false);
        activeMenu = recordsMenu;
    }

    public void Back()
    {
        StartAudioManager.instance.ButtonSFX();
        activeMenu.SetActive(false);
        startMenu.SetActive(true);
        activeMenu = startMenu;
    }

    #endregion
}
