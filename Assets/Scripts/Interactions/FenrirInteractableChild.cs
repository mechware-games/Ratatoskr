using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenrirInteractableChild : MonoBehaviour
{
    public bool isFenrirInsideMe;
    public GameObject sphere;
    public bool blank = false;

    private GameObject fenrirHimself;
    private FenrirScript fenrir;

    [SerializeField] private float fenrirSpeed;
    private float fenrirBaseSpeed;


    private void Start()
    {
        fenrirHimself = GameObject.FindWithTag("Fenrir");
        fenrir = fenrirHimself.GetComponent<FenrirScript>();
        fenrirBaseSpeed = fenrir.GetBaseSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fenrir"))
        {
            isFenrirInsideMe = true;
            fenrir.currentSpeed = fenrirSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fenrir"))
        {
            isFenrirInsideMe = false;
            fenrir.currentSpeed = fenrirBaseSpeed;
        }
    }
}