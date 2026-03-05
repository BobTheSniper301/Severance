using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UiManager : MonoBehaviour
{
    public static UiManager instance { get; private set; }

    #region Vars

    // Extra Menus
    [SerializeField] GameObject interactPrompt;
    [SerializeField] GameObject crosshair;

    #endregion

    public void InteractPrompt(bool status)
    {
        interactPrompt.SetActive(status);
        crosshair.SetActive(!status);
    }

    #region Function Calls

    void Awake()
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

    #endregion
}