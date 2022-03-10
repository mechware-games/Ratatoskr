using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FenrirScript : MonoBehaviour
{
    private Transform _player;

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


    [SerializeField]
    [Range(2f, 15f)]
    private float _xSpawnMinDistance = 5f;

    [SerializeField]
    [Range(2f, 15f)]
    private float _ySpawnMinDistance = 5f;

    [SerializeField]
    [Range(2f, 15f)]
    private float _zSpawnMinDistance = 5f;

    private Vector3 _playerSpottedLocation;

    private enum State { Chasing, Despawned };
    private State _currentState = State.Chasing;

    List<Transform> _children;

    private float _LastDistanceFromPlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        _pauseTimer = _pauseLength;
        _chaseTimer = _chaseLength;
        _children = new List<Transform>(transform.GetComponentsInChildren<Transform>());

    }

	private void OnEnable()
	{
        // The player must have the tag "Player" for this script to work.
        // No other object in the scene should have the tag "Player"
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _currentState = State.Chasing;

    }

	// Update is called once per frame
	void Update()
    {
        transform.LookAt(_playerSpottedLocation);

        switch (_currentState)
		{
            case State.Chasing:
                MoveTowardsLastKnownPlayerLocation();
                _chaseTimer -= Time.deltaTime;
                if (_chaseTimer < 0) 
                { 
                    _chaseTimer = _chaseLength; _currentState = State.Despawned; 
                    ToggleChildrenActive(false);
                    _LastDistanceFromPlayer = (_player.position - transform.position).magnitude;
                };
                break;
			case State.Despawned:
                _pauseTimer -= Time.deltaTime;
                if (_pauseTimer < 0) { _pauseTimer = _pauseLength; _currentState = State.Chasing; Respawn(true); }
                break;
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

        Vector3 movementDirection = _playerSpottedLocation - transform.position;

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

        if (comparison.y < _xSpawnMinDistance && comparison.y > -_xSpawnMinDistance)
        {
            comparison.y = comparison.y > 0 ? _xSpawnMinDistance : -_xSpawnMinDistance;
        }

        if (comparison.z < _xSpawnMinDistance && comparison.z > -_xSpawnMinDistance)
        {
            comparison.z = comparison.z > 0 ? _xSpawnMinDistance : -_xSpawnMinDistance;
        }
        return comparison;
    }

    void Respawn(bool tf_switch)
	{
        Vector3 spawningVector = GetSpawningDirection() * _LastDistanceFromPlayer;
        spawningVector = GetOffSetVector(_player.position, spawningVector);
        Vector3 spawnLocation = _player.position + spawningVector;
        transform.position = spawnLocation;
        ToggleChildrenActive(tf_switch);
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
		{
            SceneManager.LoadScene("Main Scene");
        }
	}
}
