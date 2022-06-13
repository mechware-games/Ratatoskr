using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinStore : MonoBehaviour
{
    public bool isPinkUnlocked = true;
    public bool isBlueUnlocked;
    public bool isGoldUnlocked;

    [SerializeField] private int pinkCost = 0;
    [SerializeField] private int blueCost = 10;
    [SerializeField] private int goldCost = 3;

    [SerializeField] private TMP_Text cost;
    [SerializeField] private TMP_Text costBlue;
    [SerializeField] private TMP_Text costGold;

    [SerializeField] private GameObject goldcostcanvas;
    [SerializeField] private GameObject bluecostcanvas;

    [SerializeField] private GameObject blueSelect;
    [SerializeField] private GameObject pinkSelect;
    [SerializeField] private GameObject goldSelect;


    private void Start()
    {
        cost.text = pinkCost.ToString();
        costBlue.text = blueCost.ToString();
        costGold.text = goldCost.ToString();

        blueSelect.SetActive(false);
        pinkSelect.SetActive(true);
        goldSelect.SetActive(false);
    }

    private void Update()
    {
        Debug.Log("Acorns: " + PlayerPrefs.GetInt("Acorns"));
        Debug.Log("gOLDEN aCORNS: " + PlayerPrefs.GetInt("GoldenAcorns"));
    }

    public void BuyBlue()
    {
        if (isBlueUnlocked == false)
        {
            if (PlayerPrefs.GetInt("Acorns") >= blueCost)
            {
                PlayerPrefs.SetInt("Acorns", PlayerPrefs.GetInt("Acorns") - blueCost);
                isBlueUnlocked = true;
                GameObject.Find("SkinManager").GetComponent<SkinManager>().SetRat(2);
                blueSelect.SetActive(true);
                pinkSelect.SetActive(false);
                goldSelect.SetActive(false);

                bluecostcanvas.SetActive(false);
            }
            else
            {
                Debug.Log("You can't afford this skin!");
            }
        }
        else
        {
            GameObject.Find("SkinManager").GetComponent<SkinManager>().SetRat(2);

            blueSelect.SetActive(true);
            pinkSelect.SetActive(false);
            goldSelect.SetActive(false);
        }
    }
    public void BuyPink()
    {
        if (isPinkUnlocked == false)
        {
            if (PlayerPrefs.GetInt("Acorns") >= pinkCost)
            {
                PlayerPrefs.SetInt("Acorns", PlayerPrefs.GetInt("Acorns") - pinkCost);
                isPinkUnlocked = true;

                GameObject.Find("SkinManager").GetComponent<SkinManager>().SetRat(1);
                blueSelect.SetActive(false);
                pinkSelect.SetActive(true);
                goldSelect.SetActive(false);

            }
            else
            {
                Debug.Log("You can't afford this skin!");
            }
        }
        else
        {
            GameObject.Find("SkinManager").GetComponent<SkinManager>().SetRat(1);

            blueSelect.SetActive(false);
            pinkSelect.SetActive(true);
            goldSelect.SetActive(false);
        }
    }
    public void BuyGold()
    {
        if (isGoldUnlocked == false)
        {
            if(PlayerPrefs.GetInt("GoldenAcorns") >= goldCost)
            {
                PlayerPrefs.SetInt("GoldenAcorns", PlayerPrefs.GetInt("GoldenAcorns") - goldCost);
                isGoldUnlocked = true;

                GameObject.Find("SkinManager").GetComponent<SkinManager>().SetRat(3);

                blueSelect.SetActive(false);
                pinkSelect.SetActive(false);
                goldSelect.SetActive(true);

                goldcostcanvas.SetActive(false);
            }
            else
            {
                Debug.Log("You can't afford this skin!");
            }
        }
        else
        {
            GameObject.Find("SkinManager").GetComponent<SkinManager>().SetRat(3);

            blueSelect.SetActive(false);
            pinkSelect.SetActive(false);
            goldSelect.SetActive(true);
        }
    }
}