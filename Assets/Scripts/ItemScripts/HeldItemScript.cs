using UnityEngine;

public class HeldItemScript : InteractableObjectScript
{
    public override void Interact()
    {
        ItemInventoryManager.instance.Pickup(this.gameObject);
    }
}
