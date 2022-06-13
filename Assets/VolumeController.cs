using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private string volumeParam = "Master";
    [SerializeField] private Slider slider;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private int multiplier = 30;

    private void Awake()
    {
        slider.onValueChanged.AddListener(GetChangedValue);
    }

    private void GetChangedValue(float value)
    {
        mixer.SetFloat(volumeParam, Mathf.Log10(value) * multiplier);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParam, slider.value);
    }

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volumeParam, slider.value);
    }
}