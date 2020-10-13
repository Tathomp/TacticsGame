using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlagInt : Flags
{
    private int goalValue;
    private int currentValue;
    private bool incrementor;

    public int init_turn = -1;

    public FlagInt(string id, int goalValue) : base(id)
    {
        this.goalValue = goalValue;
        this.incrementor = true;
        this.currentValue = 0;
    }

    public FlagInt(string id, int goalValue, bool incrementor, int currentValue) : base(id)
    {
        this.goalValue = goalValue;
        this.incrementor = incrementor;
        this.currentValue = currentValue;
    }


    public override bool CheckFlagStatus()
    {
        TickFlag();

        if(incrementor)
        {
            return currentValue >= goalValue;
        }
        else
        {
            return currentValue <= goalValue;

        }
    }

    public void TickFlag()
    {
        if (init_turn == -1)
        {
            init_turn = Globals.GetBoardManager().currentMission.currentTurn;
        }
        else
        {
            if (init_turn != Globals.GetBoardManager().currentMission.currentTurn)
            {


                if (incrementor)
                {
                    currentValue++;
                }
                else
                {
                    currentValue--;
                }
            }
        }
    }
}
