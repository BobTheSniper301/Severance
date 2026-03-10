using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance { get; private set; }

    public GameObject playerCameraJoint;
    public Camera playerCamera;
    public GameObject playerBody;

    public PlayerMovementScript playerMovementScript;

    #region Function Calls

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
        GameManager.instance.isTimerActive = true;
    }

    #endregion

}
