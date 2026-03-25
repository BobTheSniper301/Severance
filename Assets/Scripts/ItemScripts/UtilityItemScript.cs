using UnityEngine;

public class UtilityItemScript : InteractableObjectScript
{
    // TODO: change this to an enum when adding other utility items or change the system
    public bool isKeycard;

    public override void Interact()
    {
        ItemInventoryManager.instance.Equip(this.gameObject);
    }
    void Start()
    {
        isKeycard = true;
    }
}