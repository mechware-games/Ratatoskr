using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    float r, g, b, intensity;
    [SerializeField]
    float rmin, rmax, gmin, gmax, bmin, bmax;
    [SerializeField]
    float intensitymin, intensitymax;
    [SerializeField]
    List<Light> lights = new List<Light>();
    [SerializeField]
    float interval;
    [SerializeField]
    bool intensityflicker, colourflicker;

    public float Rmax { get => rmax; set => rmax = value; }
    public float Rmin { get => rmin; set => rmin = value; }
    public float Gmax { get => gmax; set => gmax = value; }
    public float Gmin { get => gmin; set => gmin = value; }
    public float Bmax { get => bmax; set => bmax = value; }
    public float Bmin { get => bmin; set => bmin = value; }
    public float Intensitymin { get => intensitymin; set => intensitymin = value; }
    public float Intensitymax { get => intensitymax; set => intensitymax = value; }

    void Start()
    {
        r = Random.Range(rmin, rmax);
        g = Random.Range(gmin, gmax);
        b = Random.Range(bmin, bmax);
        intensity = Random.Range(intensitymin, intensitymax);
        Color tempcolor = new Color(r, g, b);
        foreach (var L in lights) L.intensity = intensity;
        foreach (var C in lights) C.color = tempcolor;
        StartCoroutine(flicker());
    }

    IEnumerator flicker()
    {
        for (; ; )
        {
            r = Random.Range(rmin, rmax);
            g = Random.Range(gmin, gmax);
            b = Random.Range(bmin, bmax);
            intensity = Random.Range(intensitymin, intensitymax);
            Color tempcolor = new Color(r, g, b);
            if (intensityflicker)
            {
                foreach (var L in lights) L.intensity = intensity;
            }
            if (colourflicker)
            {
                foreach (var C in lights) C.color = tempcolor;
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
