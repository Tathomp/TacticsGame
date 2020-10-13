using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Job : DatabaseEntry
{
    public string Name;

    public string AbilitNames, descript;
    public string Descript;

    public StatsContainer statGrowth, baseStats;


    public List<Discipline> avalibleDisciples;
    public List<ItemType> allowedItemTypes;

    public string jobportrait;
    public string jobanimcontroller;

    public Job(string key) : base(key)
    {
        statGrowth = new StatsContainer();
        baseStats = new StatsContainer();

        avalibleDisciples = new List<Discipline>();
        allowedItemTypes = new List<ItemType>();
        Descript = "";

        jobportrait = "";
        jobanimcontroller = "";

    }

    public List<string> GetTalentKeys()
    {
        List<string> temp = new List<string>();

        foreach (Talent talent in GetTotalTalentPool())
        {
            temp.Add(talent.GetKey());
        }

        return temp;
    }

    public List<Talent> GetTotalTalentPool()
    {
        List<Talent> tmep = new List<Talent>();

        foreach (Discipline discipline in avalibleDisciples)
        {
            foreach (string talent in discipline.TalenPool)
            {
                tmep.Add(Globals.campaign.contentLibrary.TalentDB.GetData(talent));
            }
        }

        return tmep;
    }


    public List<Skill> GetAvaliableSkills()
    {
        List<Skill> temp = new List<Skill>();

        foreach (Discipline disc in avalibleDisciples)
        {
            foreach (Skill skill in disc.GetAvaliableSkills())
            {
                temp.Add(skill);
            }
        }


        return temp;
    }


    public virtual bool JobUnlocked(ActorData data)
    {
        return true;
    }


    public override DatabaseEntry Copy()
    {
        Job temp = new Job(key);

        temp.Name = Name;
        temp.Descript = Descript;
        temp.AbilitNames = AbilitNames;

        temp.jobanimcontroller = jobanimcontroller;
        temp.jobportrait = jobportrait;

        //finish copying stats and stuff
        temp.baseStats = (StatsContainer) baseStats.Copy();
        temp.statGrowth = (StatsContainer) statGrowth.Copy();

        foreach (Discipline d in avalibleDisciples)
        {
            temp.avalibleDisciples.Add((Discipline)d.Copy());
        }

        foreach (ItemType t in allowedItemTypes)
        {
            
            temp.allowedItemTypes.Add(t);
        }

        temp.descript = descript;

        return temp;
    }
}
