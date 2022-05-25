using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenrirInteractableChild : MonoBehaviour
{
    public bool isFenrirInsideMe;
    public GameObject sphere;
    public bool blank = false;
    public FenrirScript fenrir;

    private void Start()
    {
        fenrir = GetComponentInParent(typeof(FenrirScript)) as FenrirScript;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fenrir"))
        {
            isFenrirInsideMe = true;
            Debug.Log("FENRIR IS INSIDE ME");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fenrir"))
        {
            isFenrirInsideMe = false;
            Debug.Log("FENRIR HAS LEFT ME");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Fenrir"))
        {
            isFenrirInsideMe = true;
            Debug.Log("FENRIR IS staying INSIDE ME");
        }
    }
}