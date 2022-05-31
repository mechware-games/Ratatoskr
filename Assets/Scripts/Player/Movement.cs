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
    [SerializeField]
    private State _playerState;

    public bool isWallRunning = false;
    public bool publicWallRight;
    public bool publicWallLeft;


    #region Settings
    [Header("Movement")]
    public float baseSpeed = 100f;
    public float maxSpeed = 250f;

    [SerializeField]
    [Range(0.1f, 1f)]
    private float _speedDecayCoefficient = 0.3f;

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
    [Range(1, 50f)]
    public float _wallKickOffForce = 20;

    [SerializeField]
    [Range(1, 50f)]
    public float _wallKickUpForce = 20;


    [SerializeField]
    [Range(0.1f, 10f)]
    private float _wallHorizontalActivationDistance = 0.3f;

    [SerializeField]
    [Range(0.1f, 3f)]
    private float _wallRunForwardActivationDistance = 0.3f;

    [SerializeField]
    bool _wallJumped = false;

    [Header("Bools")]
    public bool _debugMode;

    [Header("Transform References")]
    public Transform cam;
    public Transform tppcam;
    public Transform MainCamTransform;
    public Transform orientation;
    public Transform groundCheck;

    [Header("Down Force")]
    [SerializeField]
    [Range(1f, 5f)]
    private float _DownForceModifierCap = 2f;
    [SerializeField]
    [Range(1f, 3f)]
    private float _gravityIncreaseRate = 1f;
    [SerializeField]
    private float _currentGravityModifier = 1f;
    [SerializeField]
    [Range(0.01f, 1f)]
    private float _gravityModifierTimer = 0.5f;
    [SerializeField]
    private float _currentGravityModifierTimer = 0;



    [Header("Masks")]
    public LayerMask groundMask;
    public LayerMask wallMask;
    public LayerMask vaultMask;

    [Header("Misc")]
    private Rigidbody rb;
    float turnSmoothVelocity;
    public float CameraSmooth = 0.1f;
    [SerializeField]
    private float _animationWallCheckSize = 2f;
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

    public bool CheckGrounded()
	{
        if (_playerState == State.Grounded)
		{
            return true;
		}
        return false;
    }
    public bool CheckWallRun()
    {
        if(_playerState == State.Wallrunning)
        {
            return true;
        }
        return false;
	}

	public bool CheckNearWall()
	{
        return Physics.CheckSphere(groundCheck.position, _animationWallCheckSize, wallMask);
    }


	public bool CheckWallLeft()
    {
        if (publicWallLeft)
        {
            return true;
        }
        return false;
    }
    public bool CheckWallRight()
    {
        if (publicWallRight)
        {
            return true;
        }
        else return false;
    }

    private void Update()
    {
        if (cam == tppcam)
        {
            cam = MainCamTransform;
        }

        if (_debugMode)
		{
            DebugMode();
		}

        if (_playerState != State.Wallrunning && _CurrentWallrunForce <= _MaxWallrunForce)
		{
            _CurrentWallrunForce += (Time.deltaTime * _wallRunRegen);
		}

        if (_playerState == State.Wallrunning)
        {
            isWallRunning = true;
            Debug.Log("MV  WALL RUNNING IS TRUE");
        }
        else
        {
            isWallRunning = false;
            Debug.Log("MV  WALL RUNNING IS FALSE");
        }

        // Input and State Handling
        switch (_playerState)
        {
            case State.Grounded:
                {
					if (!CheckGrounded())
					{
                        _playerState = State.NotGrounded;
					}
                    _ActionTimer += Time.deltaTime;
                    if (_ActionTimer > _ActionTimerLength)
                    {
                        _wallJumped = false;
                        //if (Input.GetKeyDown(KeyCode.Space))
                        if (Input.GetButtonDown("Jump"))
                        {
                            Jump();
                            _playerState = State.NotGrounded;
                            _ActionTimer = 0;
                        }
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
                            _wallJumped = false;
                        }
                        // Resets the action timer if it goes over the length of the timer
                        _ActionTimer = 0;
                    }
                }
                break;
            case State.Wallrunning:
				{
                    //if (Input.GetKeyDown(KeyCode.Space))
                    if(Input.GetButtonDown("Jump"))
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
        Debug.DrawLine(transform.position, transform.position + -transform.right * _wallHorizontalActivationDistance, Color.green);
        Debug.DrawLine(transform.position, transform.position + transform.forward * _wallRunForwardActivationDistance, Color.blue);
    }

    private void Move()
    {
        switch (_playerState)
        {
            case State.Wallrunning:
				{
                    if (WallCheck())
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
                    }
                    else
                    {
                        _playerState = State.NotGrounded;
                    }
                }
                break;
                case State.NotGrounded:
			        {
                    _currentGravityModifierTimer += Time.deltaTime; 
                    if (_currentGravityModifierTimer > _gravityModifierTimer)
					{
                        if (_currentGravityModifier <= _DownForceModifierCap)
						{
                            _currentGravityModifier += Time.deltaTime * _gravityIncreaseRate;  
						}
                        rb.AddForce(_currentGravityModifier * Physics.gravity, ForceMode.Acceleration);
                    }
                        goto default;
			        }
            case State.Grounded:
                _currentGravityModifier = 0;
                _currentGravityModifierTimer = 0;
                goto default;
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
					else
					{
                        if (rb.velocity.magnitude > maxSpeed && _playerState != State.NotGrounded)
						{
                            rb.velocity *= _speedDecayCoefficient;
						}
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
                {
                    if (!_wallJumped)
                    {
                        // Wallrun Jump
                        bool isWallRight = Physics.Raycast(transform.position, transform.right, _wallHorizontalActivationDistance, wallMask);
                        bool isWallLeft = Physics.Raycast(transform.position, -transform.right, _wallHorizontalActivationDistance, wallMask);

                        publicWallRight = isWallRight;
                        publicWallLeft = isWallLeft;

                        rb.AddForce(Vector3.up * _wallKickUpForce, ForceMode.Impulse);
                        if (isWallRight)
                        {
                            rb.AddForce(-transform.right * _wallKickOffForce, ForceMode.Impulse);
                        }
                        if (isWallLeft)
                        {
                            rb.AddForce(transform.right * _wallKickOffForce, ForceMode.Impulse);
                        }
                        _wallJumped = true;
                    }
                }
                break;
            default:
                // Standard Jump
                rb.AddForce(Vector3.up * baseJumpForce, ForceMode.Impulse);
                rb.AddForce(transform.forward * currentJumpForwardForce, ForceMode.Impulse);
                break;
		}
    }
    private bool WallCheck()
    {
        bool isWallRight = Physics.Raycast(transform.position, transform.right, _wallHorizontalActivationDistance, wallMask);
        bool isWallLeft = Physics.Raycast(transform.position, -transform.right, _wallHorizontalActivationDistance, wallMask);
        bool isWallFront = Physics.Raycast(transform.position, transform.forward, _wallRunForwardActivationDistance, wallMask);

        publicWallRight = isWallRight;
        publicWallLeft = isWallLeft;
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
