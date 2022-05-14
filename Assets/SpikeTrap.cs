using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private bool trapActive;
    public float delay = 3;
    Animation anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (trapActive)
            {
                FindObjectOfType<Player>().Death();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (trapActive)
            {
                FindObjectOfType<Player>().Death();
            }
        }
    }

    private void Start()
    {
        anim = GetComponent<Animation>();
        StartCoroutine(Pewpew());
    }

    private void Update()
    {
        Debug.Log(trapActive);
    }

    IEnumerator Pewpew()
    {
        while (true)
        {
            anim.Play();
            trapActive = true;
            yield return new WaitForSeconds(2);
            trapActive = false;
            yield return new WaitForSeconds(delay);
        }
    }
}