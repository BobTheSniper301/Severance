using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance { get; private set; }

    public GameObject playerCameraJoint;
    public Camera playerCamera;

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

    #endregion

}
