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
    }

    private void Update()
    {
        Debug.Log("Acorns: " + PlayerPrefs.GetInt("Acorns"));
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
            }
            else
            {
                Debug.Log("You can't afford this skin!");
            }
        }
        else
        {
            GameObject.Find("SkinManager").GetComponent<SkinManager>().SetRat(2);
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
            }
            else
            {
                Debug.Log("You can't afford this skin!");
            }
        }
        else
        {
            GameObject.Find("SkinManager").GetComponent<SkinManager>().SetRat(1);
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
            }
            else
            {
                Debug.Log("You can't afford this skin!");
            }
        }
        else
        {
            GameObject.Find("SkinManager").GetComponent<SkinManager>().SetRat(3);
        }
    }
}