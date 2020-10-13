using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomRollConditional : Conditional
{
    //if the roll is less than or equal to this, the condition is met
    //
    public int threshold;


    public RandomRollConditional(int threshold)
    {
        this.threshold = threshold;
    }


    public override bool ConditionMet(Actor actor, TileNode target, Skill skill)
    {
        return threshold >= Globals.GetRandomNumber(0, 100);
    }


    public override Conditional Copy()
    {
        return new RandomRollConditional(threshold);
    }

    public override string DisplayCondition(Actor actor, TileNode target, Skill skill)
    {
        throw new System.NotImplementedException();
    }
}
