using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AddSkillCostBuffEffect : BuffEffect
{
    private SkillCost costToadd;

    public AddSkillCostBuffEffect(SkillCost costToadd)
    {
        this.costToadd = costToadd;
    }

    public override void ModSkillCost(Actor source, Skill skillToMod)
    {
        skillToMod.skillCost.Add(costToadd);   
    }

    public override BuffEffect Copy()
    {
        return new AddSkillCostBuffEffect(costToadd);
    }


    public override string PrintNameOfEffect()
    {
        return "Add Skill Cost";

    }
}
