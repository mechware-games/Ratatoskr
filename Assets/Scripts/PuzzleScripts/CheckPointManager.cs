using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField]
    [Range(5f, 20f)]
    private float _checkpointResetTimerLength = 10;

    [SerializeField]
    private List<Transform> _checkPoints;

    [SerializeField]
    private int _puzzleProgress = 0;

    [SerializeField]
    private ActivatableObject[] _activatableObjects;
    private class Checkpoint
	{
        public Checkpoint (float timerLength)
		{
            activated = false;
            timer = timerLength;
		}

        public bool CheckTimerCompleted()
		{
            if (timer < 0)
			{
                return true;
			}
            return false;
		}

        public bool activated;
        public float timer;
    }


	[SerializeField]
    private List<Checkpoint> _checkPointStatuses = new List<Checkpoint>();

    private List<int> _order = new List<int>();

    [SerializeField]
    private bool _CompletedPuzzle;
    // Start is called before the first frame update

    [SerializeField]
    private AudioSource _failedAudio;

    [SerializeField]
    private AudioSource _successAudio;

    void OnEnable()
    {
        _CompletedPuzzle = false;

        for (int i = 0; i < _checkPoints.Count; i++)
		{
            Checkpoint newCheckpoint = new(_checkpointResetTimerLength);

            _checkPointStatuses.Add(newCheckpoint);
        }

        int randomNum = Random.Range(0, _checkPoints.Count);
        for (int i = 0; i < _checkPoints.Count; i++)
		{
            Random.InitState(System.DateTime.Now.Millisecond);
            while (_order.Contains(randomNum))
            {
                randomNum = Random.Range(0, _checkPoints.Count);
                //Debug.Log(randomNum);
            }

            _checkPoints[randomNum].GetComponent<CheckPoint>().SetIndex(randomNum);
            _order.Add(randomNum); // Adds the number to the order list after confirming it isn't already in the list

		}
        //Debug.Log($"The correct order: {_order[0]}{_order[1]}{_order[2]}");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_CompletedPuzzle) 
        { 
            DecrementTimers();
            if (CheckPuzzleCompletion())
            {
                for (int i = 0; i < _checkPoints.Count; i++)
				{
                    _checkPoints[i].GetComponent<CheckPoint>().CompletedColour();
				}
                _CompletedPuzzle = true;
                for (int i = 0; i < _activatableObjects.Length; i++)
                {
                    _activatableObjects[i].Activate();
                }

            }
        }
    }

    public void ActivateCheckPoint(int i)
	{
        if (!_checkPointStatuses[i].activated)
        {
            if (i == _order[_puzzleProgress])
            {
                _checkPointStatuses[i].activated = true;
                _checkPointStatuses[i].timer = _checkpointResetTimerLength;
                _puzzleProgress++;
            }
            else
            {
                DeactivateAll();
                _puzzleProgress = 0;
            }
        }
	}

    void DecrementTimers()
	{
        for (int i = 0; i < _checkPoints.Count; i++)
		{
            int index = _order[i];
            if (_checkPointStatuses[index].activated)
            {
                _checkPoints[index].GetComponent<CheckPoint>().ChangeColour();
                _checkPointStatuses[index].timer -= Time.deltaTime; 
                if (_checkPointStatuses[index].CheckTimerCompleted()) // Checks if the activation timer has finished
				{
                    _checkPointStatuses[index].activated = false;
                    _checkPointStatuses[index].timer = _checkpointResetTimerLength;
                    DeactivateAll();
                    _puzzleProgress = 0;
                }
			}
			else
			{
                _checkPoints[index].GetComponent<CheckPoint>().UnChangeColour();
            }
		}
	}

    private bool CheckPuzzleCompletion()
    {
        for (int i = 0; i < _checkPoints.Count; i++)
		{
            if (!_checkPointStatuses[i].activated)
			{
                return false;
			}      
		}
        _successAudio.Play();
        return true;
    }

    private void DeactivateAll()
	{
        _failedAudio.Play();
        for (int i = 0; i < _checkPoints.Count; i++)
		{
            _checkPointStatuses[i].timer = _checkpointResetTimerLength;
            _checkPointStatuses[i].activated = false;
        }
	}
}
