using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlagHasItem : Flags
{
    private string itemID;
    private int amtRequired;

    public FlagHasItem(string id, string itemID, int amtRequired = 1) : base(id)
    {
        this.itemID = itemID;
        this.amtRequired = amtRequired;
    }

    public override bool CheckFlagStatus()
    {
        List<ItemContainer> inventoryItems = Globals.campaign.currentparty.partyInvenotry.ItemSlots;

        int count = 0;

        foreach (ItemContainer item in inventoryItems)
        {
            //if (item != null)
            {
                if (item.itemKey == itemID)
                {
                    count += item.currCapcity;
                }
            }
        }

        List<ActorData> partyActors = Globals.campaign.currentparty.partyCharacter;

        foreach (ActorData ad in partyActors)
        {
            foreach (ItemContainer item in ad.inventory.ItemSlots)
            {
                if(item.itemKey == itemID)
                {
                    count += item.currCapcity;
                }
            }

            foreach (EquipmentSlottt item in ad.equipment.GetAllEquipement())
            {
                if(item.itemKey == itemID)
                {
                    count++;
                }
            }
        }

        return count >= amtRequired;
    }
}
