using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenrirInteractions : Interactable
{
    //get outerradius
    public float aoeRadius;
    public float aoeBaseRadius;
    public float aoeGrowth;
    private float aoeTimeToGrowTimer = 0f;

    public float aoeMaxTimeActive;

    private bool isActive = false;
    private bool isGrowing = false;
    private bool isShrinking = false;

    public AudioSource audio;

    private FenrirInteractableChild child;

    private void Start()
    {
        child = GetComponentInChildren<FenrirInteractableChild>();
    }

    public override void Action()
    {
        if (!isActive)
        {
            aoeTimeToGrowTimer = 0f;
            audio.Play();
            StartCoroutine(GrowArea());
        }
    }

    private void Update()
    {
        range.radius = radius;
        GetInput();
    }

    IEnumerator GrowArea()
    {
        isActive = true;
        isGrowing = true;

        while (isGrowing)
        {
            child.transform.localScale += new Vector3(aoeGrowth, aoeGrowth, aoeGrowth) * Time.deltaTime;
            yield return null;

            if(child.transform.localScale.x >= aoeRadius)
            {
                isGrowing = false;
            }
        }

        Debug.Log("we are fully grown");
        isGrowing = false;

        yield return new WaitForSeconds(aoeMaxTimeActive);

        Debug.Log("We have waited the time to grow, now start to shrink");
        isShrinking = true;

        while (isShrinking)
        {
            aoeTimeToGrowTimer += Time.deltaTime;
            child.transform.localScale -= new Vector3(aoeGrowth, aoeGrowth, aoeGrowth) * Time.deltaTime;
            yield return null;

            if (child.transform.localScale.x <= aoeBaseRadius)
            {
                isShrinking = false;
            }
        }

        isActive = false;
    }
}