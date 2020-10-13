using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockTargetPanel : EditTargetPanel
{
    public TMP_InputField w,h;

    private BlockTarget curTarget;


    public void InitDisplay(BlockTarget t)
    {
        curTarget = t;
        w.text = t.width + "";
        h.text = t.height + "";
    }

    public override void Save()
    {
        base.Save();
        curTarget.width = int.Parse(w.text);
        curTarget.height = int.Parse(h.text);
    }
}
