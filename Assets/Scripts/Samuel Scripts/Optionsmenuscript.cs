using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.EventSystems;

public class Optionsmenuscript : MonoBehaviour
{
    Resolution[] resolutions;
    public GameObject OptionsMenu;
    public GameObject MainMenu;
    public GameObject PauseMenu;

    public TMP_Dropdown textureDropdown;
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown Resolution;
    public TMP_Dropdown aaDropdown;

    public Slider Audio;
    public AudioMixer MasterMix;

    [SerializeField] private GameObject FirstButtonMain;

    private void Start()
    {
        resolutions = Screen.resolutions;

        Resolution.ClearOptions();

        List<string> options = new List<string>();

        int currentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }    

        Resolution.AddOptions(options);
        Resolution.value = currentResolution;
        Resolution.RefreshShownValue();
    }

    public void SetAntiAliasing(int aaIndex)
    {
        QualitySettings.antiAliasing = aaIndex;
        //qualityDropdown.value = 6;
    }
    public void SetTextureQuality(int textureIndex)
    {
        //for some reason removing the useless int textureIndex from above breaks this so please leave it in - unless you can fix the issue
        QualitySettings.masterTextureLimit = textureDropdown.value;
        //qualityDropdown.value = 6;
    }
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void Back()
    {
        OptionsMenu.SetActive(false);
        MainMenu.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(FirstButtonMain, null);
    }
    public void BackPause()
    {
        OptionsMenu.SetActive(false);
        PauseMenu.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(FirstButtonMain, null);
    }
    public void BackLevelSelect()
    {

    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void DebugButton()
    {
        Debug.Log("texture quality is " + QualitySettings.masterTextureLimit);
        Debug.Log(textureDropdown.value);
    }

    //public void MasterVolume(float masterLevel)
    //{
    //    MasterMix.SetFloat("Master", Mathf.Log10(masterLevel) * 20);
    //}
    //
    //public void SetMusicLvl (float musicLvl)
    //{
    //    MasterMix.SetFloat("Music", Mathf.Log10(musicLvl) * 20);
    //}
    //public void SetSFXLevel(float sfxlevel)
    //{
    //    MasterMix.SetFloat("Sound Effects", Mathf.Log10(sfxlevel) * 20);
    //}
    //public void SetVoiceLevel(float voicelevel)
    //{
    //    MasterMix.SetFloat("Voice", Mathf.Log10(voicelevel) * 20);
    //}
}