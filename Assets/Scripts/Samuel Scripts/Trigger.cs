using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public MovePlatform arse;
    public bool stoponexit;

    private void OnTriggerEnter(Collider other)
    {
        arse.Activate = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (stoponexit)
        {
            arse.Activate = false;
        }
    }
}
