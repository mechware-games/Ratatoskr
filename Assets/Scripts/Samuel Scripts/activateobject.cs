using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateobject : MonoBehaviour
{
    public activatedeactivatefunction thing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        thing.Active = !thing.Active;
    }
    private void OnTriggerExit(Collider other)
    {

    }
}
