using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPanner : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCameraBase cam1;
    [SerializeField] private CinemachineVirtualCameraBase cam2;
    [SerializeField] private CinemachineVirtualCameraBase cam3;

    [SerializeField] private CinemachineVirtualCameraBase startCam;

    [SerializeField] private Vector3 boxSize;

    private bool hasEnteredOnce;

    [SerializeField] private float secondsBetweenPan = 3f;

    BoxCollider box;

    void Awake()
    {
        box = gameObject.GetComponent<BoxCollider>();
        box.isTrigger = true;
        box.size = boxSize;
        hasEnteredOnce = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && hasEnteredOnce == false)
        {
            hasEnteredOnce = true;
            StartCoroutine(CamSwapper());
        }
    }

    private IEnumerator CamSwapper()
    {
        if (CameraSwitcher.ActiveCamera != cam1)
        {
            CameraSwitcher.SwitchCamera(cam1);
            yield return new WaitForSeconds(secondsBetweenPan);

            CameraSwitcher.SwitchCamera(cam2);
            yield return new WaitForSeconds(secondsBetweenPan);

            CameraSwitcher.SwitchCamera(cam3);
            yield return new WaitForSeconds(secondsBetweenPan);

            CameraSwitcher.SwitchCamera(startCam);
        }
    }
}