using UnityEngine;

public class HeldItemScript : InteractableObjectScript
{
    [SerializeField] int handcount;
    
    public override void Interact()
    {
        ItemInventoryManager.instance.Pickup(this.gameObject);
    }
}
