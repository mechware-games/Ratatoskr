using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenrirInteractions : Interactable
{
    //get outerradius
    public float aoeRadius;
    public float aoeBaseRadius;
    public float aoeTimeToGrow;

    public float aoeMaxTimeActive;

    private float fenrirBaseSpeed;

    private bool isActive = false;
    private bool isGrowing = false;
    private bool isShrinking = false;

    [Header("Fenrir Effects")]
    [SerializeField] private float fenrirNewSpeed;

    private FenrirInteractableChild child;

    private void Start()
    {
        child = GetComponentInChildren<FenrirInteractableChild>();
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
        range.radius = radius;
        GetInput();
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

        yield return new WaitForSeconds(2);
        isActive = false;
    }
}