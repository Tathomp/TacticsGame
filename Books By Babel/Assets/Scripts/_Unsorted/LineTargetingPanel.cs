using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTargetingPanel : EditTargetPanel
{
    private LineTarget curTarget;


    public void InitDisplay(LineTarget t)
    {
        curTarget = t;
    }
}
