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

    [SerializeField] private MeshRenderer acornMesh;

    private float destroyTimer = 0f;
    private float destroyTimerLength = 3f;

    private bool despawning = false;


	private void Update()
	{
        if (despawning)
        {
            RemoveFromGame();
        }
    }
	private void Start()
    {
        //sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (acorn)
        {
            int acorns = PlayerPrefs.GetInt("Acorns", 0);
            acorns += 1;
            PlayerPrefs.SetInt("Acorns", acorns);
            sound.Play();
            despawning = true;
            MasterObjective.acorns += 1f;
        }
        if (goldenAcorn)
        {
            int goldenAcorns = PlayerPrefs.GetInt("GoldenAcorns", 0);
            goldenAcorns += 1;
            PlayerPrefs.SetInt("GoldenAcorns", goldenAcorns);
            sound.Play();
            despawning = true;
            MasterObjective.goldenAcorns += 1f;
        }
    }
    private void RemoveFromGame()
    {
        destroyTimer += Time.deltaTime;
        acornMesh.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        if (destroyTimer > destroyTimerLength) { Destroy(gameObject); }
    }
}