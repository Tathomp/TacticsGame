using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TalentJobReq : JobReq
{
    public string JobKey { get; set; }
    public int TalentThreshold { get; set; }

    public TalentJobReq (string job_key, int talent_threshold)
    {
        JobKey = job_key;
        TalentThreshold = talent_threshold;
    }

    public override JobReq Copy()
    {
        return new TalentJobReq(JobKey, TalentThreshold);
    }

    public override bool ReqMet(ActorData data)
    {
        Job j = Globals.campaign.GetJobsData().JobDB.GetData(JobKey);

        int count = 0;

        foreach (Talent talent in j.GetTotalTalentPool())
        {
            // You could probabl yjust reduce this down to check the TalentLearned[key].COunt
            if(data.JobDataState.TalentsLearned[JobKey].Contains(talent.GetKey()))
            {
                count++;
            }
        }

        if(count >= TalentThreshold)
        {
            return true;
        }

        return false;
    }
}
