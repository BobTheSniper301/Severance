using UnityEngine;

public class UtilityItemScript : InteractableObjectScript
{
    public override void Interact()
    {
        ItemInventoryManager.instance.Equip(this.gameObject);
    }
}