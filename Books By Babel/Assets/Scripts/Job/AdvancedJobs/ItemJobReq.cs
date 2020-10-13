using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemJobReq : JobReq
{
    public string itemID;

    public ItemJobReq(string ids)
    {
        this.itemID = ids;
    }

    public override JobReq Copy()
    {       
        return new ItemJobReq(itemID);
    }

    public override bool ReqMet(ActorData data)
    {
        List<ItemContainer> inventoryItems = Globals.campaign.currentparty.partyInvenotry.ItemSlots;


        foreach (ItemContainer item in inventoryItems)
        {
            if (item != null)

            {
                if (item.itemKey == itemID)
                {
                    return true;
                }
            }
        }


        List<ActorData> partyActors = Globals.campaign.currentparty.partyCharacter;

        foreach (ActorData ad in partyActors)
        {
            foreach (ItemContainer item in ad.inventory.ItemSlots)
            {
                if (item.itemKey == itemID)
                {
                    return true;
                }
            }

            foreach (EquipmentSlottt item in ad.equipment.GetAllEquipement())
            {
                if (item.itemKey == itemID)
                {
                    return true;

                }
            }
        }

        return false;
    }
}
