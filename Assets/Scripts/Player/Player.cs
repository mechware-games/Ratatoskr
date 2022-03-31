using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 LastCheckpoint;
    public GameObject self;
    Renderer rend;

    public void Death()
    {
        transform.position = LastCheckpoint;
    }
}