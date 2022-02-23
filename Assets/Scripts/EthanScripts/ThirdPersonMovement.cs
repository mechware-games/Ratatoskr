using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    public Transform cam;
    public Transform orientation;
    public Transform groundCheck;

    private Rigidbody rb;

    public float speed = 5f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public float groundDist = 0.4f;
    public float glideYSpeed = -1.5f;

    public float wallrunForce;
    public float maxWallRunTime;
    public float maxWallSpeed;

    Vector3 velocity;

    //bools
    bool isGrounded;
    bool isGliding;

    bool isWallRunning;
    bool isWallRight;
    bool isWallLeft;

    //Masks
    public LayerMask groundMask;
    public LayerMask wallMask;

    // Camera smoothing
    float turnSmoothVelocity;
    public float CameraSmooth = 0.1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Jump();
        Glide();
        CheckForWall();
        WallRunInput();
    }

    void Move()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(hor, 0, ver).normalized;

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, CameraSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetAxisRaw("Jump") > 0 && isGrounded && !isGliding)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    void Glide()
    {
        if (!isGrounded && velocity.y <= 0 && Input.GetAxisRaw("Jump") > 0)
        {
            isGliding = true;
            velocity.y = glideYSpeed;
        }
        if (!isGrounded && velocity.y < glideYSpeed)
        {
            isGliding = false;
        }
    }

    void WallRunInput()
    {
        if (Input.GetAxisRaw("Horizontal") <= -0.8 && isWallLeft) StartWallRun();
        if (Input.GetAxisRaw("Horizontal") >= 0.8 && isWallRight) StartWallRun();
    }

    void StopWallRun()
    {
        rb.useGravity = true;
        isWallRunning = false;
    }

    void StartWallRun()
    {
        rb.useGravity = false;

        isWallRunning = true;
        Debug.Log("YO WE START THE WALLRUN");

        if (rb.velocity.magnitude <= maxWallSpeed)
        {
            rb.AddForce(orientation.forward * wallrunForce * Time.deltaTime);
            if (isWallRight)
            {
                rb.AddForce(orientation.right * wallrunForce / 5 * Time.deltaTime);
            }
            else
            {
                rb.AddForce(-orientation.right * wallrunForce / 5 * Time.deltaTime);
            }
        }
    }

    private void CheckForWall()
    {
        isWallRight = Physics.Raycast(transform.position, orientation.right, 1f, wallMask);
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, wallMask);

        if (!isWallLeft && !isWallRight) StopWallRun();
    }

}
