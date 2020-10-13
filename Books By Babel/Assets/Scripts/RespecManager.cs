using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RespecManager 
{
    /// <summary>
    /// Currently not used at all
    /// If we need to 
    /// </summary>
    // Cost to respec
    //
    public RespecCost cost;


}

[System.Serializable]
public abstract class RespecCost
{
    public abstract void PayCost();
    public abstract string DisplayCost();
    public abstract bool CanPayCost();

    public void ResetTalents(string jobToReset, ActorData data)
    {
        //we'll have to remove any talent buffs as well
        //
        foreach (Talent t in data.JobDataState.GetAllLearnedTalents(jobToReset))
        {

            if(t.HasPassive())
            {
                data.buffContainer.RemoveBuff(data, t.PassiveBuff.GetKey());
            }

            data.JobDataState.AddJobPoints(jobToReset, t.skillCost);
            data.JobDataState.UnLearnSkill(jobToReset, t.GetKey());
        }
    }
}

[System.Serializable]
public class RespectCostNoCost : RespecCost
{
    public override bool CanPayCost()
    {
        return true;
    }

    public override string DisplayCost()
    {
        throw new System.NotImplementedException();
    }

    public override void PayCost()
    {
        
    }
}


[System.Serializable]
public class RespectCostCredits : RespecCost
{
    int cost;

    public RespectCostCredits(int cost)
    {
        this.cost = cost;
    }

    public override bool CanPayCost()
    {
        return Globals.campaign.currentparty.Credits >= cost;
    }

    public override string DisplayCost()
    {
        return "Cost: "+ cost;
    }

    public override void PayCost()
    {
        Globals.campaign.currentparty.Credits -= cost;

    }
}

[System.Serializable]
public class RespectItemCost : RespecCost
{
    public override bool CanPayCost()
    {
        throw new System.NotImplementedException();
    }

    public override string DisplayCost()
    {
        throw new System.NotImplementedException();
    }

    public override void PayCost()
    {
        throw new System.NotImplementedException();
    }
}
