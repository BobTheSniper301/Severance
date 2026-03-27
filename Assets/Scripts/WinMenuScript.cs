using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuScript : MonoBehaviour
{
    [SerializeField] TMP_Text totalTime;

    void Start()
    {
        totalTime.text = "Total: " + SaveDataManager.instance.finalTotal.ToString() + "s";
    }

    public void MainMenu()
    {
        AudioManager.instance.ButtonSFX();
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 0;
    }

    public void Quit()
    {
        SaveDataManager.instance.SaveAll();
        AudioManager.instance.ButtonSFX();
        Application.Quit();
    }
}
