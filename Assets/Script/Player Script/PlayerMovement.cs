using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Deklarasi Variabel
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

    // Metode Unity Standar
    void Start()
    {
        InitializeVariables();
    }

    void Update()
    {
        CheckGroundStatus();
        HandleMovementInput();
        HandleCrouchInput();
    }

    // Inisialisasi Variabel
    void InitializeVariables()
    {
        defaultHeight = controller.height;
    }

    // Logika untuk Mengecek Status Ground
    void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset the fall speed when grounded
        }
    }

    // Logika Gerakan
    void HandleMovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        float currentSpeed = isCrouching ? speed / 1.5f : speed; // Adjust crouch speed reduction factor
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime; // Apply gravity when not grounded
        }

        controller.Move(velocity * Time.deltaTime);
    }

    // Logika untuk Crouch
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

    // Metode Tambahan
    public bool IsMoving()
    {
        return controller.velocity.magnitude > 0.1f;
    }
}
