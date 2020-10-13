using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlagMissionTurn : Flags
{
    int turnToTrigger;

    public FlagMissionTurn(string id, int turn) : base(id)
    {
        turnToTrigger = turn;
    }

    public override bool CheckFlagStatus()
    {
        return turnToTrigger == Globals.GetBoardManager().currentMission.currentTurn;
    }
}
