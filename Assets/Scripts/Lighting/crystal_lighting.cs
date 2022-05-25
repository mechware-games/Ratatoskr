using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystal_lighting : MonoBehaviour
{
    [SerializeField]
    float _initialIntensity;

    [SerializeField]
    float _resetTimerMax = 3;

    [SerializeField]
    float _resetTimerMin = 1;

    [SerializeField]
    float _lightingRandomTimerLength = 1f;
    float _lightingRandomTimer = 0;
    // Start is called before the first frame update

    Light _light;

    bool _countingUp = true;
    void Start()
    {
        _initialIntensity = transform.GetComponent<Light>().intensity;
        _light = transform.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_countingUp)
		{
            _lightingRandomTimer += Time.deltaTime;
            float lightingIntensity;
            lightingIntensity = Mathf.Lerp(1, _initialIntensity, _lightingRandomTimer / _lightingRandomTimerLength);
            _light.intensity = lightingIntensity;

            if (_lightingRandomTimer > _lightingRandomTimerLength)
            {
                _countingUp = false;
                _lightingRandomTimerLength = Random.Range(_resetTimerMin, _resetTimerMax);

            }
        }
		else
		{
            _lightingRandomTimer -= Time.deltaTime;
            float lightingIntensity;
            lightingIntensity = Mathf.Lerp(1, _initialIntensity, _lightingRandomTimer / _lightingRandomTimerLength);
            _light.intensity = lightingIntensity;

            if (_lightingRandomTimer < 0)
			{
                _countingUp = true;
			}
        }


    }
}
