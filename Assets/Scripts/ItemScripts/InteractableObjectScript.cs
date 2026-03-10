using UnityEngine;

public abstract class InteractableObjectScript : MonoBehaviour
{
    [SerializeField] float weight;
    //[SerializeField] sound effect

    public abstract void Interact();

}
