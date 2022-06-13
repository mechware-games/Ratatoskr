using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class givemeacorns : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Acorns", 99);
        PlayerPrefs.SetInt("GoldenAcorns", 99);
    }
}
