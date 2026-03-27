using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class UiManager : MonoBehaviour
{
    public static UiManager instance { get; private set; }

    #region Vars

    // Extra Menus
    [SerializeField] GameObject interactPrompt;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject throwPowerPrompt;
        // ThrowPowerPrompt Values
    [SerializeField] TMP_Text throwPower;
    [SerializeField] TMP_Text errorPromptText;
    [SerializeField] GameObject errorPrompt;
    [SerializeField] GameObject deathMenu;

    public float errorTimerBaseValue = 3f;
    public float errorTimer;
    bool isErrorPromptActive;

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

    void Update()
    {
        if (isErrorPromptActive)
        {
            errorTimer -= Time.deltaTime;
        }
        if (errorTimer <= 0f)
        {
            isErrorPromptActive = false;
            errorPrompt.SetActive(false);
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
        throwPower.text = "Power: " + ItemInventoryManager.instance.throwPower.ToString();
    }

    public void ErrorPrompt(string errorText)
    {
        Debug.Log("error prompt");
        errorTimer = errorTimerBaseValue;
        isErrorPromptActive = true;
        errorPrompt.SetActive(true);
        errorPromptText.text = errorText;
    }

    public void DeathRestart()
    {
        deathMenu.SetActive(false);
        AudioManager.instance.ButtonSFX();
        SceneManager.LoadScene("Floor" + SaveDataManager.instance.currentFloor.ToString());
        Time.timeScale = 1;
    }

    public void DeathMenu()
    {
        MenuManager.instance.activeMenu = null;
        deathMenu.SetActive(true);
        Time.timeScale = 0;
    }
}