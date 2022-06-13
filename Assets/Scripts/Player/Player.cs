using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public Vector3 LastCheckpoint;

    private GameObject fenrir;

    private bool hasDied = false;
    private Animator anim;

    private float maskMinSize = 0.01f;
    private float maskMaxSize = 40f;
    [SerializeField] private float rate = 3f;

    [SerializeField] private GameObject acornCanvas;
    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private GameObject deathMask;
    [SerializeField] private Movement movementController;
    public bool restarting = false;
    [SerializeField] private AudioSource deathSound;

    private void OnEnable()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        fenrir = GameObject.Find("Fenrir");
        deathSound = GetComponent<AudioSource>();
        anim.Rebind();
        movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            FenrirScript fenrir = GameObject.Find("Fenrir").GetComponent<FenrirScript>();
            fenrir.SetActive(false);
            fenrir.Despawn();
            movementController.KillPlayer();
            restarting = true;
            StartCoroutine(RestartLoop());
        }

        Debug.Log("HasDied: " + hasDied);
        isFenrirAfterYou();
    }

    public void Death()
    {
        FenrirScript fenrir = GameObject.Find("Fenrir").GetComponent<FenrirScript>();
        fenrir.SetActive(false);
        fenrir.Despawn();
        movementController.KillPlayer();
        StartCoroutine(DeathLoop());
    }

    public void isFenrirAfterYou()
    {
        if(fenrir.GetComponent<FenrirScript>().GetActive())
        {
            acornCanvas.SetActive(true);
        }
        else
        {
            acornCanvas.SetActive(false);
        }
    }

    public bool HasDied()
    {
        return hasDied;
    }

    public void SetHasDied(bool setting)
    {
        hasDied = setting;
    }

    private void MoveRat()
    {
        transform.position = LastCheckpoint;
    }

    IEnumerator DeathLoop()
    {
        deathCanvas.SetActive(true);
        SetHasDied(true);
        deathSound.Play();
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(ShrinkUI());
        yield return new WaitForSecondsRealtime(2.5f);
        MoveRat();
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(GrowUI());
        yield return new WaitForSeconds(1.5f);
        SetHasDied(false);
        yield return new WaitForSeconds(0.3f);
        movementController.RevivePlayer();
        deathCanvas.SetActive(false);
        yield return null;
    }

    IEnumerator RestartLoop()
    {
        deathCanvas.SetActive(true);
        SetHasDied(false);
        StartCoroutine(ShrinkUI());
        yield return new WaitForSecondsRealtime(2.5f);
        SetHasDied(true);
        MoveRat();
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(GrowUI());
        SetHasDied(false);
        yield return new WaitForSeconds(0.3f);
        movementController.RevivePlayer();
        restarting = false;
        deathCanvas.SetActive(false);
        yield return null;
    }
    IEnumerator ShrinkUI() 
    {
        float scale = Mathf.Clamp(maskMaxSize, maskMinSize, maskMaxSize);
        while(deathMask.transform.localScale.x > maskMinSize)
        {
            scale -= rate * Time.deltaTime;
            deathMask.transform.localScale -= new Vector3(scale, scale, scale) * Time.deltaTime;
            yield return null;
            if (deathMask.transform.localScale.x < maskMinSize)
            {
                deathMask.transform.localScale = new Vector3(maskMinSize, maskMinSize, maskMinSize);
            }
            yield return null;
        }
    }
    IEnumerator GrowUI()
    {
        float scale = Mathf.Clamp(maskMinSize, maskMaxSize, maskMinSize);

        while (deathMask.transform.localScale.x < maskMaxSize)
        {
            scale += rate * Time.deltaTime;
            deathMask.transform.localScale += new Vector3(scale, scale, scale) * Time.deltaTime;
            yield return null;
            if (deathMask.transform.localScale.x > maskMaxSize)
            {
                deathMask.transform.localScale = new Vector3(maskMaxSize, maskMaxSize, maskMaxSize);
            }
            yield return null;
        }
    }
}