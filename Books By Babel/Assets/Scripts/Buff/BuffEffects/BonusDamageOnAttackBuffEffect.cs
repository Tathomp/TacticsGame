using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BonusDamageOnAttack : BuffEffect
{
    private int amt;

    public BonusDamageOnAttack(int amt)
    {
        this.amt = amt;
        conditionalsRequired = new List<Conditional>();
    }

    public override void OnDamageInflicted(Combat combat, AnimationData currentData)
    {

        if(ConditionsMet(combat.source, currentData.DestNode, currentData.skillUsed as Skill))
        {
            IncreasePotencyOfSkillComponent componet = new IncreasePotencyOfSkillComponent(amt);

            componet.ApplyEffect(currentData.skillUsed as Skill);
        }

    }

    public override BuffEffect Copy()
    {

        BonusDamageOnAttack a = new BonusDamageOnAttack(amt);

        CopyConditionals(a);

        return a;
    }

    public override string PrintNameOfEffect()
    {
        return "Bonus Damage";

    }
}
