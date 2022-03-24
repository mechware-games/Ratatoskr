using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class TPPVCam : MonoBehaviour
{
    public float Speed = 5f;
    public float PitchMax = 70f;
    public float PitchMin = 45f;

    private CinemachineVirtualCamera cam;
    [SerializeField] private Transform lookAt;

    private float _currentX, _currentY;

    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();

        cam.LookAt = lookAt;
        cam.Follow = lookAt;
    }

    private void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        _currentX = Mathf.Clamp(_currentX + y * Speed, PitchMin, PitchMax);
        _currentY += x * Speed;

        Vector3 dir = Vector3.forward * 10f;

        Vector3 input = new Vector3(-_currentX, _currentY);
        Quaternion r = Quaternion.Euler(input);

        Vector3 freeLook = r * dir;
        Vector3 pos = transform.position + freeLook;
        lookAt.position = pos;

        Debug.DrawLine(lookAt.position, transform.position, Color.green);
    }
}
