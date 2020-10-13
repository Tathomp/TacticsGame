using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionCompleteJobReq : JobReq
{
    List<string> missionIDsRequired;

    public MissionCompleteJobReq()
    {
        missionIDsRequired = new List<string>();
    }

    public MissionCompleteJobReq(List<string> ids)
    {
        missionIDsRequired = ids;
    }

    public override JobReq Copy()
    {
        return new MissionCompleteJobReq(missionIDsRequired);
    }

    public override bool ReqMet(ActorData data)
    {
        foreach (string id in missionIDsRequired)
        {
           if(Globals.campaign.HasMissionBeenCompleted(id) == false)
           {
                return false;
           }
        }

        return true;
    }
}
