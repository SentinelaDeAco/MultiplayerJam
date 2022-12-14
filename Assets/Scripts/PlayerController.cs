using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private CharacterController controller = default;
    //[SerializeField] private Transform spawnPoint = default;
    [SerializeField] private float speed = default;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canFall = true;
    [SerializeField] private bool isAlive = true;
    [SerializeField] private float rotationSpeed;

    [Header("Gravity & Jumping")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float gravity = default;
    [SerializeField] private float jumpSpeed = default;
    [SerializeField] private float groundDistance = default;
    [SerializeField] private Transform groundCheck = default;
    [SerializeField] private LayerMask groundMask = default;

    private Vector3 velocity;
    private PhotonView view;

    public static Action<PlayerController> OnLocalPlayerCreated;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        
        if (view.IsMine)
            OnLocalPlayerCreated?.Invoke(this);

        Actions.OnPlayerJoin(this);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Rspwn1();
        Rspwn2();

        if (isAlive)
        {
            canMove = true;
            canJump = true;
        }

        if (!isAlive)
        {
            canMove = false;
            canJump = false;
            CheckForRespawnInput();
        }

        if (view.IsMine)
        {
            if (canMove)
                HandleMovement();

            if (canJump)
                HandleJumping();
        }

        if (canFall)
            HandleGravity();

        if (transform.position.y < -20.0f)
            Actions.OnPlayerFalling(this);
    }

    private void OnDestroy()
    {
        Actions.OnPlayerLeave(this);
    }

    private void HandleMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        transform.Rotate(Vector3.up * mouseX);
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

    public void CheckForRespawnInput()
    {
        if (Input.GetKeyDown("x"))
            Actions.OnPlayerRespawn();
    }

    public void Rspwn1()
    {
        if (Input.GetKeyDown("n"))
            Actions.Rspwn1(this);
    }

    public void Rspwn2()
    {
        if (Input.GetKeyDown("m"))
            Actions.Rspwn2(this);
    }

    public void RespawnPlayer(Transform spawnPoint)
    {
        Vector3 distance = controller.gameObject.transform.position - spawnPoint.position;
        controller.Move(-distance);
        controller.gameObject.transform.rotation = spawnPoint.rotation;
        isAlive = true;
    }

    public void KillPlayer()
    {
        isAlive = false;
    }

    public bool CheckStatus()
    {
        return isAlive;
    }

    public int RetrieveId()
    {
        return view.ViewID;
    }

    public void MovePlayer(Vector3 position)
    {
        controller.transform.position = position;
    }
}
