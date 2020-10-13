using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AdvancedJob : Job
{
    public List<JobReq> JobRequirements;

    public AdvancedJob(string key, List<JobReq> reqs) : base(key)
    {
        JobRequirements = reqs;
    }


    public override bool JobUnlocked(ActorData data)
    {
        foreach (JobReq req in JobRequirements)
        {
            if (req.ReqMet(data) == false)
            {
                return false;
            }
        }

        return true;
    }

    public override DatabaseEntry Copy()
    {
        List<JobReq> reqs = new List<JobReq>();

        foreach (JobReq req in JobRequirements)
        {
            reqs.Add(req.Copy());
        }

        AdvancedJob temp = new AdvancedJob(key, reqs);

        temp.Name = Name;
        temp.Descript = Descript;
        temp.AbilitNames = AbilitNames;

        //finish copying stats and stuff
        temp.baseStats = (StatsContainer)baseStats.Copy();
        temp.statGrowth = (StatsContainer)statGrowth.Copy();

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