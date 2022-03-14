using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private CheckPointManager _manager;

    [SerializeField]
    private Material _red;

    [SerializeField]
    private Material _blue;

    [SerializeField]
    private Material _gold;

    private int _checkpointIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColour()
	{   
        gameObject.GetComponent<MeshRenderer>().material = _blue;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void UnChangeColour()
    {
        gameObject.GetComponent<MeshRenderer>().material = _red;
        gameObject.GetComponent<Renderer>().enabled = true;
    }

    public void SetIndex(int value)
	{
        _checkpointIndex = value;
	}

	public void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
		{
            _manager.ActivateCheckPoint(_checkpointIndex);
        }
	}

    public void CompletedColour()
	{
        gameObject.GetComponent<MeshRenderer>().material = _gold;
        gameObject.GetComponent<Renderer>().enabled = true;
    }


}
