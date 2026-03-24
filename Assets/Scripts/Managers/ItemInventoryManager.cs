using System;
using UnityEngine;

public class ItemInventoryManager : MonoBehaviour
{
    public static ItemInventoryManager instance { get; private set; }

    public GameObject[] heldObjects;
    public GameObject[] itemsInInventory = new GameObject[3];

    public GameObject activeItem;
    public GameObject visibleItem;

    public bool isholdingObject;

    public float throwPower;
    float _throwPower;
    public float baseThrowPower = 3;
    public float throwRateMultiplier = 2;
    
    

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
        if (! MenuManager.instance.activeMenu)
        {
            CheckItemDrop();
            CheckItemThrow();

            // Won't switch to different item slot if holding an object
            if (!isholdingObject) SelectItemCheck();
        }
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
        if (Input.GetKeyUp(KeyCode.Mouse1) && activeItem )
        {
            ThrowItem();
        }
        if (Input.GetKey(KeyCode.Mouse1) && activeItem)
        {
            ChargeThrow();
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
        activeItem.transform.position = visibleItem.transform.position;
        activeItem.transform.rotation = visibleItem.transform.rotation;
        activeItem.GetComponent<InteractableObjectScript>().isThrownOrDropped = true;
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

    void ChargeThrow()
    {
        _throwPower += Time.deltaTime * throwRateMultiplier;

        throwPower = (float)Math.Round(_throwPower, 1);
        throwPower = Mathf.Clamp(throwPower, 0.1f, 10);
        UiManager.instance.ThrowPowerPrompt(true);
    }

    void ThrowItem()
    {
        GameObject _activeItem = activeItem;
        activeItem.GetComponent<InteractableObjectScript>().itemThrownPower = throwPower;
        activeItem.GetComponent<InteractableObjectScript>().isThrownOrDropped = true;
        DropItem();
        
        UiManager.instance.ThrowPowerPrompt(false);

        _activeItem.GetComponent<Rigidbody>().AddForce(PlayerScript.instance.playerCamera.transform.forward * baseThrowPower * throwPower, ForceMode.Impulse);

        _throwPower = 0;
    }

    #region ItemInteractFunctions

    public void Equip(GameObject itemToEquip)
    {
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
        activeItem = itemToPickup;
        itemToPickup.SetActive(false);
        EnableActiveItem(activeItem);
        isholdingObject = true;
    }

    #endregion
}
