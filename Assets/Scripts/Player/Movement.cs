using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    enum State
    {
        Grounded,
        NotGrounded,
        Wallrunning,
        Rolling
    }
    private State _playerState;

    #region Settings
    [Header("Movement")]
    public float baseSpeed = 100f;
    public float maxSpeed = 250f;

    private float currentSpeed;

    [Tooltip("Determines the length of time needed to pass between chainable movement.")]
    // Also prevents a double jump bug
    public float _ActionTimerLength = 0.3f;
    private float _ActionTimer = 0;

    [Header("Jumping")]
    public float baseJumpForce = 2f;
    public float baseForwardForce = 1.5f;
    public float groundDist = 0.2f;

    private float currentJumpForce;
    private float currentJumpForwardForce;

    private bool isJumping;

    [Header("Wallrunning")]
    public float _MaxWallrunForce = 100f;

    public float _CurrentWallrunForce = 0f;

    [Range(1, 100f)]
    public float _wallRunDecay = 100;
    [Range(1, 100f)]
    public float _wallRunRegen = 60;

    public float _minWallrunSpeed = 10;

    [SerializeField]
    [Range(0.1f, 3f)]
    private float _wallHorizontalActivationDistance = 0.3f;

    [SerializeField]
    [Range(0.1f, 3f)]
    private float _wallRunForwardActivationDistance = 0.3f;


    [Header("Bools")]
    public bool _debugMode;

    [Header("Transform References")]
    public Transform cam;
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentSpeed = baseSpeed;
        currentJumpForce = baseJumpForce;
        currentJumpForwardForce = baseForwardForce;
    }

    private void Update()
    {
        if (_debugMode)
		{
            DebugMode();
		}

        if (_playerState != State.Wallrunning && _CurrentWallrunForce <= _MaxWallrunForce)
		{
            _CurrentWallrunForce += (Time.deltaTime * _wallRunRegen);
		}

        // Input and State Handling
        switch (_playerState)
        {
            case State.Grounded:
                { 
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Jump();
                        _playerState = State.NotGrounded;
                    }
                }
                break;
            case State.NotGrounded:
				{
                    _ActionTimer += Time.deltaTime;
                    if (_ActionTimer > _ActionTimerLength)
                    {
                        if (Physics.CheckSphere(groundCheck.position, groundDist, groundMask))
                        {
                            _playerState = State.Grounded;
                        }

                        if (WallCheck() && (rb.velocity.x < _minWallrunSpeed || rb.velocity.x > -_minWallrunSpeed))
						{
                            _playerState = State.Wallrunning;
                        }
                        // Resets the action timer if it goes over the length of the timer
                        _ActionTimer = 0;
                    }
                }
                break;
            case State.Wallrunning:
				{
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Jump();
                        _playerState = State.NotGrounded;
                    }
                    if (Physics.CheckSphere(groundCheck.position, groundDist, groundMask))
                    {
                        _playerState = State.Grounded;
                    }

                    if(_CurrentWallrunForce >= 0)
					{
                        _CurrentWallrunForce -= (Time.deltaTime * _wallRunDecay);
					}
                }
                break;
            case State.Rolling:
                break;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void DebugMode()
    {
        Debug.DrawLine(transform.position, transform.position + transform.right * _wallHorizontalActivationDistance, Color.red);
        Debug.DrawLine(transform.position, transform.position + -transform.right * _wallHorizontalActivationDistance, Color.red);
        Debug.DrawLine(transform.position, transform.position + transform.forward * _wallRunForwardActivationDistance, Color.red);
    }

    private void Move()
    {
        switch (_playerState)
        {
            case State.Wallrunning:
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

                        Vector3 wallMoveDir = Quaternion.Euler(-90f, targetAngle, 0f) * Vector3.forward;

                        rb.AddForce(moveDir.normalized * currentSpeed, ForceMode.Acceleration);
                        rb.AddForce(wallMoveDir.normalized * _CurrentWallrunForce, ForceMode.Acceleration);
                    }

                    if (!WallCheck())
                    {
                        _playerState = State.NotGrounded;
                    }
                }
                break;
            default:
                {
                    // TODO: Add code to make the character not able to add input in the direction of a wall if they are touching a wall

                    float hor = Input.GetAxisRaw("Horizontal");
                    float ver = Input.GetAxisRaw("Vertical");
                    Vector3 dir = new Vector3(hor, 0, ver).normalized;
                    if (dir.magnitude >= 0.1f)
                    {
                        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, CameraSmooth);
                        transform.rotation = Quaternion.Euler(0f, angle, 0f);
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                        rb.AddForce(moveDir.normalized * currentSpeed, ForceMode.Acceleration);
                    }
                }
                break;
        }
    }

    private void Jump()
    {
        switch (_playerState)
		{
            case State.Wallrunning:
                // Wallrun Jump
                bool isWallRight = Physics.Raycast(transform.position, transform.right, 1f, wallMask);
                rb.AddForce(Vector3.up * Mathf.Sqrt(currentJumpForce * -2 * Physics.gravity.y), ForceMode.Impulse);
                if (isWallRight)
                {
                    rb.AddForce(-transform.right * currentJumpForwardForce, ForceMode.Impulse);
                }
				else
				{
                    rb.AddForce(transform.right * currentJumpForwardForce, ForceMode.Impulse);
                }

                break;
            default:
                // Standard Jump
                rb.AddForce(Vector3.up * Mathf.Sqrt(currentJumpForce * -2 * Physics.gravity.y), ForceMode.Impulse);
                rb.AddForce(transform.forward * currentJumpForwardForce, ForceMode.Impulse);
                break;
		}
    }
    private bool WallCheck()
    {
        bool isWallRight = Physics.Raycast(transform.position, transform.right, _wallHorizontalActivationDistance, wallMask);
        bool isWallLeft = Physics.Raycast(transform.position, transform.right, _wallHorizontalActivationDistance, wallMask);
        bool isWallFront = Physics.Raycast(transform.position, transform.forward, _wallRunForwardActivationDistance, wallMask);

        return isWallRight || isWallLeft || isWallFront;
    }
    /*private (RaycastHit, RaycastHit, RaycastHit) GetWallRunChecks()
	{
        bool isWallRight = Physics.Raycast(transform.position, transform.right, _wallHorizontalActivationDistance, wallMask);
        RaycastHit rightwall = RaycastHit.transform;
        var isWallLeft = Physics.Raycast(transform.position, transform.right, _wallHorizontalActivationDistance, wallMask);
        var isWallFront = Physics.Raycast(transform.position, transform.forward, _wallRunForwardActivationDistance, wallMask);

        return GetWallRunChecks;
    }

    

    private Vector3 GetWallRunAngle()
	{
        Vector3 angle = new Vector3(0, 0, 0);

        if (Physics.Raycast(transform.position, transform.right, _wallHorizontalActivationDistance, wallMask))
		{

		}
 

}   */
}
