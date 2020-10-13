using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnNumberFailState : TurnNumberObjectiveComponent
{
    public TurnNumberFailState(int turnsToPass) : base(turnsToPass)
    {

    }


    public override string PrintProgress()
    {
        return "Win in " + turnsToPass + " or less";
    }
}
