using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndComplexConditional : ComplextConditional
{
    public AndComplexConditional(List<Conditional> c) : base(c)
    {

    }

    public override bool ConditionMet(Actor actor, TileNode target, Skill skill)
    {
        foreach (Conditional item in conditonals)
        {
            if (item.ConditionMet(actor, target, skill) == false)
            {
                return false;
            }
        }

        return true;
    }

    public override Conditional Copy()
    {
        return new AndComplexConditional(conditonals);
    }

    public override string DisplayCondition(Actor actor, TileNode target, Skill skill)
    {
        throw new System.NotImplementedException();
    }
}
