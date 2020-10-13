using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JPReward : Reward
{

    int rewardAmt;

    public JPReward(int amt)
    {
        this.rewardAmt = amt;
    }

    public override Reward Copy()
    {
        return new JPReward(rewardAmt);
    }

    public override void DistributeReward(BoardManager bm)
    {
        List<ActorData> party = bm.party.GetSelectedAndAliveActors();

        foreach (ActorData actor in party)
        {
            actor.JobDataState.JobPoints[actor.primaryJob] += rewardAmt;
        }
    }

    public override string RewardString()
    {
        return "JP Awarded: " + FinalXPCalculation();
    }

    public int FinalXPCalculation()
    {
        return Mathf.RoundToInt(rewardAmt * Globals.campaign.campaignModifier.jp_bonus);

    }
}
