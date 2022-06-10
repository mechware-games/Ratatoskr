using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePickUp : MonoBehaviour
{
    //public GameObject pickUp;
    private float score = 0f;
    [SerializeField]
    private bool acorn;
    [SerializeField]
    private bool goldenAcorn;

    [SerializeField] private AudioSource sound;


    private void Start()
    {
        //sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(pickUp);
        if (acorn)
        {
            int acorns = PlayerPrefs.GetInt("Acorns", 0);
            acorns += 1;
            PlayerPrefs.SetInt("Acorns", acorns);
            sound.Play();
            Destroy(gameObject);
            MasterObjective.acorns += 1f;
        }
        if (goldenAcorn)
        {
            int goldenAcorns = PlayerPrefs.GetInt("GoldenAcorns", 0);
            goldenAcorns += 1;
            PlayerPrefs.SetInt("Acorns", goldenAcorns);
            sound.Play();
            Destroy(gameObject);
            MasterObjective.goldenAcorns += 1f;
        }
    }
}