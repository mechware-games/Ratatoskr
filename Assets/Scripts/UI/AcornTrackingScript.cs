using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AcornTrackingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int acorns = PlayerPrefs.GetInt("Acorns", 0);
        int goldenAcorns = PlayerPrefs.GetInt("GoldenAcrons", 0);
        transform.GetComponent<TextMeshProUGUI>().SetText( $"Acorns: {acorns}\nGolden Acorns: {goldenAcorns}");
    }
}
