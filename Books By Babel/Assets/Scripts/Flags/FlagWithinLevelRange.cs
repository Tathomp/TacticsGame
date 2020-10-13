using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlagWithinLevelRange : Flags
{
    int maxlevel, minLevel;

    public FlagWithinLevelRange(string id, int min, int max) : base(id)
    {
        maxlevel = max;
        minLevel = min;
    }

    public override bool CheckFlagStatus()
    {
        int avgLevel = Globals.campaign.currentparty.GetAvgLevel();

        return avgLevel >= minLevel && avgLevel <= maxlevel;
    }
}
