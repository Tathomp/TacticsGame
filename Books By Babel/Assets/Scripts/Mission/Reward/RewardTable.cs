using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RewardTable
{

    public List<Reward> rewards;

    public RewardTable()
    {
        rewards = new List<Reward>();
    }

    public void AddReward(Reward r)
    {
        rewards.Add(r);
    }

    public void DistributeReward(BoardManager bm)
    {
        foreach (Reward r in rewards)
        {
            r.DistributeReward(bm);
        }
    }

    public RewardTable Copy()
    {
        RewardTable rt = new RewardTable();

        foreach (Reward r in rewards)
        {
            rt.AddReward((Reward)r.Copy());
        }

        return rt;
    }

    public override string ToString()
    {
        string s = "";

        foreach (Reward r in rewards)
        {
            s += r.RewardString() + "\n";
        }


        return s;
    }
}
