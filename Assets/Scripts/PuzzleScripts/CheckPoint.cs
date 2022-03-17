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

    [SerializeField]
    private GameObject _mesh;

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
        _mesh.GetComponent<MeshRenderer>().material = _blue;
        gameObject.transform.Find("Point Light").gameObject.GetComponent<Light>().color = Color.blue;

    }

    public void UnChangeColour()
    {
        _mesh.GetComponent<MeshRenderer>().material = _red;
        gameObject.transform.Find("Point Light").gameObject.GetComponent<Light>().color = Color.red;
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
        _mesh.GetComponent<MeshRenderer>().material = _gold;
        gameObject.transform.Find("Point Light").gameObject.GetComponent<Light>().color = Color.green;
    }


}
