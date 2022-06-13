using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] Slider mainSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    private void Update()
    {
        mainSlider.value = PlayerPrefs.GetFloat("Master", mainSlider.value);
        musicSlider.value = PlayerPrefs.GetFloat("Music", musicSlider.value);
        sfxSlider.value = PlayerPrefs.GetFloat("Sound Effects", sfxSlider.value);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt("Acorns", 0);
        PlayerPrefs.SetInt("GoldenAcorns", 0);

        PlayerPrefs.SetInt("BlueUnlocked", 0);
        PlayerPrefs.SetInt("PinkUnlocked", 0);
        PlayerPrefs.SetInt("GoldUnlocked", 0);
    }
}