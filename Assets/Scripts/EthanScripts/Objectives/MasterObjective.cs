using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MasterObjective : MonoBehaviour
{
    public static float score = 0f;
    public TMP_Text scoreText;

    private void OnEnable()
    {
        score = 0f;
    }

    void Update()
    {
        scoreText.text = score.ToString();
    }
}