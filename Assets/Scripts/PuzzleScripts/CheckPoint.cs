using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    [SerializeField]
    private Material _red;

    [SerializeField]
    private Material _blue;

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
        gameObject.GetComponent<MeshRenderer>().material = _red;
        gameObject.GetComponent<MeshRenderer>().enabled = true;

    }

    public void UnChangeColour()
    {
        gameObject.GetComponent<MeshRenderer>().material = _blue;
        gameObject.GetComponent<Renderer>().enabled = true;
    }
}
