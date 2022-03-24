using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRegister : MonoBehaviour
{
    private void OnEnable()
    {
        CameraSwitcher.RegisterCamera(GetComponent<CinemachineVirtualCameraBase>());
    }

    private void OnDisable()
    {
        CameraSwitcher.UnregisterCamera(GetComponent<CinemachineVirtualCameraBase>());
    }
}
