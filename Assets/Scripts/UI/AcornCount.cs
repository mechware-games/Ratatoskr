using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AcornCount : MonoBehaviour
{
    [SerializeField] private TMP_Text acorntext;
    [SerializeField] private TMP_Text goldacorntext;

    private void Start()
    {
        acorntext.text = PlayerPrefs.GetInt("Acorns").ToString();
        goldacorntext.text = PlayerPrefs.GetInt("GoldenAcorns").ToString();
    }

    private void Update()
    {
        acorntext.text = PlayerPrefs.GetInt("Acorns").ToString();
        goldacorntext.text = PlayerPrefs.GetInt("GoldenAcorns").ToString();
    }
}