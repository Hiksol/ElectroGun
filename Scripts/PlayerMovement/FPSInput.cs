using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    private CharacterController _characterController;
    private float gravity = -9.81f;
    public float speed = 7.0f;
    private float reserveSpeed;
    public float jumpForce = 20.0f;
    private Vector3 velocity;
    private float oldPositionY = 1.08f;

    private bool isCrouching = false;
    private float originalHeight;
    private float crouchHeight = 0.5f;
    private float crouchSpeed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        originalHeight = _characterController.height;
        reserveSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 1.2f)
            velocity.y = 0;
        oldPositionY = transform.position.y;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDir = new Vector3(horizontalInput, gravity, verticalInput);
        movementDir = transform.TransformDirection(movementDir);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            velocity.y = 0;
            _characterController.height = crouchHeight;
            speed = crouchSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            _characterController.height = originalHeight;
            speed = reserveSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
            _characterController.Move(movementDir * ((speed + 8) * Time.deltaTime));
        else
            _characterController.Move(movementDir * (speed * Time.deltaTime));

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
        }

        if (velocity.y > 0)
        {
            velocity.y += gravity * Time.deltaTime;
            _characterController.Move(velocity * Time.deltaTime);
        }
    }

    public void applyJumpForce(float force)
    {
        velocity.y = force;
    }
}