using System;
using UnityEngine;
// cursor locked is in MenuManager

public class PlayerLookScript : MonoBehaviour
{
     private Ray cameraRay;
    [SerializeField] float PlayerLookDistance;

    private RaycastHit interactableHit;
    private RaycastHit assassinationColliderHit;
    // private RaycastHit clickableHit;

    [SerializeField] new Camera camera;

    private LayerMask interactableMask;
    private LayerMask assassinationMask;
    // private LayerMask clickableMask;

    #region Function Calls

    private void Awake()
    {
        interactableMask = LayerMask.GetMask("Interactable");
        interactableMask = LayerMask.GetMask("Assassination");
    }

    void Update()
    {
        // Ray shoots out from center of player view
        cameraRay = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        InteractCheck();

        // ClickCheck();

        Debug.DrawRay(cameraRay.origin, cameraRay.direction * PlayerLookDistance, Color.red, 0.5f);
    }

    #endregion

    void InteractCheck()
    {
        if (Physics.Raycast(cameraRay, out interactableHit, PlayerLookDistance, interactableMask) && ! MenuManager.instance.activeMenu)
        {
            UiManager.instance.InteractPrompt(true);
            if (Input.GetKeyDown("f") && ! ItemInventoryManager.instance.isholdingObject)
            {
                Debug.Log(interactableHit.transform.name);
                // interactableHit.transform.root.GetComponent<InteractableObjectScript>().Invoke("Interact", 0);
                interactableHit.transform.GetComponent<InteractableObjectScript>().Invoke("Interact", 0);
                
            }
            else if (Input.GetKeyDown("f") && ItemInventoryManager.instance.isholdingObject)
            {
                Debug.Log("Hands are full");
            }
        }
        else
        {
            UiManager.instance.InteractPrompt(false);
        }
    }

    void AssassinationCheck()
    {
        if (Physics.Raycast(cameraRay, out assassinationColliderHit, PlayerLookDistance, assassinationMask) && !MenuManager.instance.activeMenu && ItemInventoryManager.instance.activeItem.GetComponent<WeaponScript>())
        {
            assassinationColliderHit.transform.parent.GetComponent<TestEnemyScript>().Die();
        }
    }
}
