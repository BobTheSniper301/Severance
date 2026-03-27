using System;
using UnityEngine;
// cursor locked is in MenuManager

public class PlayerLookScript : MonoBehaviour
{
     private Ray cameraRay;
    [SerializeField] float PlayerLookDistance;

    private RaycastHit interactableHit;
    private RaycastHit assassinationColliderHit;
    private RaycastHit endingColliderHit;
    // private RaycastHit clickableHit;

    [SerializeField] new Camera camera;

    private LayerMask interactableMask;
    private LayerMask assassinationMask;
    private LayerMask endingMask;
    public AiBehaviourScript enemyBeingKilledScript;
    // private LayerMask clickableMask;

    #region Function Calls

    private void Awake()
    {
        interactableMask = LayerMask.GetMask("Interactable");
        assassinationMask = LayerMask.GetMask("Assassination");
        endingMask = LayerMask.GetMask("Ending");
    }

    void Update()
    {
        // Ray shoots out from center of player view
        cameraRay = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        InteractCheck();

        AssassinationCheck();

        if (SaveDataManager.instance.currentFloor == 6)
        {
            EndingCheck();
        }
    }

    #endregion

    void InteractCheck()
    {
        // TODO: Could clean up this logic but too lazy to do all that
        if (Physics.Raycast(cameraRay, out interactableHit, PlayerLookDistance, interactableMask) && ! MenuManager.instance.activeMenu)
        {
            UiManager.instance.InteractPrompt(true);
            if (Input.GetKeyDown("f"))
            {
                if (interactableHit.transform.GetComponent<InteractableObjectScript>())
                {
                    if (! ItemInventoryManager.instance.isholdingObject)
                    {
                        interactableHit.transform.GetComponent<InteractableObjectScript>().Invoke("Interact", 0);
                    }
                    else UiManager.instance.ErrorPrompt("Your hands are full");
                }
                else
                {
                    interactableHit.transform.parent.GetComponent<MonoBehaviour>().Invoke("Interact", 0);
                }
            }
        }
        else
        {
            UiManager.instance.InteractPrompt(false);
        }
    }

    void AssassinationCheck()
    {
        if (Physics.Raycast(cameraRay, out assassinationColliderHit, PlayerLookDistance, assassinationMask) && !MenuManager.instance.activeMenu && assassinationColliderHit.transform.parent.GetComponent<AiBehaviourScript>().isVulnerable)
        {
            UiManager.instance.InteractPrompt(true);
            if (Input.GetKeyDown("f"))
            {
                if (! ItemInventoryManager.instance.activeItem || ! ItemInventoryManager.instance.activeItem?.GetComponent<WeaponScript>())
                {
                    UiManager.instance.ErrorPrompt("You need a weapon to assassinate");
                }
                else if (ItemInventoryManager.instance.activeItem && ItemInventoryManager.instance.activeItem?.GetComponent<WeaponScript>())
                {
                    enemyBeingKilledScript = assassinationColliderHit.transform.parent.GetComponent<AiBehaviourScript>();
                    enemyBeingKilledScript.StopEnemy();
                    PlayerScript.instance.StartAssassination(enemyBeingKilledScript.killPosition, enemyBeingKilledScript.gameObject);
                }
            }
        }
    }

    void EndingCheck()
    {
        if (Physics.Raycast(cameraRay, out endingColliderHit, PlayerLookDistance, endingMask) && !MenuManager.instance.activeMenu)
        {
            UiManager.instance.endPrompt.SetActive(true);
            EndScript endScript = endingColliderHit.transform.gameObject.GetComponent<EndScript>();
            if (Input.GetKeyDown("f"))
            {
                if (! ItemInventoryManager.instance.visibleItem)
                {
                    UiManager.instance.ErrorPrompt("You need a weapon to assassinate");
                }
                else
                {
                    PlayerScript.instance.StartEndAssassination(endScript.killSpot);
                }
            }
        }
        else
        {
            UiManager.instance.endPrompt.SetActive(false);
        }
    }
}
