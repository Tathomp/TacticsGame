using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillCostItemEquipped : SkillCost
{
    //specfic item
    public string itemKey;

    //Item category needed
    public ItemType typeNeed;

    //
    public bool searchTag; // we need 
    public bool searchEquipment;


    public SkillCostItemEquipped(string itemKey, bool consumeReq, bool searchTag, bool searchEquipment) : base (consumeReq)
    {
        this.itemKey = itemKey;
        this.searchTag = searchTag;
        this.searchEquipment = searchEquipment;
    }

    public SkillCostItemEquipped(ItemType type, bool consumeReq, bool searchTag, bool searchEquipment) : base (consumeReq)
    {
        itemKey = "";
        typeNeed = type;
        this.searchTag = searchTag;
        this.searchEquipment = searchEquipment;

    }

    public override bool CanPayCost(Actor actor)
    {
      

        if (itemKey == "") //Then we're checking the item category
        {
            if (searchEquipment)
            {
                //search the equipment for a particular item type
                foreach (EquipmentSlottt equipmentSlottt in actor.actorData.equipment.GetAllEquipement())
                {
                    if (
                    Globals.campaign.GetItemData(equipmentSlottt.itemKey).itemType == typeNeed)
                    {
                        return true;
                    }
                }
            }
            else
            {
                //search inventory for a particular item  type
            }
        }
        else if (false) //right now we dont have tags on items
        {
            if (searchEquipment)
            {
                
            }
            else
            {
                //search inventory for a particular item  tag
            }
        }
        else
        {
            //we should be looking for a particular item key here
            if (searchEquipment)
            {
                foreach (EquipmentSlottt equipmentSlottt in actor.actorData.equipment.GetAllEquipement())
                {
                    if (
                    equipmentSlottt.itemKey == itemKey)
                    {
                        return true;
                    }
                }
            }
            else
            {
                //what is this
                //search inventory?
            }
        }


        return false;



    }

    public override SkillCost Copy()
    {
        if (itemKey == "")
        {
            return new SkillCostItemEquipped(typeNeed, consumeReq, searchTag, searchEquipment);
        }
        else
        {
            return new SkillCostItemEquipped(itemKey, consumeReq, searchTag, searchEquipment);
        }
    }


    public override void PayCost(Actor actor)
    {
        if (consumeReq)
        {
            if (itemKey == "") //Then we're checking the item category
            {
                if (searchEquipment)
                {
                    //search the equipment for a particular item type
                }
                else
                {
                    //search inventory for a particular item  type
                }
            }
            else if (searchTag)
            {
                if (searchEquipment)
                {
                    //search the equipment for a particular item tag
                }
                else
                {
                    //search inventory for a particular item  tag
                }
            }
            else
            {
                //we should be looking for a particular item key here
                if (searchEquipment)
                {

                    actor.actorData.equipment.UnequipItem(
                        actor.actorData,
                        itemKey);
                }
                else
                {
                    //remove from inventory

                }
            }
        }
    }

    public override string PrintCost()
    {
        return "Requires: " + itemKey + " to be equipped";
    }
}
