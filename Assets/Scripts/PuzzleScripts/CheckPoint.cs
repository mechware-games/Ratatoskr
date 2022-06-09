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
    [SerializeField]
    private AudioSource _activeSound;

    bool _isActive;

    private int _checkpointIndex;

    // Start is called before the first frame update
    void Start()
    {
        _isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColour()
	{
        if (!_isActive)
        {
            _activeSound.Play();
        }
        _mesh.GetComponent<MeshRenderer>().material = _blue;
        gameObject.transform.Find("Point Light").gameObject.GetComponent<Light>().color = Color.blue;
        _isActive = true;
    }

    public void UnChangeColour()
    {
        _mesh.GetComponent<MeshRenderer>().material = _red;
        gameObject.transform.Find("Point Light").gameObject.GetComponent<Light>().color = Color.red;
        _isActive = false;
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
