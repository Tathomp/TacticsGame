using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Equipment 
{


    //////
    ///TODO I kinda want to make a dictionary where the name of the slot is the key, the item equipped is the value
    ///     we should have another dict with that's name with the valid slot types
    ///      should slot types just be a string too? probably if we're going down this dark paths
    //////
    EquipmentSlottt HeadItem;
    EquipmentSlottt PrimaryWeapon;
    EquipmentSlottt SecondaryWeapon;

    EquipmentSlottt Body;
    EquipmentSlottt Legs;
    EquipmentSlottt Feet;

    EquipmentSlottt hands;
    EquipmentSlottt Access1;

    public Equipment()
    {
        HeadItem = new EquipmentSlottt("", "Head");
        PrimaryWeapon = new EquipmentSlottt("","Main Hand");
        SecondaryWeapon = new EquipmentSlottt("","Off Hand");

        Body = new EquipmentSlottt("","Body");
        Legs = new EquipmentSlottt("", "Legs");
        Feet = new EquipmentSlottt("", "Feet");

        hands = new EquipmentSlottt("", "Hands");
        Access1 = new EquipmentSlottt("", "Accessory");

    }


    public Equipment Copy()
    {     

        Equipment e = new Equipment();
        e.HeadItem = HeadItem.Copy() as EquipmentSlottt;
        e.PrimaryWeapon = PrimaryWeapon.Copy() as EquipmentSlottt; ;
        e.SecondaryWeapon = SecondaryWeapon.Copy() as EquipmentSlottt; ;
        e.Body = Body.Copy() as EquipmentSlottt; ;
        e.Legs = Legs.Copy() as EquipmentSlottt; ;
        e.Feet = Feet.Copy() as EquipmentSlottt; ;
        e.hands = hands.Copy() as EquipmentSlottt; ;
        e.Access1 = Access1.Copy() as EquipmentSlottt; ;
        e.Body = Body;


        return e;
    }

    public void CorrectStats(Item currItem, Item prevItem, ActorData actorData)
    {
     

        if (prevItem != null)
        {
            actorData.currentStatCollection.RemoveStats(prevItem.GetEquippedItem().GetBonusStats());
            actorData.maxStatCollection.RemoveStats(prevItem.GetEquippedItem().GetBonusStats());
        }


        if (currItem != null)
        {

            actorData.currentStatCollection.AddStats(currItem.GetEquippedItem().GetBonusStats());
            actorData.maxStatCollection.AddStats(currItem.GetEquippedItem().GetBonusStats());
        }


    }

    private string EquipItem(Item newItem, ActorData  actorData, EquipmentSlottt slot)
    {
        Item prevItem = null;

        if (slot.itemKey != "")
        {
            prevItem = Globals.campaign.GetItemCopy(slot.itemKey);

        }

        

        slot.itemKey = newItem.GetKey();


        CorrectStats(newItem, prevItem, actorData);

        if (prevItem == null)
        {
            return "";
        }


        return prevItem.GetKey();
    }

    public string UneqipItem(ActorData data, EquipmentSlot slot)
    {
        string item = "";

        switch (slot)
        {
            case EquipmentSlot.Head:
                if(HasHeadItem())
                {
                    item = HeadItem.itemKey;
                }
                break;
            case EquipmentSlot.MainHand:
                if (HasWep())
                {
                    item = PrimaryWeapon.itemKey;
                }
                break;
            case EquipmentSlot.OffHand:
                if (HasSecondary())
                {
                    item = SecondaryWeapon.itemKey;
                }
                break;
            case EquipmentSlot.BodyArmor:
                if (HasBody())
                {
                    item = Body.itemKey;
                }
                break;
            case EquipmentSlot.Gloves:
                if (HasGloves())
                {
                    item = hands.itemKey;
                }
                break;
            case EquipmentSlot.Boots:
                if (HasFeet())
                {
                    item = Feet.itemKey;
                }
                break;
            case EquipmentSlot.Accessory:
                if (HassAccess())
                {
                    item = Access1.itemKey;
                }
                break;
        }

        UnequipItem(data, item);

        return item;
    }

    public void UnequipItem(ActorData data, EquipmentSlottt slot)
    {
        UnequipItem(data, slot.itemKey);
    }

    public void UnequipItem(ActorData data, string itemToRemove)
    {
        if(itemToRemove == "")
        {
            return;
        }

        Item t = Globals.campaign.GetItemCopy(itemToRemove);

        CorrectStats(null, t, data);

        
        if (HeadItem.itemKey == itemToRemove)
        {
            HeadItem.itemKey = "";
        }
        else if( PrimaryWeapon.itemKey == itemToRemove)
        {
            PrimaryWeapon.itemKey = "";
        }
        else if (SecondaryWeapon.itemKey == itemToRemove)
        {
            SecondaryWeapon.itemKey = "";
        }
        else if (Body.itemKey == itemToRemove)
        {
            Body.itemKey = "";
        }
        else if (Legs.itemKey == itemToRemove)
        {
            Legs.itemKey = "";
        }
        else if (Feet.itemKey == itemToRemove)
        {
            Feet.itemKey = "";
        }
        else if (hands.itemKey == itemToRemove)
        {
            hands.itemKey = "";
        }
        else if (Access1.itemKey == itemToRemove)
        {
            Access1.itemKey = "";
        }
    }

    public string EquipItem(Item newItem, ActorData actorData)
    {
        //we still need to add the item back to the inventory
        //

        if( newItem.GetEquippedItem().validSlots.Contains(EquipmentSlot.Head))
        {
            return EquipItem(newItem, actorData, HeadItem);
        }
        else if (newItem.GetEquippedItem().validSlots.Contains(EquipmentSlot.MainHand))
        {
            return EquipItem(newItem, actorData, PrimaryWeapon);
        }
        else if (newItem.GetEquippedItem().validSlots.Contains(EquipmentSlot.OffHand))
        {
            return EquipItem(newItem, actorData, SecondaryWeapon);
        }
        else if (newItem.GetEquippedItem().validSlots.Contains(EquipmentSlot.BodyArmor))
        {
            return EquipItem(newItem, actorData, Body);
        }
        else if (newItem.GetEquippedItem().validSlots.Contains(EquipmentSlot.Gloves))
        {
            return EquipItem(newItem, actorData, hands);
        }
        else if (newItem.GetEquippedItem().validSlots.Contains(EquipmentSlot.Boots))
        {
            return EquipItem(newItem, actorData, Feet);
        }
        else if (newItem.GetEquippedItem().validSlots.Contains(EquipmentSlot.Accessory))
        {
            return EquipItem(newItem, actorData, Access1);
        }

        return "";
    }

    public string GetItemEquipped(EquipmentSlot slot)
    {
        switch (slot)
        {
            case EquipmentSlot.Head:
                {
                    return HeadItem.itemKey;
                }
            case EquipmentSlot.Accessory:
                {
                    return Access1.itemKey;
                }
            case EquipmentSlot.MainHand:
                {
                    return PrimaryWeapon.itemKey;
                }
            case EquipmentSlot.OffHand:
                {
                    return SecondaryWeapon.itemKey;
                }
            case EquipmentSlot.BodyArmor:
                {
                    return Body.itemKey;
                }
            case EquipmentSlot.Gloves:
                {
                    return hands.itemKey;
                }
            case EquipmentSlot.Boots:
                {
                    return Feet.itemKey;
                }
            default:
                {
                    return "";
                }
        }

    }


    #region Item Getters
    public string GetHeadItem()
    {
        return HeadItem.itemKey;
    }

    public string GetPrimaryWeapon()
    {
        return PrimaryWeapon.itemKey;
    }

    public string GetsecondaryWeapon()
    {
        return SecondaryWeapon.itemKey;
    }

    public string GetBodyItem()
    {
        return Body.itemKey;
    }
    public string GetLegItem()
    {
        return Legs.itemKey;
    }

    public string GetFeetItem()
    {
        return Feet.itemKey;
    }
    public string GetAcces1()
    {
        return Access1.itemKey;
    }

#endregion



    #region Item Checks
    public bool HasHeadItem()
    {
        return HeadItem.itemKey != "";
    }


    public bool HasWep()
    {
        return PrimaryWeapon.itemKey != "";
    }

    public bool HasSecondary()
    {
        return SecondaryWeapon.itemKey != "";
    }

    public bool HasFeet()
    {
        return Feet.itemKey != "";
    }

    public bool HasGloves()
    {
        return hands.itemKey != "";
    }

    public bool HasBody()
    {
        return Body.itemKey != "";
    }

    public bool HasLegs()
    {
        return Legs.itemKey != "";
    }

    public bool HassAccess()
    {
        return Access1.itemKey != "";
    }


    #endregion


    public List<EquipmentSlottt> GetAllEquipement()
    {
        List<EquipmentSlottt> items = new List<EquipmentSlottt>();

       if(HeadItem.itemKey != "")
        {
            items.Add(HeadItem);
        }
        if (PrimaryWeapon.itemKey != "")
        {
            items.Add(PrimaryWeapon);
        }
        if (SecondaryWeapon.itemKey != "")
        {
            items.Add(SecondaryWeapon);
        }
        if (Body.itemKey != "")
        {
            items.Add(Body);
        }
        if (Legs.itemKey != "")
        {
            items.Add(Legs);
        }
        if (Feet.itemKey != "")
        {
            items.Add(Feet);
        }
        if (hands.itemKey != "")
        {
            items.Add(hands);
        }
        if (Access1.itemKey != "")
        {
            items.Add(Access1);
        }






        return items;
    }
}

[System.Serializable]
public class EquipmentSlottt : ItemContainer
{
    public string slotName;
    public List<EquipmentSlot> validEquips;
    ///public string itemEquipped;

    public EquipmentSlottt(string key ="", string slotName = "", int currCapcity =1) : base (key, currCapcity)
    {
        validEquips = new List<EquipmentSlot>();
        this.slotName = slotName;
    }

    public override ItemContainer Copy()
    {
        EquipmentSlottt s=  new EquipmentSlottt( itemKey, slotName, currCapcity);

        foreach (EquipmentSlot item in validEquips)
        {
            s.validEquips.Add(item);
        }

        return s;
    }
}

