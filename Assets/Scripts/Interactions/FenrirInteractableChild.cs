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
            fenrir._speed = 5;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fenrir"))
        {
            isFenrirInsideMe = false;
            Debug.Log("FENRIR IS NOT INSIDE ME");

            fenrir._speed = 10;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Fenrir"))
        {
            isFenrirInsideMe = true;
            Debug.Log("FENRIR IS INSIDE ME");

        }
    }
}