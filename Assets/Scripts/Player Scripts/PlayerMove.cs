using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float doubleJumpModifierSpeed = 2;

    private bool allowDoubleJump;

    private Vector3 moveDirection = Vector3.zero;

    private Animator playerAnimator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        if (Input.GetButton("Horizontal"))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            playerAnimator.SetBool("walking", true);
        }
        else
        {
            playerAnimator.SetBool("walking", false);
        }

        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                playerAnimator.SetBool("jumping", true);
                allowDoubleJump = true;
            }
            else
            {
                playerAnimator.SetBool("jumping", false);
            }
        }
        else if (allowDoubleJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAnimator.SetBool("jumping", false);
                Invoke("JumpAgain", 0.1f);
                moveDirection.y += jumpSpeed * doubleJumpModifierSpeed;
                allowDoubleJump = false;
            }
        }
        else
        {
            playerAnimator.SetBool("walking", false);
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void JumpAgain()
    {
        playerAnimator.SetBool("jumping", true);
    }
}
