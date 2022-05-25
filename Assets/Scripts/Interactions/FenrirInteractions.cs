using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenrirInteractions : Interactable
{
    //get outerradius
    public FenrirScript fenrir;
    public float aoeRadius;
    public float aoeBaseRadius;
    public float aoeTimeToGrow;

    public float aoeMaxTimeActive;

    private float fenrirBaseSpeed;
    private float fenrirNewSpeed;

    private bool isActive = false;
    private bool isGrowing = false;
    private bool isShrinking = false;

    private FenrirInteractableChild child;

    private void Start()
    {
        fenrirBaseSpeed = fenrir._speed;
        fenrirNewSpeed = fenrirBaseSpeed * 0.1f;

        child = GetComponentInChildren(typeof(FenrirInteractableChild)) as FenrirInteractableChild;
    }

    public override void Action()
    {
        if (!isActive)
        {
            StartCoroutine(GrowArea());
        }
    }

    private void Update()
    {
        GetInput();
        if (child.isFenrirInsideMe)
        {
            fenrir._speed = fenrirNewSpeed;
        }
        else
        {
            fenrir._speed = fenrirBaseSpeed;
        }
    }

    IEnumerator GrowArea()
    {
        float scaleUp = Mathf.Lerp(aoeBaseRadius, aoeRadius, aoeTimeToGrow);
        float scaleDown = Mathf.Lerp(aoeRadius, aoeBaseRadius, aoeTimeToGrow);

        isActive = true;
        isGrowing = true;

        while (isGrowing)
        {
            child.transform.localScale += new Vector3(scaleUp, scaleUp, scaleUp) * Time.deltaTime;
            yield return null;

            if(child.transform.localScale.x >= aoeRadius)
            {
                isGrowing = false;
            }
            Debug.Log("transform x: " + child.transform.localScale.x);
            Debug.Log("scaleUp: " + scaleUp);
        }
        yield return new WaitForSeconds(aoeTimeToGrow);

        Debug.Log("we are fully grown");
        isGrowing = false;

        yield return new WaitForSeconds(aoeMaxTimeActive);

        Debug.Log("We have waited the time to grow, now start to shrink");
        isShrinking = true;

        while (isShrinking)
        {
            child.transform.localScale -= new Vector3(scaleUp, scaleUp, scaleUp) * Time.deltaTime;
            yield return null;

            if (child.transform.localScale.x <= aoeBaseRadius)
            {
                isShrinking = false;
            }
        }
        yield return new WaitForSeconds(aoeTimeToGrow);

        // yield return new WaitForSeconds(aoeMaxTimeActive);
        // 
        // 
        // while (child.transform.localScale.x > aoeBaseRadius)
        // {
        //     child.transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime;
        // }
        // 
        // if (child.transform.localScale.x < aoeBaseRadius)
        // {
        //     child.transform.localScale = new Vector3(aoeBaseRadius, aoeBaseRadius, aoeBaseRadius);
        // }
        // 
        // yield return new WaitForSeconds(1);

        yield return new WaitForSeconds(2);
        isActive = false;
    }
}