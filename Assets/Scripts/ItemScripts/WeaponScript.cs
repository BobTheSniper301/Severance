using UnityEngine;

public class WeaponScript : InteractableObjectScript
{
    //[SerializeField] sound hit effect
    [SerializeField] int handcount;
    [SerializeField] int damage;

    public override void Interact()
    {
        Debug.Log("equip");
    }
}
