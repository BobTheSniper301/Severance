using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class UiManager : MonoBehaviour
{
    public static UiManager instance { get; private set; }

    #region Vars

    // Extra Menus
    [SerializeField] GameObject interactPrompt;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject throwPowerPrompt;
        // ThrowPowerPrompt Values
    [SerializeField] TMP_Text throwHoldTime;

    #endregion

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

    public void InteractPrompt(bool status)
    {
        interactPrompt.SetActive(status);
        crosshair.SetActive(!status);
    }

    // Used to enable and update the throw power prompt
    public void ThrowPowerPrompt(bool SetActive)
    {
        throwPowerPrompt.SetActive(SetActive);
        throwHoldTime.text = "Power: " + ItemInventoryManager.instance.throwHoldTime.ToString();
    }
}