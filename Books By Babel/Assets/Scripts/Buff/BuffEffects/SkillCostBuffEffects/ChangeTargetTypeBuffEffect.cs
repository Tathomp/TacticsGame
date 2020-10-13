using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChangeTargetTypeBuffEffect : BuffEffect
{
    public ITargetable newTargetType;

    public ChangeTargetTypeBuffEffect(ITargetable newTargetType)
    {
        this.newTargetType = newTargetType;
    }

    public override void ModSkillCost(Actor source, Skill skillToMod)
    {
        if(ConditionsMet(source, null, skillToMod))
        skillToMod.targetType = newTargetType;
    }


    public override BuffEffect Copy()
    {
        ChangeTargetTypeBuffEffect t = new ChangeTargetTypeBuffEffect(newTargetType);

        CopyConditionals(t);

        return t;
    }


    public override string PrintNameOfEffect()
    {
        return "Change Target Type";

    }
}
