using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSwitchTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCameraBase camToSwitchTo;
    [SerializeField] private Vector3 boxSize;

    BoxCollider box;

    void Awake()
    {
        box = gameObject.GetComponent<BoxCollider>();
        box.isTrigger = true;
        box.size = boxSize;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CameraSwitcher.ActiveCamera != camToSwitchTo)
            {
                CameraSwitcher.SwitchCamera(camToSwitchTo);
            }
        }
    }
}