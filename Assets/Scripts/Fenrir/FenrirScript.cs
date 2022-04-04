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
    [Range(0.1f, 20f)]
    public float _speed;

    [SerializeField]
    [Range(1f, 10f)]
    public float _chaseLength;
    public float _chaseTimer;

    [SerializeField]
    [Range(1f,5f)]
    private float _pauseLength;
    private float _pauseTimer;

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
	#endregion

	#region Offsets
	[SerializeField]
    Vector3 _chasePositionOffset = new Vector3(0, 1, 0);

    [SerializeField]
    [Tooltip("The vector Fenrir will use for strafing when chasing the player and not closer than the full agrro range.")]
    Vector3 _fenrirStrafeVector = new Vector3(0, 1, 0);
    #endregion

    #region State
    private enum State { Chasing, Despawned };
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

    // Start is called before the first frame update
    void Start()
    {
        _pauseTimer = _pauseLength;
        _chaseTimer = _chaseLength;
        _children = new List<Transform>(transform.GetComponentsInChildren<Transform>());
        _LastDistanceFromPlayer = (_player.position - transform.position).magnitude;

    }

    public bool GetActive()
	{
        return _isActive;
	}

    public void SetActive(bool tf)
	{
        _isActive = tf;
	}

	private void OnEnable()
    {
        _children = new List<Transform>(transform.GetComponentsInChildren<Transform>());
        // The player must have the tag "Player" for this script to work.
        // No other object in the scene should have the tag "Player"
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _currentState = State.Chasing;
        if (!_isActive)
		{
            Spawn(false);
		}

    }

	// Update is called once per frame
	void Update()
    {
        transform.LookAt(_playerSpottedLocation);
        if (_isActive)
        {
            switch (_currentState)
            {
                case State.Chasing:
                    MoveTowardsLastKnownPlayerLocation();
                    _chaseTimer -= Time.deltaTime;
                    if (_chaseTimer < 0)
                    {
                        _chaseTimer = _chaseLength; _currentState = State.Despawned;
                        ToggleChildrenActive(false);
                        GetComponent<SphereCollider>().enabled = false;
                        _LastDistanceFromPlayer = (_player.position - transform.position).magnitude;
                    };
                    break;
                case State.Despawned:
                    _pauseTimer -= Time.deltaTime;
                    if (_pauseTimer < 0) { _pauseTimer = _pauseLength; _currentState = State.Chasing; Spawn(true); }
                    break;
            }
        }
    }
    private void ToggleChildrenActive(bool tf_switch)
	{
        for(int i = 1; i < _children.Count; i++)
		{
            _children[i].gameObject.SetActive(tf_switch);
		}
	}
     
    void MoveTowardsLastKnownPlayerLocation()
    {
        _playerSpottedLocation = _player.position;
        
        float distanceFromPlayer = (_player.position - transform.position).magnitude;
        Vector3 movementDirection;
        
        // Causes Fenrir to chase the player more directly when they are within 5 units of the player
        if (distanceFromPlayer < 5) 
        { 
            movementDirection = _playerSpottedLocation - transform.position;
        }
		else
		{
            movementDirection = (_playerSpottedLocation + _chasePositionOffset) - transform.position;
        }

        movementDirection = movementDirection.normalized;

        transform.position += movementDirection * _speed * Time.deltaTime;
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

    public void Spawn(bool tf_switch)
	{
        if (_LastDistanceFromPlayer > _maxFenrirSpawnRange)
		{
            _LastDistanceFromPlayer = _maxFenrirSpawnRange;
		}
        Vector3 spawningVector = GetSpawningDirection() * _LastDistanceFromPlayer;
        spawningVector = GetOffSetVector(_player.position, spawningVector);
        Vector3 spawnLocation = _player.position + spawningVector;
        transform.position = spawnLocation;
		ToggleChildrenActive(tf_switch);
		GetComponent<SphereCollider>().enabled = tf_switch;
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
		{
            SceneManager.LoadScene("Main Scene");
        }
	}
}
