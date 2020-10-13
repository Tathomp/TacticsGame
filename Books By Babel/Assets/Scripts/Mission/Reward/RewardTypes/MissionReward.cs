using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionReward : Reward
{
    string missionKey;
    string flagkey;

    public MissionReward(string missionKey, string flagkey)
    {
        this.missionKey = missionKey;
        this.flagkey = flagkey;
    }

    public override Reward Copy()
    {
        MissionReward r = new MissionReward(missionKey, flagkey);

        return r;
    }

    public override void DistributeReward(BoardManager bm)
    {
        //This was here but doesn't appear to do anything?
        //Mission m = Globals.campaign.contentLibrary.missionHandler.GetEntry(missionKey);

        ((FlagBool)(bm.campaign.GlobalFlags[missionKey])).ChangeFlag(true);
    }

    public override string RewardString()
    {
        string r = "Mission: " + missionKey;

        return r;
    }
}
