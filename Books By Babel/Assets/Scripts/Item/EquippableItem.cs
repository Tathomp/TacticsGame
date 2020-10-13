using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquippableItem : IHotbar
{
    /// <summary>
    /// This isn't the actual item
    /// Maybe this can be a base class for an item that gives a passive buff (like setting the tile blow
    /// the unit on fire
    /// </summary>

    public List<EquipmentSlot> validSlots;
    public StatsContainer bonusStats;


  //  public bool excludeList;  //when true, jobs on list CAN'T use the item, when false, only jobs on the list can use the item 


    public EquippableItem()
    {
        validSlots = new List<EquipmentSlot>();
        bonusStats = new StatsContainer();

        //excludeList = true;
    }

    public bool ValidSlot(EquipmentSlot e)
    {
        return validSlots.Contains(e);
    }

    public void AddSlot(EquipmentSlot slot)
    {
        validSlots.Add(slot);
    }

    public StatsContainer GetBonusStats()
    {
        return bonusStats;
    }

  

    public virtual bool IsWeapon()
    {
        return false;
    }

    #region Interfaces
    public virtual EquippableItem Copy()
    {
        EquippableItem e = new EquippableItem();
        e.bonusStats = (StatsContainer)bonusStats.Copy();


       // e.excludeList = excludeList;


        foreach (EquipmentSlot s in validSlots)
        {
            e.AddSlot(s);
        }

        return e;
    }

    public string GetHotbarDescription()
    {
        throw new System.NotImplementedException();
    }

    public string GetName()
    {
        throw new System.NotImplementedException();
    }

    public string GetIconFilePath()
    {
        throw new System.NotImplementedException();
    }

    #endregion
}
