using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkins : MonoBehaviour
{
    private SkinnedMeshRenderer ratSkin;

    #region Skins
    public Texture normalSkin;
    public Texture goldSkin;
    public Texture blueSkin;
    #endregion
    #region Hats

    #endregion

    public bool norm;
    public bool gold;
    public bool blue;

    private void Start()
    {
        ratSkin = GetComponent<SkinnedMeshRenderer>();
    }
    private void Update()
    {
        if (norm) SetActiveSkin(normalSkin);
        if (gold) SetActiveSkin(goldSkin);
        if (blue) SetActiveSkin(blueSkin);
    }

    public void SetActiveSkin(Texture colour)
    {
        ratSkin.material.SetTexture("_BaseMap", colour);
    }
    public Material GetActiveSkin()
    {
        return ratSkin.material;
    }
    public void SetActiveHat(GameObject hat)
    {

    }
    public GameObject GetActiveHat()
    {
        return null;
    }
}