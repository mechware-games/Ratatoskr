using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBTPP : MonoBehaviour
{
    #region Settings
    [Header("Movement")]
    public float baseSpeed = 10f;
    public float maxSpeed = 25f;

    private float currentSpeed;

    [Header("Jumping")]
    public float baseJumpForce = 2f;
    public float baseForwardForce = 1.5f;
    public float groundDist = 0.4f;

    private float currentJumpForce;
    private float currentJumpForwardForce;

    private bool isJumping;

    [Header("Wall Running")]
    public float wallrunForce = 5f;
    public float maxWallRunTime = 2f;
    public float wallrunSpeedMultiplier = 25f;
    public float wallrunGravity = -1f;

    private float wallrunBaseSpeed;
    private float wallrunCounterGravity;
    private float currentWallrunGravity;

    private bool isWallRunning;
    private bool isWallRight;
    private bool isWallLeft;

    [Header("Gliding")]
    public float glideGravity = -1.5f;
    private float glideCounterGravity;

    private bool isGliding;

    [Header("Bools")]
    public bool debugMode;
    private bool isGrounded;

    [Header("Transform References")]
    public Transform cam;
    public Transform tppcam;
    public Transform MainCamTransform;
    public Transform orientation;
    public Transform groundCheck;

    [Header("Masks")]
    public LayerMask groundMask;
    public LayerMask wallMask;
    public LayerMask vaultMask;
    
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
        wallrunBaseSpeed = wallrunSpeedMultiplier * currentSpeed;

        currentSpeed = baseSpeed;
        currentJumpForce = baseJumpForce;
        currentJumpForwardForce = baseForwardForce;
    }

    private void Update()
    {
        if (CameraSwitcher.ActiveCamera != null)
        {
            cam = CameraSwitcher.ActiveCamera.transform;
        }
        DebugMode();
        Jump();
        if (isGrounded)
        {
            currentWallrunGravity = wallrunCounterGravity;
        }
        if (currentSpeed > maxSpeed && isGrounded)
        {
            //reduce current speed over time
        }

        if (cam == tppcam)
        {
            cam = MainCamTransform;
        }
    }

    private void FixedUpdate()
    {
        //Jump();
        Move();
        CheckForWall();
        WallRunInput();
        //Glide();
    }

    private void DebugMode()
    {

    }

    private void Move()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(hor, 0, ver).normalized;
        if (currentSpeed > baseSpeed)
        {
            float diff = currentSpeed - baseSpeed;
            if (diff > 1)
            {
                currentSpeed -= diff / 2 * Time.deltaTime;
            }
            else
            {
                currentSpeed = baseSpeed;
            }
        }
        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, CameraSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            rb.MovePosition(transform.position + moveDir.normalized * currentSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(currentJumpForce * -2 * Physics.gravity.y), ForceMode.Impulse);
            rb.AddForce(transform.forward * currentJumpForwardForce, ForceMode.Impulse);
            isJumping = true;
        }
        else 
        {
            isJumping = false;
        }
    }

    /* private void Glide()
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
    }*/

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
                rb.AddForce(transform.right * wallrunForce);
                rb.AddForce(Vector3.up * wallrunCounterGravity, ForceMode.Acceleration);
                if (Input.GetKey(KeyCode.Space)) WallRunJump();
            }
            else
            {
                rb.AddForce(-transform.right * wallrunForce);
                rb.AddForce(Vector3.up * wallrunCounterGravity, ForceMode.Acceleration);
                if (Input.GetKey(KeyCode.Space)) WallRunJump();
            }
        }
    }

    private void CheckForWall()
    {
        isWallRight = Physics.Raycast(transform.position, orientation.right, 1f, wallMask);
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, wallMask);

        if (!isWallLeft && !isWallRight) StopWallRun();
    }

    private void WallRunJump()
    {
        if (isWallRunning)
        {
            //Forces the player away from the wall 
            if (isWallRight)
            {
                rb.AddForce(-transform.right * (currentJumpForce), ForceMode.Impulse); 
                rb.AddForce(transform.forward * (currentJumpForwardForce), ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(transform.right * (currentJumpForce), ForceMode.Impulse);
                rb.AddForce(transform.forward * (currentJumpForwardForce), ForceMode.Impulse);
            }
        } 
    }

    private void Roll()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isWallLeft && !isWallRight && isGrounded)
        {
            //Roll
            if(currentSpeed > baseSpeed)
            {

            }
        }
    } // I don't understand what you're wanting here Chris :D
}
