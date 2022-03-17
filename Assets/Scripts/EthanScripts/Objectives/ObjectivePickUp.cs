using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePickUp : MonoBehaviour
{
    //public GameObject pickUp;
    private float score = 0f;
    private void OnTriggerEnter(Collider other)
    {
        //Destroy(pickUp);
        Destroy(gameObject);
        MasterObjective.score += 1f;
    }
}