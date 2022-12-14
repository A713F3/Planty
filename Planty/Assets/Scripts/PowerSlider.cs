using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSlider : MonoBehaviour
{
    public float velo;
    private float max_velo = 5f;
    private float min_velo = 0f;

    private float step = 2f;
    public int add = 1;

    public Slider power_slider;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        power_slider.value = velo;
        
        velo += add * step * Time.deltaTime;

        if (velo > max_velo) add = -1;
        if (velo < min_velo) add = 1;
    }
}
