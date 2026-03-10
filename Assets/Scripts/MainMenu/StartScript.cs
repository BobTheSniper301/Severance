using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject recordsMenu;
    GameObject activeMenu;

    void Start()
    {
        activeMenu = startMenu;
        StartAudioManager.instance.Sound(Resources.Load<AudioClip>("Sounds/Track_1_piano"), true, true, StartAudioManager.SoundType.MUSIC);
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
        recordsMenu.SetActive(true);
        activeMenu.SetActive(false);
        activeMenu = recordsMenu;
    }

    public void Back()
    {
        activeMenu.SetActive(false);
        startMenu.SetActive(true);
        activeMenu = startMenu;
    }

    #endregion
}
