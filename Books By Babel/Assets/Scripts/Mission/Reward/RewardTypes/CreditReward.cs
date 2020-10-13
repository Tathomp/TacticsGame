using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreditReward : Reward
{
    public int creditReward;

    public CreditReward(int creditReward)
    {
        this.creditReward = creditReward;
    }

    public override Reward Copy()
    {
        CreditReward cr = new CreditReward(creditReward);

        return cr;
    }

    public override void DistributeReward(BoardManager bm)
    {
        bm.party.Credits += CalculateFinalReward();
    }

    public override string RewardString()
    {
        return "Credits: " + CalculateFinalReward();
    }


    public int CalculateFinalReward()
    {
        return Mathf.RoundToInt( creditReward * Globals.campaign.campaignModifier.currency_bonus);
    }
}
