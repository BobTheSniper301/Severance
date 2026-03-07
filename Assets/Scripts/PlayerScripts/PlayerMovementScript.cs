using UnityEngine;
using System;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    public bool canMove = true;
    public bool isSprinting;
    public bool isCrouching;

    public float crouchSpeed = 2f;
    public float walkSpeed = 4f;
    public float sprintSpeed = 6f;
    public float gravity = 100000000f;

    [SerializeField] float lookSpeed = 10f;
    [SerializeField] float lookXLimit = 82f;

    Vector3 moveDirection = Vector3.zero;
    public float currentPlayerMoveSpeed;
    float rotationX = 0;

    private Vector3 playerBodyStartScale;
    private float characterControlerStartHeight;
    private float playerBodyCapsuleStartHeight;


    void Start()
    {
        playerBodyStartScale = PlayerScript.instance.playerBody.transform.localScale;
        characterControlerStartHeight = this.GetComponent<CharacterController>().height;
        playerBodyCapsuleStartHeight = PlayerScript.instance.playerBody.GetComponent<CapsuleCollider>().height;
    }

    void FixedUpdate()
    {
        Debug.Log(moveDirection);
        #region Handles Movement

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        isSprinting = !isCrouching ? Input.GetKey(KeyCode.LeftShift) : false;

        float curSpeedX = canMove ? (isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        currentPlayerMoveSpeed = Math.Abs(moveDirection.x) + Math.Abs(moveDirection.z);

    }

    private void Update()
    {
        #region Handles Rotation

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            PlayerScript.instance.playerCameraJoint.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion

        #region Handles Crouching

        if (Input.GetKeyDown("c") && canMove && !isSprinting && ! isCrouching)
        {
            isCrouching = true;
            PlayerScript.instance.playerBody.transform.localScale = new Vector3(1, 0.75f, 1);
            PlayerScript.instance.playerBody.GetComponent<CapsuleCollider>().height *= .75f;
            this.GetComponent<CharacterController>().height *= .75f;
        }
        else if (Input.GetKeyDown("c") && canMove && !isSprinting)
        {
            isCrouching = false;
            PlayerScript.instance.playerBody.transform.localScale = playerBodyStartScale;
            PlayerScript.instance.playerBody.GetComponent<CapsuleCollider>().height = playerBodyCapsuleStartHeight;
            this.GetComponent<CharacterController>().height = characterControlerStartHeight;
        }

        #endregion
    }

}
