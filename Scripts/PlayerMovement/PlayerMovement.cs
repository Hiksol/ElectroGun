using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public AudioSource footstepsAudioSource;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float sensitivity = 500f;

    private Rigidbody rb;
    private bool isGrounded = true;
    private float horizontalRot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        footstepsAudioSource = GetComponent<AudioSource>();

        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        horizontalRot = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        transform.Rotate(0, horizontalRot, 0);
    }

    void FixedUpdate()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized;

        footstepsAudioSource.pitch = movement.magnitude;
        footstepsAudioSource.volume = movement.magnitude;

        rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
        //rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
