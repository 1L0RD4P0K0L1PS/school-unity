using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float turnSpeed = 10f; // Speed of turning
    public float jumpForce = 5f; // Force applied for jumping
    public float sprintMultiplier = 1.5f; // Sprint speed multiplier
    public Animator animator; // Animator for movement animations
    public Camera playerCamera; // Camera to determine mouse position
    public Rigidbody rb; // Rigidbody for physics-based jump

    private bool isGrounded = true; // Track if the character is on the ground

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        // Get input from the user (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down

        // Sprinting check
        float currentSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= sprintMultiplier;
        }

        // Create a movement vector based on input
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        // If there is no movement, stop early
        if (move.magnitude < 0.1f) return;

        // Move the character in the desired direction
        transform.Translate(move * currentSpeed * Time.deltaTime, Space.World);

        // Reset all animations before applying new movement
        animator.SetBool("WalkForwards", false);
        animator.SetBool("WalkBackwards", false);
        animator.SetBool("WalkLeft", false);
        animator.SetBool("WalkRight", false);
        animator.SetBool("Sprint", false);

        // Apply movement animations based on input
        if (moveZ > 0.1f) // Walk Forward
        {
            animator.SetBool("WalkForwards", true);
            if (Input.GetKey(KeyCode.LeftShift)) animator.SetBool("Sprint", true);
        }
        else if (moveZ < -0.1f) // Walk Backward
        {
            animator.SetBool("WalkBackwards", true);
        }
        else if (moveX < -0.1f) // Walk Left
        {
            animator.SetBool("WalkLeft", true);
        }
        else if (moveX > 0.1f) // Walk Right
        {
            animator.SetBool("WalkRight", true);
        }
    }

    void HandleJump()
    {
        // Jump only when grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetTrigger("Jump");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the character lands on the ground
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
