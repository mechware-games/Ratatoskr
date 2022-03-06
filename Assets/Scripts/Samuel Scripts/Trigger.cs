using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public MoveObjectontrigger arse;
    public bool stoponexit;

    private void OnTriggerEnter(Collider other)
    {
        arse.activate = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (stoponexit)
        {
            arse.activate = false;
        }
    }
}
