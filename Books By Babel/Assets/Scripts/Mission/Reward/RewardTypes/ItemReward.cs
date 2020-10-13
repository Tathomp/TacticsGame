using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemReward : Reward
{
    List<string> itemRewards;

    public ItemReward(List<string> items)
    {
        itemRewards = items;
    }

    public override Reward Copy()
    {
        List<string> items = new List<string>();

        foreach (string i in itemRewards)
        {
            items.Add(i);
        }

        ItemReward ir = new ItemReward(items);

        return ir;
    }

    public override void DistributeReward(BoardManager bm)
    {
        foreach (string i in itemRewards)
        {
            bm.party.partyInvenotry.AddItem(i);

        }
    }

    public override string RewardString()
    {
        string r = "";

        foreach (string i in itemRewards)
        {
            r += Globals.campaign.GetItemCopy(i).Name;
        }

        return r;
    }
}
