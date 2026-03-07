using Unity.VisualScripting;
using UnityEngine;

public class Item_InventoryManager : MonoBehaviour
{
    public static Item_InventoryManager instance { get; private set; }

    public GameObject[] heldObjects;
    public GameObject[] itemsInInventory = new GameObject[3];

    public GameObject activeItem;

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
        SelectItemCheck();
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
        // later me stuff
    }

    #endregion
}
