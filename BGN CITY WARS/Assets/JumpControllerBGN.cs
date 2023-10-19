using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpControllerBGN : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection;
    public bool isJumping = false;
    public  float jumpForce = 10.0f;
    public float gravity = 20.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is grounded
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            if (Input.GetButtonDown("Jump"))
            {
                // Set the jump flag to true
                isJumping = true;
            }
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Check if the player is jumping
        if (isJumping)
        {
            moveDirection.y = jumpForce;
            isJumping = false;
        }

        // Move the character controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
