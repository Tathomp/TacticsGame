using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomTargetEditPanel : EditTargetPanel
{
    public TMP_InputField min, max;

    private RandomTargeting curTarget;




    public void InitDisplay(RandomTargeting t)
    {
        curTarget = t;

        min.text = t.min + "";
        max.text = t.maxTargets + "";
    }
}
