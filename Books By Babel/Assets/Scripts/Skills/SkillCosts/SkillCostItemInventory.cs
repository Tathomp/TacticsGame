using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillCostItemInventory : SkillCost
{
    public string itemKey;

    public SkillCostItemInventory(string itemKey, bool consumeReq) : base(consumeReq)
    {
        this.itemKey = itemKey;
    }

    public override bool CanPayCost(Actor actor)
    {
        foreach (ItemContainer key in actor.actorData.inventory.ItemSlots)
        {
            if(key.itemKey == itemKey)
            {
                return true;
            }
        }

        return false;
    }

    public override SkillCost Copy()
    {
        return new SkillCostItemInventory(itemKey, consumeReq);
    }

    public override void PayCost(Actor actor)
    {
        if(consumeReq)
        {
            actor.actorData.inventory.UseItem(itemKey);
        }
    }

    public override string PrintCost()
    {
        return "Item needed in inventory: " + itemKey;
    }
}
