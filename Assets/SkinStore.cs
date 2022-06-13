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
        Debug.Log("Golden Acorns: " + PlayerPrefs.GetInt("GoldenAcorns"));

        Debug.Log("Is Blue Unlocked?: " + PlayerPrefs.GetInt("BlueUnlocked"));
        Debug.Log("Is Pink Unlocked?: " + PlayerPrefs.GetInt("PinkUnlocked"));
        Debug.Log("Is Gold Unlocked?: " + PlayerPrefs.GetInt("GoldUnlocked"));

        if(PlayerPrefs.GetInt("BlueUnlocked") == 1) { bluecostcanvas.SetActive(false); } else { bluecostcanvas.SetActive(true); }
        if(PlayerPrefs.GetInt("GoldUnlocked") == 1) { goldcostcanvas.SetActive(false); } else { goldcostcanvas.SetActive(true); }
    }

    public void BuyBlue()
    {
        if (PlayerPrefs.GetInt("BlueUnlocked") != 1)
        {
            if (PlayerPrefs.GetInt("Acorns") >= blueCost)
            {
                PlayerPrefs.SetInt("Acorns", PlayerPrefs.GetInt("Acorns") - blueCost);
                PlayerPrefs.SetInt("BlueUnlocked", 1); // isBlueUnlocked = true;

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
        if (PlayerPrefs.GetInt("PinkUnlocked") != 1)
        {
            if (PlayerPrefs.GetInt("Acorns") >= pinkCost)
            {
                PlayerPrefs.SetInt("Acorns", PlayerPrefs.GetInt("Acorns") - pinkCost);
                PlayerPrefs.SetInt("PinkUnlocked", 1); // isPinkUnlocked = true;

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
        if (PlayerPrefs.GetInt("GoldUnlocked") != 1)
        {
            if(PlayerPrefs.GetInt("GoldenAcorns") >= goldCost)
            {
                PlayerPrefs.SetInt("GoldenAcorns", PlayerPrefs.GetInt("GoldenAcorns") - goldCost);
                PlayerPrefs.SetInt("GoldUnlocked", 1);  //isGoldUnlocked = true;

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