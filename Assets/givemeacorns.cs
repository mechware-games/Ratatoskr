using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class givemeacorns : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Acorns", 100);
        PlayerPrefs.SetInt("GoldenAcorns", 100);
    }
}
