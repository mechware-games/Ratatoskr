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

    private GameObject[] contList;

    private void OnEnable()
    {
        anim = GetComponentInChildren<Animator>();
        movementController = GetComponent<Movement>();
    }
    private void Start()
    {
        fenrir = GameObject.Find("Fenrir");
        deathSound = GetComponent<AudioSource>();
        anim.Rebind();
        movementController = movementControl();

        contList = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update()
    {
        if (movementController == null)
        {
            movementControl();
        }
        if (Input.GetButtonDown("Restart"))
        {
            FenrirScript fenrir = GameObject.Find("Fenrir").GetComponent<FenrirScript>();
            fenrir.SetActive(false);
            fenrir.Despawn();
            movementController.KillPlayer();
            restarting = true;
            StartCoroutine(RestartLoop());
        }
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

    private Movement movementControl()
    {
        for (int i = 0; i < contList.Length; i++)
        {
            if (contList[i].GetComponent<Movement>() != null)
            {
                return movementController = contList[i].GetComponent<Movement>();
            }
        }
        return null;
    }

    public Transform GetPlayer()
    {
        return null;
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
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShrinkUI());
        yield return new WaitForSecondsRealtime(2.5f);
        MoveRat();
        SetHasDied(false);
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(GrowUI());
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