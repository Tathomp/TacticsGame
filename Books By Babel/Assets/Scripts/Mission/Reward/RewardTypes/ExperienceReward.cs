using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExperienceReward : Reward
{
    int xpAmt;


    public ExperienceReward(int xp)
    {
        xpAmt = xp;
    }


    public override Reward Copy()
    {
        ExperienceReward r = new ExperienceReward(xpAmt);

        return r;
    }


    public override void DistributeReward(BoardManager bm)
    {
        List<ActorData> party = bm.party.GetSelectedAndAliveActors();

        foreach (ActorData a in party)
        {
            a.AddXp(FinalXPCalculation());           
        }
        
    }

    
    public override string RewardString()
    {
        return "Experience: " + FinalXPCalculation();
    }

    public int FinalXPCalculation()
    {
        return Mathf.RoundToInt(xpAmt * Globals.campaign.campaignModifier.xp_bonus);

    }
}
