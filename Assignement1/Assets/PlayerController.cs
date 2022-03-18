using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera = null;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private float walkSpeed = 6.0f;
    [SerializeField] private float gravity = -13.0f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private bool lockCursor = true;

    private float cameraPitch = 0.0f;
    private CharacterController controller = null;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook()
    {
        Vector2 currentMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            velocity.y = 0.0f;
        }

        velocity.y += gravity * Time.deltaTime;

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        Vector3 move = (transform.right * x) + (transform.forward * z) + (Vector3.up * velocity.y);
        controller.Move(move * Time.deltaTime * walkSpeed);

    }

}
