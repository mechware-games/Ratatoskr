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
    public ForceMode appliedForceMode;

    [Header("Wall Running")]
    public float wallrunForce = 5f;
    public float maxWallRunTime = 2f;
    public float maxWallSpeed = 25f;

    [Header("Gliding")]
    public float glideFallOff = -1.5f;
    public float maxGlideTime;
    public float minlideTime;

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
    }
    private void Update()
    {
        DebugMode();
        //Jump();
        Glide();
        CheckForWall();
        WallRunInput(); 

    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        if (isWallRunning && rb.velocity.magnitude <= maxWallSpeed)
        {
            //rb.velocity += transform.forward * 50 * Time.deltaTime + transform.up * -0.1f;
            //rb.AddForce(transform.forward * wallrunForce * Time.deltaTime);
            rb.useGravity = false;
            rb.AddForce(orientation.forward * wallrunForce * Time.deltaTime);
            Vector3 veloc = new Vector3(baseSpeed, 0f, baseSpeed);
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

        if (Input.GetAxisRaw("Jump") > 0 && isGrounded && !isGliding)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2 * Physics.gravity.y), ForceMode.VelocityChange);
            isJumping = true;
        }
        else isJumping = false;
    }

    private void Glide()
    {
        if (!isWallRunning && rb.velocity.y < 0)
        {
            if (Input.GetAxis("Jump") > 0)
            {
                Vector3 glideVec = new Vector3(rb.velocity.x, glideFallOff, rb.velocity.z);
                rb.useGravity = false;
                rb.MovePosition(transform.position + glideVec.normalized * Time.deltaTime);
            }
        }
        //else rb.useGravity = true;
    }

    void WallRunInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isWallLeft) StartWallRun(); // fire3 = left shift
        if (Input.GetKey(KeyCode.LeftShift) && isWallRight) StartWallRun();
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
        /*if (rb.velocity.magnitude <= maxWallSpeed)
        {
            Debug.Log("in the first IF");
            rb.AddForce(orientation.forward * wallrunForce * Time.deltaTime);
            if (isWallRight)
            {
                Debug.Log("in the wall right if");
                rb.AddForce(orientation.right * wallrunForce / 5 * Time.deltaTime);
            }
            else
            {
                Debug.Log("in the wall lfet if");
                rb.AddForce(-orientation.right * wallrunForce / 5 * Time.deltaTime);
            }
        }*/
    }

    private void CheckForWall()
    {
        isWallRight = Physics.Raycast(transform.position, orientation.right, 5f, wallMask);
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, 5f, wallMask);

        if (!isWallLeft && !isWallRight) StopWallRun();
    }
}
