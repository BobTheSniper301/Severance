using UnityEngine;
using UnityEngine.SceneManagement;

// Ensure all other menus are off by default except for StartMenu
public class StartScript : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject recordsMenu;
    [SerializeField] GameObject continueButton;
    GameObject activeMenu;

    void Start()
    {
        activeMenu = startMenu;
        StartAudioManager.instance.GameOpen();
    }

    private void Update() // Soley in update for if player wipes save data
    {
        if (activeMenu == startMenu)
        {
            if (SaveDataManager.instance.isActiveRun)
            {
                continueButton.SetActive(true);
            }
            else
            {
                continueButton.SetActive(false);
            }
        }
    }

    #region For Buttons

    public void Play()
    {
        SaveDataManager.instance.isActiveRun = true;
        StartAudioManager.instance.ButtonSFX();
        SaveDataManager.instance.currentFloor = 1;
        SaveDataManager.instance.SaveAll();
        SaveDataManager.instance.LoadAll();
        SceneManager.LoadScene("Floor1");
        Time.timeScale = 1;
    }

    public void Continue()
    {
        StartAudioManager.instance.ButtonSFX();
        SceneManager.LoadScene("Floor" + SaveDataManager.instance.currentFloor.ToString());
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
        recordsMenu.GetComponent<RecordsMenuScript>().UpdateTimes();
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
