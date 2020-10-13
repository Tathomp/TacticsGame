using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LearnSkillBuffEffect : BuffEffect
{
    public string jobToChange, skillToLearn;

    public LearnSkillBuffEffect(string jobToChange, string skillToLearn)
    {
        this.jobToChange = jobToChange;
        this.skillToLearn = skillToLearn;
    }

    public override void OnApply(ActorData actor, ActorData source)
    {
        actor.JobDataState.LearnSkill(jobToChange, skillToLearn);
    }

    public override BuffEffect Copy()
    {
        return new LearnSkillBuffEffect(jobToChange, skillToLearn);
    }

    public override string PrintNameOfEffect()
    {
        return "Learn Skill";

    }
}
