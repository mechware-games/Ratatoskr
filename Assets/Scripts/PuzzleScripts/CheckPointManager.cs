using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    [SerializeField]
    private List<Transform> _checkPoints;

    private List<int> _order = new List<int>();

    // Start is called before the first frame update
    void OnEnable()
    {

        int randomNum = Random.Range(0, _checkPoints.Count);
        for (int i = 0; i < _checkPoints.Count; i++)
		{
            Random.InitState(System.DateTime.Now.Millisecond);
            while (_order.Contains(randomNum))
            {
                randomNum = Random.Range(0, _checkPoints.Count);
                Debug.Log(randomNum);
            }
            _order.Add(randomNum);
		}
        Debug.Log($"The correct order: {_order[0]}{_order[1]}{_order[2]}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
