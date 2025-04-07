using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private Transform playerCamera;
    private Rigidbody rb;

    float xRotation = 0f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        MovePlayer();
        RotateCamera();
    }

    void MovePlayer()
    {
        isGrounded = controller.isGrounded;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        if (Time.timeScale == 0f)
        {
            transform.Rotate(Vector3.up * mouseX);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, 0f, 0f);

            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        else if (Time.timeScale == 1f)
        {
            transform.Rotate(Vector3.up * mouseX);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -60f, 60f);

            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}
