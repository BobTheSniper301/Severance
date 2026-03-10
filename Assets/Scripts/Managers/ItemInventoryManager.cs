using Unity.VisualScripting;
using UnityEngine;

public class ItemInventoryManager : MonoBehaviour
{
    public static ItemInventoryManager instance { get; private set; }

    public GameObject[] heldObjects;
    public GameObject[] itemsInInventory = new GameObject[3];

    public GameObject activeItem;
    public bool isholdingObject;
    

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
    }
    #endregion

    void DisableActiveItem()
    {
        if (activeItem)
        {
            foreach(GameObject heldObject in heldObjects)
            {
                if (heldObject.name == activeItem.name)
                {
                    heldObject.SetActive(false);
                }
            }
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
            }
        }
    }

    void CheckItemDrop()
    {
        if (Input.GetKeyDown("g"))
        {
            DropItem();
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
                    EnableActiveItem(itemsInInventory[i]);
                }
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
