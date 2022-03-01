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

	private class Checkpoint
	{
        public Checkpoint (float timerLength)
		{
            activated = true;
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

    // Start is called before the first frame update
    void OnEnable()
    {

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
                Debug.Log(randomNum);
            }

           
            _order.Add(randomNum); // Adds the number to the order list after confirming it isn't already in the list

		}
        Debug.Log($"The correct order: {_order[0]}{_order[1]}{_order[2]}");
    }

    // Update is called once per frame
    void Update()
    {
        DecrementTimers();
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
                    _checkPoints[index].GetComponent<CheckPoint>().UnChangeColour();
                    _checkPointStatuses[index].activated = false;
                    _checkPointStatuses[index].timer = _checkpointResetTimerLength;
                }
			}        
		}
	}
}
