using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : DatabaseEntry, IHotbar
{
    public ItemType itemType;
    public string Name;
    public string descript;
    public bool DisappearsInventory = false;
    public bool ChargeItem;
    public int cost;
    public string iconName;

    public int maxStack;

    public EquippableItem equippEffect;
    public Activateableitem consumeableEffect;
    public Activateableitem activationEffect;


    public List<string> validJobs = new List<string>();


    public Item(string key, string iconfilepath = "unkown", int cost= 100) : base (key)
    {
        this.cost = cost;
        iconName = iconfilepath;

        maxStack = 1;
        ChargeItem = false;

        descript = "No description added right now";

    }

    public bool IsAllowedToBeEquiped(string key) //let's just allow this
    {

        return validJobs.Contains(key);


    }

    public override DatabaseEntry Copy()
    {
        Item item = new Item(key);


        item.itemType = itemType;
        item.Name = Name;
        item.descript = descript;
        item.DisappearsInventory = DisappearsInventory;
        item.cost = cost;
        item.maxStack = maxStack;
        item.ChargeItem = ChargeItem;

        if (IsEquippable())
        {
            item.equippEffect = (EquippableItem)equippEffect.Copy();
        }

        if(HasConsumableEFfect())
        {
            item.consumeableEffect = (Activateableitem)consumeableEffect.Copy();
        }

        if(HasActivationEffect())
        {
            item.activationEffect = (Activateableitem)activationEffect.Copy();

        }


        foreach (string key in validJobs)
        {
            item.validJobs.Add(key);
        }

        return item;

    }
    public string GetHotbarDescription()
    {
        return descript;
    }

    public string GetName()
    {
        return Name;
    }

    #region Composite Getters
    public EquippableItem GetEquippedItem()
    {
        return equippEffect;
    }

    public Activateableitem GetConsumableEffect()
    {
        return consumeableEffect;
    }

    public Activateableitem GetActivateableEffect()
    {
        return activationEffect;
    }

    #endregion

    #region Composition Test
    public bool IsEquippable()
    {
        return equippEffect != null;
    }

    public bool HasConsumableEFfect()
    {
        return consumeableEffect != null;
    }

    public bool HasActivationEffect()
    {
        return activationEffect != null;
    }

    public string GetIconFilePath()
    {
        return iconName;
    }
    #endregion

    public bool StackableItem()
    {
        if(ChargeItem)
        {
            /// Charge items can't be stacked
            ///
            return false;
        }


        return maxStack > 1;
    }


}
