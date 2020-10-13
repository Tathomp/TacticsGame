using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePotencyOfSkillComponent
{
    public int amt;

    public IncreasePotencyOfSkillComponent(int amt)
    {
        this.amt = amt;
    }

    public void ApplyEffect(Skill skill)
    {
        for (int i = 0; i < skill.effects.Count; i++)
        {
            if (skill.effects[i] is ChangeHealthEffect)
            {
                ChangeHealthEffect e = skill.effects[i] as ChangeHealthEffect;

                foreach (DamageObject d in e.dmgObjData)
                {
                    d.baseValue += amt;
                }
            }
        }
    }
}