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

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(pickUp);
        if (acorn)
        {
            Destroy(gameObject);
            MasterObjective.acorns += 1f;
        }
        if (goldenAcorn)
        {
            Destroy(gameObject);
            MasterObjective.goldenAcorns += 1f;
        }
    }
}