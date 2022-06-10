using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePickUp : MonoBehaviour
{
    //public GameObject pickUp;
    private float score = 0f;
    [SerializeField]
    private bool acorn;
    [SerializeField]
    private bool goldenAcorn;

    [SerializeField] private AudioSource sound;


    private void Start()
    {
        //sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(pickUp);
        if (acorn)
        {
            sound.Play();
            Destroy(gameObject);
            MasterObjective.acorns += 1f;
            globalValues.acorns += 1;
        }
        if (goldenAcorn)
        {
            sound.Play();
            Destroy(gameObject);
            MasterObjective.goldenAcorns += 1f;
            globalValues.goldenAcorns += 1;
        }
    }
}