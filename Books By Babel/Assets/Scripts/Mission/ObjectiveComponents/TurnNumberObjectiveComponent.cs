using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurnNumberObjectiveComponent : ObjectiveComponent
{
    public int turnsToPass; // turns to survive


    public TurnNumberObjectiveComponent(int turnsToPass)
    {
        this.turnsToPass = turnsToPass;
    }

    public override ObjectiveComponent Copy()
    {
        return new TurnNumberObjectiveComponent(turnsToPass);
    }

    public override bool ObjectiveComplete(BoardManager bm)
    {
        return bm.currentMission.currentTurn >= turnsToPass;

    }

    public override string PrintProgress()
    {
        return "Survive for " + Globals.GetBoardManager().currentMission.currentTurn + "/" + turnsToPass + " turns";
    }
}
