using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCheckpoint : MonoBehaviour
{
    public AudioSource audio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (FindObjectOfType<Player>().LastCheckpoint != transform.position) { audio.Play(); }
            else if (FindObjectOfType<Player>().HasDied()) { audio.Play(); };
            FindObjectOfType<Player>().LastCheckpoint = transform.position;
        }
    }
}
