using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public Vector3 LastCheckpoint;

    private GameObject fenrir;
    private bool hasDied = false;

    private float maskMinSize = 40f;
    private float maskMaxSize = 0.01f;
    [SerializeField] private float rate = 10;

    [SerializeField] private GameObject acornCanvas;
    [SerializeField] private GameObject deathMask;

    private void Start()
    {
        fenrir = GameObject.Find("Fenrir");
    }

    private void Update()
    {
        Debug.Log("HasDied: " + hasDied);
        isFenrirAfterYou();
    }

    public void Death()
    {
        FenrirScript fenrir = GameObject.Find("Fenrir").GetComponent<FenrirScript>();
        fenrir.SetActive(false);
        fenrir.Despawn();

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
        SetHasDied(true);
        yield return new WaitForSecondsRealtime(.5f);
        StartCoroutine(ShrinkUI());
        yield return new WaitForSecondsRealtime(1);
        MoveRat();
        yield return new WaitForSeconds(.5f);
        StartCoroutine(GrowUI());
        SetHasDied(false);
        yield return null;
    }

    IEnumerator ShrinkUI() 
    {
        float scale = Mathf.Clamp(maskMaxSize, maskMinSize, maskMaxSize);
        while(deathMask.transform.localScale.x > 1)
        {
            scale -= rate * Time.deltaTime;
            deathMask.transform.localScale -= new Vector3(scale, scale, scale) * Time.deltaTime * rate;
            yield return null;
        }
    }

    IEnumerator GrowUI()
    {
        float scale = Mathf.Clamp(maskMinSize, maskMaxSize, maskMinSize);
        while (deathMask.transform.localScale.x < 40)
        {
            scale += rate * Time.deltaTime;
            deathMask.transform.localScale += new Vector3(scale, scale, scale) * Time.deltaTime * rate;
            yield return null;
        }
    }
}