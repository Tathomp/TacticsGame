using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AddSkillEffectBuffEffect : BuffEffect
{

    public SkillEffect effectToApply;

    public AddSkillEffectBuffEffect(SkillEffect effectToApply)
    {
        this.effectToApply = effectToApply;
    }

    public override void OnDamageInflicted(Combat combat, AnimationData currentData)
    {
        if(ConditionsMet(combat.source, currentData.DestNode, currentData.skillUsed as Skill))
        {
            (currentData.skillUsed as Skill).effects.Add(effectToApply);
        }
    }

    public override BuffEffect Copy()
    {
        return new AddSkillEffectBuffEffect(effectToApply);
    }

    public override string PrintNameOfEffect()
    {
        return "Add Skill Effect";
    }
}
