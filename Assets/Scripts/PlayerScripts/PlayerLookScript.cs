using System;
using UnityEngine;
// cursor locked is in MenuManager

public class PlayerLookScript : MonoBehaviour
{
     private Ray cameraRay;
    [SerializeField] float PlayerLookDistance;

    private RaycastHit interactableHit;
    // private RaycastHit clickableHit;

    [SerializeField] new Camera camera;

    private LayerMask interactableMask;
    // private LayerMask clickableMask;

    #region Function Calls

    private void Awake()
    {
        interactableMask = LayerMask.GetMask("Interactable");
        // clickableMask = LayerMask.GetMask("Clickable");
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
            if (Input.GetKeyDown("f"))
            {
                Debug.Log(interactableHit.transform.name);
                // interactableHit.transform.root.GetComponent<InteractableObjectScript>().Invoke("Interact", 0);
                interactableHit.transform.GetComponent<InteractableObjectScript>().Invoke("Interact", 0);
                
            }
        }
        else
        {
            UiManager.instance.InteractPrompt(false);
        }
    }

    // To click visible buttons
    // void ClickCheck()
    // {
    //     if (Physics.Raycast(cameraRay, out clickableHit,PlayerLookDistance, clickableMask) && ! MenuManager.instance.activeMenu)
    //     {
    //         if (Input.GetKeyDown(KeyCode.Mouse0))
    //         {
    //             clickableHit.transform.GetComponent<Button>().onClick.Invoke();
    //         }
    //     }
    // }
}
