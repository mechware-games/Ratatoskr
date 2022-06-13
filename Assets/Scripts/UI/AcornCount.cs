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
        if(PlayerPrefs.GetInt("Acorns") > 99)
        {
            acorntext.text = "99";
        }
        else acorntext.text = PlayerPrefs.GetInt("Acorns").ToString();

        if (PlayerPrefs.GetInt("GoldenAcorns") > 99)
        {
            goldacorntext.text = "99";
        }
        else goldacorntext.text = PlayerPrefs.GetInt("GoldenAcorns").ToString();
    }
}