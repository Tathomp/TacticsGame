using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JobsDataContainer
{
    public SavedDatabase<Race> raceDB;
    public SavedDatabase<Job> JobDB;

    public JobsDataContainer()
    {
        raceDB = new SavedDatabase<Race>();
        JobDB = new SavedDatabase<Job>();
    }

    // Doesn't return a reference
    public List<Job> GetJobsAvaliableData(ActorData data)
    {
        List<Job> temp = new List<Job>();

        List<string> jkeys = raceDB.GetCopy(data.race).GetAllPossibleJobs();

        foreach (string jkey in jkeys)
        {
            temp.Add(JobDB.GetCopy(jkey));
        }

        return temp;
    }

    
}
