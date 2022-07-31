using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private CharacterController controller = default;
    [SerializeField] private Camera playerCamera = default;
    [SerializeField] private float speed = default;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canInteract = true;

    [Header("Jump")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float gravity = default;
    [SerializeField] private float jumpSpeed = default;
    [SerializeField] private float groundDistance = default;
    [SerializeField] private Transform groundCheck = default;
    [SerializeField] private LayerMask groundMask = default;
    private Vector3 velocity;

    [Header("Interaction")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionMask = default;
    private Interactable currentInteractable;

    void Update()
    {
        if (canMove) 
            HandleMovement();

        if (canJump) 
            HandleJumping();

        if (canInteract)
        {
            HandleInteractCheck();
            HandleInteractInput();
        }
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    private void HandleJumping()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && Input.GetKeyDown("space"))
            velocity.y = jumpSpeed;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleInteractCheck()
    {
        if (Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), 
            out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.gameObject.layer == 6 && (currentInteractable == null || 
                hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID()))
            {
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                    currentInteractable.OnFocus();
            }
        }
        else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }

    private void HandleInteractInput()
    {
        if (Input.GetKeyDown("e") && currentInteractable != null && Physics.Raycast(
            playerCamera.ViewportPointToRay(interactionRayPoint), 
            out RaycastHit hit, interactionDistance, interactionMask))
        {
            currentInteractable.OnInteract();
        }
    }
}
