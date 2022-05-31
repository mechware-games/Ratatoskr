using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLevelLoad : MonoBehaviour
{
    public GameObject pause;
    ChangeSceneScript scener;
    [SerializeField] private int level;

    private void Start()
    {
        scener = pause.GetComponent<ChangeSceneScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        scener.LoadLevel(level);
    }
}