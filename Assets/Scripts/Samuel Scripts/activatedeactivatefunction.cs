using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatedeactivatefunction : MonoBehaviour
{
    public GameObject obj;
    [SerializeField]
    bool active;

    public bool Active { get => active; set => active = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        obj.SetActive(Active);
    }
}
