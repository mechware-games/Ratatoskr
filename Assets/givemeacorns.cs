using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class givemeacorns : MonoBehaviour
{
    public int acorns = 99;
    public int gacorns = 99; 

    void Start()
    {
        PlayerPrefs.SetInt("Acorns", acorns);
        PlayerPrefs.SetInt("GoldenAcorns", gacorns);
    }
}
