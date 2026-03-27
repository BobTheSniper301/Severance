using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance { get; private set; }

    public GameObject playerCameraJoint;
    public Camera playerCamera;
    public GameObject playerBody;
    public GameObject camerJoint;
    public bool isCrouching;

    public GameObject playerArms;
    public GameObject[] assassinationWeapons;
    public GameObject activeAssassinationWeapon;
    [SerializeField] CameraBobSystem cameraBobSystem;

    
    


    public PlayerMovementScript playerMovementScript;

    [SerializeField] Animator animator;

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
        assassinationWeapons = GameObject.FindGameObjectsWithTag("AssassinationWeapon");
        foreach (GameObject weapon in assassinationWeapons)
        {
            weapon.SetActive(false);
        }

        GameManager.instance.isTimerActive = true;
    }
    #endregion

    public void Die()
    {
        Debug.Log("player die");
        UiManager.instance.DeathMenu();
        MenuManager.instance.gameObject.SetActive(false);
    }

    public void StartAssassination(GameObject killPosition, GameObject enemy)
    {
        if (playerMovementScript.isCrouching)
        {
            playerMovementScript.Uncrouch();
        }
        cameraBobSystem.enabled = false;
        this.gameObject.GetComponent<PlayerMovementScript>().enabled = false;
        this.gameObject.transform.position = killPosition.transform.position;
        camerJoint.transform.localEulerAngles = new Vector3(0,0,0);
        this.gameObject.transform.rotation = enemy.transform.rotation;
        playerArms.SetActive(true);
        for (int i = 0; i < assassinationWeapons.Length; i++)
        {
            if (assassinationWeapons[i].name == ItemInventoryManager.instance.activeItem.name)
            {
                assassinationWeapons[i].SetActive(true);
                activeAssassinationWeapon = assassinationWeapons[i];
            }
        }
        AssassinationAnimation(ItemInventoryManager.instance.activeItem.GetComponent<WeaponScript>().handcount);
        Destroy(ItemInventoryManager.instance.activeItem);
        ItemInventoryManager.instance.visibleItem.SetActive(false);
        ItemInventoryManager.instance.visibleItem = null;
        ItemInventoryManager.instance.activeItem = null;
    }

    public void StartEndAssassination(GameObject killPosition)
    {
        if (playerMovementScript.isCrouching)
        {
            playerMovementScript.Uncrouch();
        }
        cameraBobSystem.enabled = false;
        this.gameObject.GetComponent<PlayerMovementScript>().enabled = false;
        this.gameObject.transform.position = killPosition.transform.position;
        camerJoint.transform.localEulerAngles = new Vector3(0,0,0);
        this.gameObject.transform.rotation = killPosition.transform.rotation;
        playerArms.SetActive(true);
        for (int i = 0; i < assassinationWeapons.Length; i++)
        {
            if (assassinationWeapons[i].name == ItemInventoryManager.instance.activeItem.name)
            {
                assassinationWeapons[i].SetActive(true);
                activeAssassinationWeapon = assassinationWeapons[i];
            }
        }
        EndAnimation();
        Destroy(ItemInventoryManager.instance.activeItem);
        ItemInventoryManager.instance.visibleItem.SetActive(false);
        ItemInventoryManager.instance.visibleItem = null;
        ItemInventoryManager.instance.activeItem = null;
    }

    public void AssassinationAnimation(int oneOrTwo)
    {
        animator.Play(oneOrTwo.ToString() + "HandAssassination");
    }

    public void EndAnimation()
    {
        animator.Play("EndAssassination");
    }

    public void EndGame()
    {
        Debug.Log("end game");
        UiManager.instance.WinMenu();
    }

    public void FinishAssassination()
    {
        GetComponent<PlayerLookScript>().enemyBeingKilledScript.Die();
        playerArms.SetActive(false);
        activeAssassinationWeapon.SetActive(false);
        this.gameObject.GetComponent<PlayerMovementScript>().enabled = true;
        cameraBobSystem.enabled = true;
    }
}
