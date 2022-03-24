using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class CameraSwitcher
{
    static List<CinemachineVirtualCameraBase> cameras = new List<CinemachineVirtualCameraBase>();
    public static CinemachineVirtualCameraBase ActiveCamera = null;

    public static void SwitchCamera(CinemachineVirtualCameraBase camera)
    {
        camera.Priority = 10;
        ActiveCamera = camera;
        foreach(CinemachineVirtualCameraBase cam in cameras)
        {
            if(cam != camera && cam.Priority != 0)
            {
                cam.Priority = 0;
            }
        }
    }

    public static void RegisterCamera(CinemachineVirtualCameraBase cam)
    {
        Debug.Log("added camera " + cam);
        cameras.Add(cam);
    }

    public static void UnregisterCamera(CinemachineVirtualCameraBase cam)
    {
        cameras.Remove(cam);
    }

    public static bool IsActiveCamera(CinemachineVirtualCameraBase cam)
    {
        return cam == ActiveCamera;
    }
}