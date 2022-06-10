using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinStore : MonoBehaviour
{
    public bool isPinkUnlocked = true;
    public bool isBlueUnlocked;
    public bool isGoldUnlocked;

    [SerializeField] private int pinkCost = 0;
    [SerializeField] private int blueCost = 10;
    [SerializeField] private int goldCost = 25;


    private PlayerSkins ratSkin;

    private void Start()
    {
        ratSkin = GameObject.FindGameObjectWithTag("PlayerSkin").GetComponent<PlayerSkins>();
    }

    public void BuyBlue()
    {
        if (isBlueUnlocked == false)
        {
            if (globalValues.acorns >= blueCost)
            {
                globalValues.acorns -= blueCost;
                isBlueUnlocked = true;
            }
            else
            {
                // PURCHASE DENIED
            }
        }
        else
        {
            ratSkin.SetActiveSkin(ratSkin.blueSkin);
        }
    }
    public void BuyPink()
    {
        if (isPinkUnlocked == false)
        {
            if (globalValues.acorns >= pinkCost)
            {
                globalValues.acorns -= pinkCost;
                isGoldUnlocked = true;
            }
            else
            {
                
            }
        }
        else
        {
            ratSkin.SetActiveSkin(ratSkin.normalSkin);
        }
    }
    public void BuyGold()
    {
        if (isGoldUnlocked == false)
        {
            if(globalValues.acorns >= goldCost)
            {
                globalValues.acorns -= goldCost;
                isGoldUnlocked = true;
            }
            else
            {
                Debug.Log("You can't afford this skin!");
            }
        }
        else
        {
            ratSkin.SetActiveSkin(ratSkin.goldSkin);
        }
    }
}