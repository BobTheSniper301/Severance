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
        assassinationMask = LayerMask.GetMask("Assassination");
    }

    void Update()
    {
        // Ray shoots out from center of player view
        cameraRay = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        InteractCheck();

        AssassinationCheck();

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
        if (Physics.Raycast(cameraRay, out assassinationColliderHit, PlayerLookDistance, assassinationMask) && !MenuManager.instance.activeMenu && assassinationColliderHit.transform.parent.GetComponent<TestEnemyScript>().isVulnerable)
        {
            UiManager.instance.InteractPrompt(true);
            if (Input.GetKeyDown("f"))
            {
                if (! ItemInventoryManager.instance.activeItem || ! ItemInventoryManager.instance.activeItem?.GetComponent<WeaponScript>())
                {
                    Debug.Log("sorry you must be holding a weapon");
                }
                else if (ItemInventoryManager.instance.activeItem && ItemInventoryManager.instance.activeItem?.GetComponent<WeaponScript>())
                {
                    assassinationColliderHit.transform.parent.GetComponent<TestEnemyScript>().Die();
                }
            }
        }
    }
}
