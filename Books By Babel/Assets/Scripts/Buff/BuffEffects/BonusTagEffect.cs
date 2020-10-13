using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
//Actually just adds a tag
public class BonusTagToSkillBuffEffect : BuffEffect
{
    public AddTagToSkillBaseComponent tagToAdd;


    public BonusTagToSkillBuffEffect(string tag) 
    {
        tagToAdd = new AddTagToSkillBaseComponent(tag);

        conditionalsRequired = new List<Conditional>();
    }

    public override BuffEffect Copy()
    {
        return new BonusTagToSkillBuffEffect(tagToAdd.tagToAdd);
    }

    public override string GetHotbarDescription()
    {
        return "Adds the effect " + tagToAdd; 
    }


    public override void OnDamageInflicted(Combat combat, AnimationData currentData)
    {
        foreach (Conditional conditional in conditionalsRequired)
        {
            if(conditional.ConditionMet(combat.source, currentData.DestNode, currentData.skillUsed as Skill) == false)
            {
                return; //Don't apply effect
            }
        }



        tagToAdd.AddTagToSkill(currentData.skillUsed as Skill);
    }


    public override string PrintNameOfEffect()
    {
        return "Add Tag to Skill";

    }
}
