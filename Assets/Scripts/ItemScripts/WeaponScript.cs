using UnityEngine;

public class WeaponScript : InteractableObjectScript
{
    [SerializeField] AudioClip hitSfx;
    [SerializeField] int damage;

    public override void Interact()
    {
        ItemInventoryManager.instance.Equip(this.gameObject);
    }
}
