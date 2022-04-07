using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MasterObjective : MonoBehaviour
{
    public static float acorns = 0f;
    public static float goldenAcorns = 0f;

    public TMP_Text acornText;
    public TMP_Text goldenAcornText;

    private void OnEnable()
    {
        acorns = 0f;
        goldenAcorns = 0f;
    }

    void Update()
    {
        acornText.text = acorns.ToString();
        goldenAcornText.text = goldenAcorns.ToString();
    }
}