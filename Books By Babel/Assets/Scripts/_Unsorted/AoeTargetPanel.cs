using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AoeTargetPanel : EditTargetPanel
{
    public TMP_InputField input;

    private AoeTarget curTarget;


    public void InitDisplay(AoeTarget t)
    {
        curTarget = t;
        input.text = t.radius + "";
    }

    public override void Save()
    {
        base.Save();
        curTarget.radius = int.Parse(input.text);
    }
}
