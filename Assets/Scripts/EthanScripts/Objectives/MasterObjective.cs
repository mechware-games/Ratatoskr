using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterObjective : MonoBehaviour
{
    public static float score = 0f;
    public Text scoreText;

    void Update()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }
}