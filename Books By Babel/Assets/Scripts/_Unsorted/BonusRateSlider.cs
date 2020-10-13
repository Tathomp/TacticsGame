using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BonusRateSlider : MonoBehaviour
{

    //ui editor
    public Slider slider;
    public TMP_Text value;
    public int sigFigs = 1;

    public void InitValue(float f)
    {
        slider.value = f;

        UpdateValue();
    }

    public void UpdateValue()
    {
        value.text = GetValue() + "";
    }


    public float GetValue()
    {
        float v = slider.value;


        string s = v + "";

        if((slider.value + "").Contains("."))
        {
            s = s.Substring(0, sigFigs + 2);
            
        }


        return float.Parse(s);
    }
    


}
