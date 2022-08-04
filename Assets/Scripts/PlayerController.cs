using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private CharacterController controller = default;
    [SerializeField] private Camera playerCamera = default;
    [SerializeField] private Canvas ui = default;
    [SerializeField] private Transform spawnPoint = default;
    [SerializeField] private float speed = default;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canLook = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canFall = true;
    [SerializeField] private bool canInteract = true;
    [SerializeField] public bool isAlive = true;

    [Header("Gravity & Jumping")]
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

    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isAlive)
        {
            canLook = true;
            canMove = true;
            canInteract = true;
            canJump = true;
        }

        if (!isAlive)
        {
            HandleRespawn();
            canLook = false;
            canMove = false;
            canInteract = false;
            canJump = false;
        }

        if (view.IsMine)
        {
            if (canLook)
                HandleLook();
            
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

        if (canFall)
            HandleGravity();
    }

    private void HandleLook()
    {
        playerCamera.GetComponent<MouseLook>().HandleLook();
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
        if (isGrounded && Input.GetKeyDown("space"))
            velocity.y = jumpSpeed;
    }

    private void HandleGravity()
    {
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

    private void HandleRespawn()
    {
        if (Input.GetKeyDown("e"))
        {
            Vector3 distance = controller.gameObject.transform.position - spawnPoint.position;
            controller.Move(-distance);
            controller.gameObject.transform.rotation = spawnPoint.rotation;
            ui.GetComponent<UiController>().SetDeathScreen(false);
            isAlive = true;
        }
    }

    public void KillPlayer()
    {
        isAlive = false;
        ui.GetComponent<UiController>().SetDeathScreen(true);
    }
}
