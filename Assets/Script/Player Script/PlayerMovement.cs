using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.8f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private bool isCrouching = false;
    private float defaultHeight;
    private float crouchHeight = 1f;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        defaultHeight = controller.height;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGroundStatus();
        HandleMovementInput();
        HandleCrouchInput();
        ApplyGravity();
    }

    void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Ensure grounded velocity reset
        }
    }

    void HandleMovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        float currentSpeed = isCrouching ? speed / 2 : speed; // Crouch speed is half of normal speed
        controller.Move(move * currentSpeed * Time.deltaTime);
    }
    void HandleCrouchInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleCrouch();
        }
    }

    void ToggleCrouch()
    {
        isCrouching = !isCrouching;
        controller.height = isCrouching ? crouchHeight : defaultHeight;
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    public bool IsMoving()
    {
        return controller.velocity.magnitude > 0.1f;
    }
}
