using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Talent : DatabaseEntry
{
    public string TalentNodename { get; protected set; }
    public Buff PassiveBuff { get; protected set; }
    public Skill SkillToLearn { get; protected set; }
    public LearnSkillBuffEffect jobeEffect;


    public string descript;
    public int skillCost; //cost to learn this skill

    public Talent(string key, string talentName) :base(key)
    {

        TalentNodename = talentName;
    }

    public string GetDescription()
    {

        string s = "";

        if(HasSkill())
            {

            s = SkillToLearn.GetHotbarDescription();
        }

        if(HasPassive())
        {
            s += "\n" + PassiveBuff.GetHotbarDescription();
        }

        return s;
    }

    public Talent(string key, string talentName, Buff passive) : base(key)
    {
        TalentNodename = talentName;
        PassiveBuff = passive;
        SkillToLearn = null;
    }

    public Talent(string key, string talentName, Skill skill) : base(key)
    {
        TalentNodename = talentName;
        PassiveBuff = null;
        SkillToLearn = skill;
    }

    public Talent(string key, string talentName, Buff passive, Skill skill) : base(key)
    {
        TalentNodename = talentName;
        PassiveBuff = passive;
        SkillToLearn = skill;
    }


    public void LearnTalent(ActorData actor)
    {
        throw new System.NotImplementedException();

    }

    public override DatabaseEntry Copy()
    {
        Talent cp = new Talent(key, TalentNodename, PassiveBuff, SkillToLearn);
        cp.jobeEffect = jobeEffect;
        return cp;
    }

    public bool HasSkill()
    {
        return SkillToLearn != null;
    }

    public bool HasPassive()
    {
        return PassiveBuff != null;
    }

    public bool HasLearnAnotherSkill()
    {
        return jobeEffect != null;
    }
}
