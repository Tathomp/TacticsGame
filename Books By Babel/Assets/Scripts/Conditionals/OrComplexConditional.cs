using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrComplexConditional : ComplextConditional
{

    public OrComplexConditional(List<Conditional> c) : base (c)
    {

    }

    public override bool ConditionMet(Actor actor, TileNode target, Skill skill)
    {
        foreach (Conditional item in conditonals)
        {
            if(item.ConditionMet(actor, target, skill) == true)
            {
                return true;
            }
        }

        return false;
    }

    public override Conditional Copy()
    {
        return new OrComplexConditional(conditonals);
    }

    public override string DisplayCondition(Actor actor, TileNode target, Skill skill)
    {
        throw new System.NotImplementedException();
    }
}
