using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FenrirScript : MonoBehaviour
{
    private Transform _player;

    #region CoreValues
    [Header("Core Values")]
    [SerializeField]
    [Range(0.1f, 50f)]
    private float _baseSpeed = 30f;
    public float currentSpeed;
    

    [SerializeField]
    [Range(1f, 30f)]
    public float _chaseLength;
    public float _chaseTimer;

    [SerializeField]
    [Range(1f,5f)]
    private float _pauseLength;
    private float _pauseTimer;

    [Header("Boss Fight Shit")]
    private float health = 60f;

    #endregion

    #region Contraints
    [Header("Contraints")]
    [SerializeField]
    [Tooltip("Minimum allowed spawning distance from the player in the x axis.")]
    [Range(2f, 15f)]
    private float _xSpawnMinDistance = 5f;

    [SerializeField]
    [Tooltip("Minimum allowed spawning distance from the player in the y axis.")]
    [Range(2f, 15f)]
    private float _ySpawnMinDistance = 5f;

    [SerializeField]
    [Tooltip("Minimum allowed spawning distance from the player in the z axis.")]
    [Range(2f, 15f)]
    private float _zSpawnMinDistance = 5f;
      
    [SerializeField]
    [Tooltip("Maximum distance Fenrir can spawn from the player.")]
    [Range(5f, 50f)]
    private float _maxFenrirSpawnRange = 10f;

    [SerializeField]
    [Tooltip("Ammount of time before Fenrir checks the player's current location")]
    [Range(0.01f, 5f)]
    private float _fenrirCheckPositionTimerLength = 0.3f;
    private float _fenrirCheckPositionTimer = 0f;

    [SerializeField]
    [Tooltip("Fenrir Pause time after failing to catch the player (When Fenrir is really close to the player).")]
    [Range(0.1f, 3f)]
    private float _fenrirPauseTimerLength = 0.5f;
    private float _fenrirPauseTimer = 0f;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float _fenrirPauseTriggerRadius;
	#endregion

	#region Offsets
	[SerializeField]
    Vector3 _chasePositionOffset = new Vector3(0, 1, 0);

    [SerializeField]
    [Tooltip("The vector Fenrir will use for strafing when chasing the player and not closer than the full agrro range.")]
    Vector3 _fenrirStrafeVector = new Vector3(0, 1, 0);
    #endregion

    #region State
    private enum State { Chasing, Despawned, Waiting };
    [SerializeField]
    private State _currentState = State.Chasing;
    #endregion

    #region Misc
    List<Transform> _children; // Is used for setActive later on

    private float _LastDistanceFromPlayer = 0;

    [Header("Active?")]
    [SerializeField]
    [Tooltip("Is ticked if Fenrir is active.")]
    private bool _isActive;

    private Vector3 _playerSpottedLocation;
	#endregion


	#region needsCleaning
	// Start is called before the first frame update
	void Start()
    {
        _pauseTimer = _pauseLength;
        _chaseTimer = _chaseLength;
        _LastDistanceFromPlayer = (_player.position - transform.position).magnitude;
        if (!GetActive())
        {
            Despawn();
        }
    }

	private void OnEnable()
    {
        currentSpeed = _baseSpeed;
        _children = new List<Transform>(transform.GetComponentsInChildren<Transform>());
        // The player must have the tag "Player" for this script to work.
        // No other object in the scene should have the tag "Player"
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _currentState = State.Chasing;
        _playerSpottedLocation = _player.position;
    }
#endregion

    public float GetBaseSpeed()
	{
        return _baseSpeed;
	}

    private void CheckFenrirDistance()
	{
        float magnitude = (_playerSpottedLocation - transform.position).magnitude;

        if (magnitude < _fenrirPauseTriggerRadius)
		{
            _currentState = State.Waiting;
		}
	}

	// Update is called once per frame
	void Update()
    {
        transform.LookAt(_player.position);
        if (GetActive())
        {
            switch (_currentState)
            {
                case State.Waiting:
                    SetPlayerLastKnownPosition();
                    _fenrirPauseTimer += Time.deltaTime;
                    if (_fenrirPauseTimer > _fenrirPauseTimerLength)
                    {
                        Debug.Log("Timer Triggered");
                        _currentState = State.Chasing;
                        _fenrirPauseTimer = 0;
                    }
                    break;
                case State.Chasing:
                    SetPlayerLastKnownPosition();
                    MoveTowardsLastKnownPlayerLocation();

                    _chaseTimer -= Time.deltaTime;
                    if (_chaseTimer < 0)
                    {
                        Despawn();
                    };
                    CheckFenrirDistance();
                    break;
                case State.Despawned:
                    _pauseTimer -= Time.deltaTime;
                    if (_pauseTimer < 0) { Spawn();}
                    break;
            }
        }
    }

    public void SetActive(bool tf)
    {
        _isActive = tf;
    }

    public bool GetActive()
    {
        return _isActive;
    }
    private void ToggleChildrenActive(bool tf_switch)
	{
        for(int i = 1; i < _children.Count; i++)
		{
            _children[i].gameObject.SetActive(tf_switch);
		}
	}

    void SetPlayerLastKnownPosition()
	{
        _fenrirCheckPositionTimer += Time.deltaTime;
        if (_fenrirCheckPositionTimer > _fenrirCheckPositionTimerLength)
		{
            _fenrirCheckPositionTimer = 0;
            _playerSpottedLocation = _player.position;
        }
    }

    void MoveTowardsLastKnownPlayerLocation()
    {     
        float distanceFromPlayer = (_player.position - transform.position).magnitude;
        Vector3 target;

        // Causes Fenrir to target the player more directly when they are within 5 units of the player
        if (distanceFromPlayer < 5)
		{
            target = _playerSpottedLocation;
        }
		else
		{
            target = _playerSpottedLocation + _chasePositionOffset;
            // Causes Fenrir to strafe if they're not directly targeting the player
            FenrirStrafe(target); 
		}

        Vector3 movementDirection = (target - transform.position).normalized;

        transform.position += currentSpeed * Time.deltaTime * movementDirection;
    }

    // Causes Fenrir to strafe using the strafe vector in relation to the target
    private void FenrirStrafe(Vector3 target)
	{
        // At the moment this is over engineered though I think the using a vector might come in handy later on
        // Fenrir would likely need to strafe if they are chasing the player through a wall
        // A check to determine whether this is the could be done but this is something I'm not going to worry about right now


        // In the future I may want to do a series of checks to consider whether strafing in each axis is necessary
        // I could then use the final strafe variable below to apply the resulting strafe vector using Fenrir's strafe vector
        // Vector3 finalStrafe = new Vector3(0,0,0); 

        if (target.y > transform.position.y)
		{
            transform.position += _fenrirStrafeVector * Time.deltaTime;
		}
    }

    // Gets direction vector that Fenrir will spawn from relative to the player
    // This function may be expanded on to allow the spawning of Fenrir to be more sofisticated
    private Vector3 GetSpawningDirection()
	{
        return new Vector3(Random.value, Random.value, Random.value).normalized;
    }

    // Uses the min spawning distances to offset Fenrir's spawn location and prevent him spawning on top of the player
    private Vector3 GetOffSetVector(Vector3 playerPosition,Vector3 spawningVector)
	{
        Vector3 comparison = playerPosition - spawningVector;
        if (comparison.x < _xSpawnMinDistance && comparison.x > -_xSpawnMinDistance)
		{
            comparison.x = comparison.x > 0 ? _xSpawnMinDistance : -_xSpawnMinDistance;
		}

        if (comparison.y < _ySpawnMinDistance && comparison.y > -_ySpawnMinDistance)
        {
            comparison.y = comparison.y > 0 ? _ySpawnMinDistance : -_ySpawnMinDistance;
        }

        if (comparison.z < _zSpawnMinDistance && comparison.z > -_zSpawnMinDistance)
        {
            comparison.z = comparison.z > 0 ? _zSpawnMinDistance : -_zSpawnMinDistance;
        }
        return comparison;
    }

    public void Despawn()
    {
        _chaseTimer = _chaseLength; 
        _currentState = State.Despawned;
        ToggleChildrenActive(false);
        GetComponent<SphereCollider>().enabled = false;
        _LastDistanceFromPlayer = (_player.position - transform.position).magnitude;
    }

    public void Spawn()
	{
        currentSpeed = _baseSpeed;
        _pauseTimer = _pauseLength;
        _currentState = State.Chasing;
        SetActive(true);
        ToggleChildrenActive(true);
        GetComponent<SphereCollider>().enabled = true;

        // Ensures Fenrir doesn't spawn too far from the player
        if (_LastDistanceFromPlayer > _maxFenrirSpawnRange)
		{
            _LastDistanceFromPlayer = _maxFenrirSpawnRange;
		}

        SetSpawnLocation();
	}

    void SetSpawnLocation()
	{
        Vector3 spawningVector = GetSpawningDirection() * _LastDistanceFromPlayer;
        spawningVector = GetOffSetVector(_player.position, spawningVector);
        Vector3 spawnLocation = _player.position + spawningVector;
        transform.position = spawnLocation;
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
		{
            FindObjectOfType<Player>().Death();
            Despawn();
        }
	}
}
