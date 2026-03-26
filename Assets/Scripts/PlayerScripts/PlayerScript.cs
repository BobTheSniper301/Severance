using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance { get; private set; }

    public GameObject playerCameraJoint;
    public Camera playerCamera;
    public GameObject playerBody;

    public GameObject playerArms;
    public GameObject[] assassinationWeapons;
    public GameObject activeAssassinationWeapon;
    int activeAnimNum;
    public bool isLockPlayer;
    public Vector3 lockPlayerPosition;
    public Quaternion lockPlayerRotation;


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

    private void Update()
    {
        if (isLockPlayer)
        {
            this.gameObject.GetComponent<PlayerMovementScript>().enabled = false;
            this.gameObject.transform.position = lockPlayerPosition;
            this.gameObject.transform.rotation = lockPlayerRotation;
        }
    }
    #endregion

    public void StartAssassination(GameObject killPosition)
    {
        isLockPlayer = true;
        this.gameObject.transform.position = killPosition.transform.position;
        this.gameObject.transform.rotation = killPosition.transform.rotation;
        lockPlayerRotation = killPosition.transform.rotation;
        lockPlayerPosition = killPosition.transform.position;
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

    public void AssassinationAnimation(int oneOrTwo)
    {
        animator.SetTrigger("isAttacking" + oneOrTwo.ToString());
        activeAnimNum = oneOrTwo;   
    }

    public void FinishAssassination()
    {
        animator.ResetTrigger("isAttacking" + activeAnimNum.ToString());
        GetComponent<PlayerLookScript>().enemyBeingKilledScript.Die();
        playerArms.SetActive(false);
        activeAssassinationWeapon.SetActive(false);
        isLockPlayer = false;
        this.gameObject.GetComponent<PlayerMovementScript>().enabled = true;
    }
}
