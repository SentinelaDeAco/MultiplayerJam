using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private static MouseLook instance;
    [SerializeField] private Camera playerCamera;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    public Transform playerBody;
    private PlayerController playerController;

    private void OnEnable()
    {
        Actions.OnPlayerRespawn += ResetCamera;
    }

    private void OnDisable()
    {
        Actions.OnPlayerRespawn -= ResetCamera;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;

        PlayerController.OnLocalPlayerCreated += SetupCamera;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (playerController == null)
            return;

        Vector3 playerPosition = playerController.transform.position;
        transform.position = playerPosition + Vector3.up * 0.8f;
        
        if (playerController.CheckStatus())
            HandleLook();
    }

    public void HandleLook()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        Vector3 playerEulerRotation = playerController.transform.rotation.eulerAngles;

        transform.localRotation = Quaternion.Euler(xRotation, playerEulerRotation.y, 0f);
    }

    public void SetupCamera(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public static bool IsLookingToObject(Interactable currentInteractable, float interactionDistance)
    {
        if (Physics.Raycast(instance.playerCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f)),
            out RaycastHit hit, interactionDistance))
        {
            return hit.collider.gameObject.GetInstanceID() == currentInteractable.gameObject.GetInstanceID();
        }
        return false;
    }

    public void ResetCamera()
    {
        /*this.transform.rotation = Quaternion.identity;
        this.transform.localRotation = Quaternion.identity;
        playerCamera.transform.rotation = Quaternion.identity;
        playerCamera.transform.localRotation = Quaternion.identity;*/
    }
}
