using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConeTargetingPanel : EditTargetPanel
{
    public TMP_InputField input;

    private ConeTarget curTarget;


    public void InitDisplay(ConeTarget t)
    {
        curTarget = t;
        input.text = t.raidus + "";
    }

    public override void Save()
    {
        base.Save();
        curTarget.raidus = int.Parse(input.text);
    }
}
