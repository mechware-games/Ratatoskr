using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerSkins : MonoBehaviour
{
    private SkinnedMeshRenderer ratSkin;
    [SerializeField] private GameObject self;

    #region Skins
    public Texture normalSkin;
    public Texture goldSkin;
    public Texture blueSkin;
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
        if (ratSkin != null)
        {
            if (norm) SetActiveSkin(normalSkin);
            if (gold) SetActiveSkin(goldSkin);
            if (blue) SetActiveSkin(blueSkin);
        }
    }

    public void SetActiveSkin(Texture colour)
    {
        ratSkin.material.SetTexture("_BaseMap", colour);
    }

    public Material GetActiveSkin()
    {
        return ratSkin.material;
    }
}