using UnityEngine;

public class KeycardReaderScript : MonoBehaviour
{
    [SerializeField] ElevatorScript elevatorScript;

    public void Interact()
    {
        if (ItemInventoryManager.instance.activeItem && ItemInventoryManager.instance.activeItem?.GetComponent<UtilityItemScript>())
        { // hate having to nest it like this but oh well
            if (ItemInventoryManager.instance.activeItem.GetComponent<UtilityItemScript>().isKeycard) elevatorScript.Open();
        }
        else
        {
            UiManager.instance.ErrorPrompt("Keycard Required");
            Debug.Log("you must have a keycard to open this door");
        }
    }
}
