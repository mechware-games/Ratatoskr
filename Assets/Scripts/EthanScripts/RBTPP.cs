using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBTPP : MonoBehaviour
{
    #region Settings
    [Header("Movement")]
    public float baseSpeed = 10f;
    public float maxSpeed = 25f;

    [Header("Jumping")]
    public float jumpForce = 2f;
    public float groundDist = 0.4f;

    [Header("Wall Running")]
    public float wallrunForce = 5f;
    public float maxWallRunTime = 2f;
    public float wallrunSpeedMultiplier = 25f;
    public float wallrunGravity = -1f;
    private float wallrunBaseSpeed;
    private float wallrunCounterGravity;
    private float currentWallrunGravity;

    [Header("Gliding")]
    public float glideGravity = -1.5f;
    private float glideCounterGravity;

    [Header("Bools")]
    public bool debugMode;
    private bool isJumping;
    private bool isGrounded;
    private bool isGliding;
    private bool isWallRunning;
    private bool isWallRight;
    private bool isWallLeft;

    [Header("Transform Refs")]
    public Transform cam;
    public Transform orientation;
    public Transform groundCheck;

    [Header("Masks")]
    public LayerMask groundMask;
    public LayerMask wallMask;
    
    [Header("Misc")]
    private Rigidbody rb;
    float turnSmoothVelocity;
    public float CameraSmooth = 0.1f;
    #endregion

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        wallrunCounterGravity = (-Physics.gravity.y + wallrunGravity);
        glideCounterGravity = (-Physics.gravity.y + glideGravity);
        wallrunBaseSpeed = wallrunSpeedMultiplier * baseSpeed;
    }

    private void Update()
    {
        DebugMode();
        if (isGrounded)
        {
            currentWallrunGravity = wallrunCounterGravity;
        }
    }

    private void FixedUpdate()
    {
        Jump();
        Move();
        CheckForWall();
        WallRunInput();
        Glide();
    }

    private void DebugMode()
    {

    }

    private void Move()
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

            rb.MovePosition(transform.position + moveDir.normalized * baseSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2 * Physics.gravity.y), ForceMode.Impulse);
            isJumping = true;
        }
        else isJumping = false;
    }

    private void Glide()
    {
        if (!isGrounded && !isWallRunning && Input.GetKey(KeyCode.Space))
        {
            isGliding = true;
            rb.AddForce(Vector3.up * glideCounterGravity, ForceMode.Acceleration);
        }
        else
        {
            isGliding = false;
        }
    }

    void WallRunInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isWallLeft) StartWallRun(); // fire3 = left shift
        if (Input.GetKey(KeyCode.LeftShift) && isWallRight) StartWallRun();
    }

    void StopWallRun()
    {
        isWallRunning = false;
    }

    void StartWallRun()
    {
        isWallRunning = true;

        if(isWallRunning)
        {
            rb.AddForce(transform.forward * wallrunForce, ForceMode.Acceleration);

            if (isWallRight)
            {
                rb.AddForce(-transform.right * wallrunForce);
                rb.AddForce(Vector3.up * wallrunCounterGravity, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(transform.right * wallrunForce);
                rb.AddForce(Vector3.up * wallrunCounterGravity, ForceMode.Acceleration);
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
