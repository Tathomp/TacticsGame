using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Race : DatabaseEntry
{
    public string Name, descript;
    public StatsContainer statGrowth, baseStats;

    public List<string> avaliablejobs, raceTags;

                

    public Race(string key) : base(key)
    {
        statGrowth = new StatsContainer();
        baseStats = new StatsContainer();

        avaliablejobs = new List<string>();
        raceTags = new List<string>();
        descript = "";
    }

    public List<Job> GetUnlockedJobs(ActorData actor)
    {
        List<Job> j = new List<Job>();

        j.Add(Globals.campaign.GetJobsData().JobDB.GetData(key));

        foreach (string job in avaliablejobs)
        {
            Job temp = Globals.campaign.GetJobsData().JobDB.GetData(job);

            if(temp.JobUnlocked(actor))
            {
                j.Add(temp.Copy() as Job);
            }
        }

        return j;
    }

    public List<string> GetAllPossibleJobs()
    {
        return avaliablejobs;
    }

    /*
    public void CheckAdvanceJobs(ActorData data)
    {

        foreach (Job job in avaliablejobs)
        {
            if(job is AdvancedJob)
            {
                AdvancedJob j = job as AdvancedJob;
                if (j.UnlockJob(data))
                {
                    j.Unlocked = true;
                }
            }

        }
    }
    */

    public override DatabaseEntry Copy()
    {
        Race temp = new Race(key);
        temp.Name = Name;
        temp.descript = descript;

        temp.statGrowth = (StatsContainer) statGrowth.Copy();
        temp.baseStats = (StatsContainer) baseStats.Copy();

        foreach (string j in avaliablejobs)
        {
            temp.avaliablejobs.Add(j);
        }

        foreach (string t in raceTags)
        {
            temp.raceTags.Add(t);
        }

        return temp;
    }
}
