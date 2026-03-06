using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    public bool canMove = true;
    public bool isSprinting;
    public bool isCrouching;

    private float crouchSpeed = 2f;
    private float walkSpeed = 6f;
    private float sprintSpeed = 8f;
    private float gravity = 100000000f;

    [SerializeField] float lookSpeed = 10f;
    [SerializeField] float lookXLimit = 89f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    void FixedUpdate()
    {
        #region Handles Movement

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        isSprinting = !isCrouching ? Input.GetKey(KeyCode.LeftShift) : false;

        float curSpeedX = canMove ? (isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

    }

    private void Update()
    {
        #region Handles Rotation

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            PlayerScript.instance.playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion

        #region Handles Crouching

        if (Input.GetKeyDown("c") && canMove && !isSprinting && ! isCrouching)
        {
            isCrouching = true;
            PlayerScript.instance.gameObject.transform.localScale = new Vector3(1, 0.75f, 1);
        }
        else if (Input.GetKeyDown("c") && canMove && !isSprinting)
        {
            isCrouching = false;
            PlayerScript.instance.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        #endregion
    }

}
