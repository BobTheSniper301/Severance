using UnityEngine;

public class HeldItemScript : InteractableObjectScript
{
    [SerializeField] int handcount;
    
    public override void Interact()
    {
        Debug.Log("pickup");
    }
}
