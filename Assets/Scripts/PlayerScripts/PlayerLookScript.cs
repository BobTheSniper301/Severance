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
    public TestEnemyScript enemyBeingKilledScript;
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
        if (Physics.Raycast(cameraRay, out assassinationColliderHit, PlayerLookDistance, assassinationMask) && !MenuManager.instance.activeMenu && assassinationColliderHit.transform.parent.GetComponent<TestEnemyScript>().isVulnerable)
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
                    Debug.Log("kill");
                    // TODO: This needs to trigger an animation, and then set the animation to call a kill function or whatever when it ends
                    enemyBeingKilledScript = assassinationColliderHit.transform.parent.GetComponent<TestEnemyScript>();
                    enemyBeingKilledScript.StopEnemy();
                    Debug.Log("kill pos: " + enemyBeingKilledScript.killPosition.name);
                    PlayerScript.instance.StartAssassination(enemyBeingKilledScript.killPosition);
                }
            }
        }
    }
}
