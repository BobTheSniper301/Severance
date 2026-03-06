using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    // for button
    public void Play()
    {
        SceneManager.LoadScene("TestScene1");
        Time.timeScale = 1;
    }

    // for button
    public void Quit()
    {
        Application.Quit();
    }
}
