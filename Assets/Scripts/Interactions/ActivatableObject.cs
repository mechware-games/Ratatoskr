using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableObject : MonoBehaviour
{
    public Material activeMat;
    public Material inactiveMat;

    [SerializeField]
    private bool _isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        if (_isActive)
		{
            Activate();
		}
		else
		{
            Deactivate();
		}
    }

    public void Activate()
	{
        _isActive = true;
        gameObject.GetComponent<Renderer>().material = activeMat;
        gameObject.GetComponent<BoxCollider>().enabled = true;
	}

    public void Deactivate()
	{
        _isActive = false;
        gameObject.GetComponent<Renderer>().material = inactiveMat;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
