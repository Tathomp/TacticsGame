using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Discipline : DatabaseEntry
{
    public string Name;

    public List<string> TalenPool;

    public Discipline(string key) : base (key)
    {
        TalenPool = new List<string>();
    }

   

    public List<Skill> GetAvaliableSkills()
    {
        List<Skill> temp = new List<Skill>();

        foreach (string key in TalenPool)
        {
            Talent talent = Globals.campaign.contentLibrary.TalentDB.GetData(key);

            if(talent.HasSkill())
            {
                temp.Add(talent.SkillToLearn);
            }
        }

        return temp;
    }

    public override DatabaseEntry Copy()
    {
        Discipline temp = new Discipline(key);

        temp.Name = Name;

        foreach (string s in TalenPool)
        {
            temp.TalenPool.Add(s);
        }

        return temp;
    }
}
