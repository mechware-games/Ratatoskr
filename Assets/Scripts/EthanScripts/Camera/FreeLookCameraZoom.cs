using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//[RequireComponent(typeof(CinemachineFreeLook))]
public class FreeLookCameraZoom : MonoBehaviour
{
    private CinemachineFreeLook freelook;
    private CinemachineFreeLook.Orbit[] originalOrbits;

    [Range(0.5F, 2F)]
    public float zoomPercent = 1f; // 1 = 100%
    public float zoomSensitivity = 0.5f;

    public float minZoom = 0.75f;
    public float maxZoom = 2f;

    public void Awake()
    {
        freelook = GetComponentInChildren<CinemachineFreeLook>();
        originalOrbits = new CinemachineFreeLook.Orbit[freelook.m_Orbits.Length];


        for (int i = 0; i < freelook.m_Orbits.Length; i++)
        {
            originalOrbits[i].m_Height = freelook.m_Orbits[i].m_Height;
            originalOrbits[i].m_Radius = freelook.m_Orbits[i].m_Radius;
        }
    }

    public void Update()
    {

        float clampedZoom = Mathf.Clamp(zoomPercent, minZoom, maxZoom);

        if(zoomPercent < minZoom) zoomPercent = minZoom;
        if(zoomPercent > maxZoom) zoomPercent = maxZoom;

        if(Input.GetAxis("Camera Zoom In") > 0)
        {
            zoomPercent += zoomSensitivity * Time.deltaTime;
            Debug.Log("Zoom In");
        }

        if(Input.GetAxis("Camera Zoom Out") < 0)
        {
            zoomPercent -= zoomSensitivity * Time.deltaTime;
            Debug.Log("Zoom Out");
        }

        for (int i = 0; i < freelook.m_Orbits.Length; i++)
        {
            freelook.m_Orbits[i].m_Height = originalOrbits[i].m_Height * clampedZoom;
            freelook.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * clampedZoom;
        }
    }
}