using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AcornMagnet : MonoBehaviour
{
    [SerializeField]
    private float speed = 12f;

    Vector3 moveDirection = new Vector3(0,0,0);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 ratPos = other.gameObject.transform.position;
            Vector3 acornPos = gameObject.transform.position;
            moveDirection = (ratPos - acornPos).normalized;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 ratPos = other.gameObject.transform.position;
            Vector3 acornPos = gameObject.transform.position;
            moveDirection = (ratPos - acornPos).normalized;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        moveDirection = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        gameObject.transform.parent.position += moveDirection * speed * Time.deltaTime;
    }
}