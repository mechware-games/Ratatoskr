using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenrirScript : MonoBehaviour
{
    private Transform _player;

    [SerializeField]
    [Range(0.1f, 20f)]
    private float _speed;

    [SerializeField]
    [Range(1f, 10f)]
    private float _chaseLength;
    private float _chaseTimer;

    [SerializeField]
    [Range(1f,5f)]
    private float _pauseLength;
    private float _pauseTimer;

    [SerializeField]
    [Range(5f, 15f)]
    private float _xSpawnRange = 5f;

    [SerializeField]
    [Range(5f, 15f)]
    private float _ySpawnRange = 5f;

    [SerializeField]
    [Range(5f, 15f)]
    private float _zSpawnRange = 5f;


    [SerializeField]
    [Range(5f, 15f)]
    private float _xSpawnMinDistance = 5f;

    [SerializeField]
    [Range(5f, 15f)]
    private float _ySpawnMinDistance = 5f;

    [SerializeField]
    [Range(5f, 15f)]
    private float _zSpawnMinDistance = 5f;

    private Vector3 _playerSpottedLocation;

    private enum State { Chasing, Despawned };
    private State _currentState = State.Chasing;

    List<Transform> _children;

    // Start is called before the first frame updatei'l
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
                if (_chaseTimer < 0) { _chaseTimer = _chaseLength; _currentState = State.Despawned; ToggleChildrenActive(false); };
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

    void Respawn(bool tf_switch)
	{
        float xOffset = Random.Range(-_xSpawnRange, _xSpawnRange) + _xSpawnMinDistance;
        float yOffset = Random.Range(-_ySpawnRange, _xSpawnRange) + _ySpawnMinDistance;
        float zOffset = Random.Range(-_zSpawnRange, _xSpawnRange) + _zSpawnMinDistance;
        Vector3 RandomPositionModifier = new Vector3(xOffset, yOffset, zOffset);
        transform.position = _player.position + RandomPositionModifier;
        ToggleChildrenActive(tf_switch);
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
		{
            // Insert code for killing the player
		}
	}
}
