using UnityEngine;

public class ItemInventoryManager : MonoBehaviour
{
    public static ItemInventoryManager instance { get; private set; }

    public GameObject[] heldObjects;
    public GameObject[] itemsInInventory = new GameObject[3];

    public GameObject activeItem;
    public GameObject visibleItem;

    public bool isholdingObject;

    public float throwHoldTime;
    public float _throwHoldTime;
    
    

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

    void Start()
    {
        heldObjects = GameObject.FindGameObjectsWithTag("HeldObject");
        foreach (GameObject heldObject in heldObjects)
        {
            heldObject.SetActive(false);
        }
    }

    void Update()
    {
        // Won't switch to different item slot if holding an object
        if (!isholdingObject) SelectItemCheck();
        
        CheckItemDrop();
        CheckItemThrow();
    }
    #endregion

    void DisableActiveItem()
    {
        if (activeItem)
        {
            visibleItem.SetActive(false);
            visibleItem = null;
            activeItem = null;
        }
    }

    void EnableActiveItem(GameObject itemToEnable)
    {
        activeItem = itemToEnable;
        foreach(GameObject heldObject in heldObjects)
        {
            if (heldObject.name == activeItem.name)
            {
                heldObject.SetActive(true);
                visibleItem = heldObject;
            }
        }
    }

    void CheckItemDrop()
    {
        if (Input.GetKeyDown("g") && activeItem)
        {
            DropItem();
        }
    }

    void CheckItemThrow()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) && activeItem)
        {
            ThrowItem();
        }
        if (Input.GetKey(KeyCode.Mouse1) && activeItem)
        {
            Debug.Log("charging throw");
            
            _throwHoldTime += Time.deltaTime;
        }
    }

    void SelectItemCheck()
    {
        for (int i = 0; i < itemsInInventory.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                if (itemsInInventory[i] == activeItem)
                {
                    DisableActiveItem();
                    return;
                }
                if (activeItem)
                {
                    DisableActiveItem();
                    activeItem = null;
                }
                EnableActiveItem(itemsInInventory[i]);
            }
        }
    }

    bool IsInventoryFull()
    {
        for (int i = 0; i < itemsInInventory.Length; i++)
        {
            if (itemsInInventory[i] == null)
            {
                return (false);
            }
        }
        return (true);
    }

    void DropItem()
    {
        Debug.Log("drop item");
        activeItem.transform.position = visibleItem.transform.position;
        activeItem.transform.rotation = visibleItem.transform.rotation;
        activeItem.SetActive(true);
        for (int i = 0; i < itemsInInventory.Length; i++)
        {
            if (itemsInInventory[i] == activeItem)
            {
                itemsInInventory[i] = null;
            }
        }
        DisableActiveItem();
        isholdingObject = false;
    }

    void ThrowItem()
    {
        Debug.Log("Throw item");
        GameObject _activeItem = activeItem;
        DropItem();


        throwHoldTime = _throwHoldTime;
        throwHoldTime = Mathf.Clamp(throwHoldTime, 1, 10);
        Debug.Log("throwHoldTime: " + throwHoldTime);

        float magnitude = 10;

        _activeItem.GetComponent<Rigidbody>().AddForce(PlayerScript.instance.playerCamera.transform.forward * magnitude * throwHoldTime, ForceMode.Impulse);
    }

    #region ItemInteractFunctions

    public void Equip(GameObject itemToEquip)
    {
        Debug.Log("equip");
        if (! IsInventoryFull())
        {
            for (int i = 0; i < itemsInInventory.Length; i++)
            {
                if (itemsInInventory[i] == null)
                {
                    itemsInInventory[i] = itemToEquip;
                    itemToEquip.SetActive(false);
                    DisableActiveItem();
                    EnableActiveItem(itemsInInventory[i]);
                    return;
                }
            }
        }
        else
        {
            Debug.Log("Your inventory is full");
        }
    }

    public void Pickup(GameObject itemToPickup)
    {
        Debug.Log("pickup");
        activeItem = itemToPickup;
        itemToPickup.SetActive(false);
        EnableActiveItem(activeItem);
        isholdingObject = true;
    }

    #endregion
}
