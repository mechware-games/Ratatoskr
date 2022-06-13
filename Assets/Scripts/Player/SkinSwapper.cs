using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSwapper : MonoBehaviour
{
    private PlayerSkins ratSkin;

    private void Start()
    {
        ratSkin = GameObject.FindGameObjectWithTag("PlayerSkin").GetComponent<PlayerSkins>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ratSkin.SetActiveSkin(ratSkin.normalSkin);
        }
    }
}
