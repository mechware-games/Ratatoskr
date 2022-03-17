using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    float r, g, b, intensity;
    [SerializeField]
    float rmin=1, rmax=2, gmin=1, gmax=2, bmin=1, bmax=2;
    [SerializeField]
    float intensitymin =0.001f, intensitymax= 0.002f;
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
            //r = Random.Range(rmin, rmax);
            //g = Random.Range(gmin, gmax);
            //b = Random.Range(bmin, bmax);
            //intensity = Random.Range(intensitymin, intensitymax);
            //Color tempcolor = new Color(r, g, b);
            if (intensityflicker)
            {
                foreach (var L in lights)
                {
                    intensity = Random.Range(intensitymin, intensitymax);
                    L.intensity = intensity;
                }
            }
            if (colourflicker)
            {
                foreach (var C in lights)
                {
                    r = Random.Range(rmin, rmax);
                    g = Random.Range(gmin, gmax);
                    b = Random.Range(bmin, bmax);
                    Color tempcolor = new Color(r, g, b);
                    C.color = tempcolor;
                }
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
