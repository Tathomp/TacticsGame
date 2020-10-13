using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponItem : EquippableItem
{
    /// <summary>
    /// This stuff isn't used currently wtf am i doing
    /// </summary>


    public ITargetable targetData;

    public WeaponItem(ITargetable targetData)
    {
        this.targetData = targetData;
    }

    public override bool IsWeapon()
    {
        return true;
    }

    public override EquippableItem Copy()
    {
        WeaponItem e = new WeaponItem(targetData);
        e.bonusStats = (StatsContainer)bonusStats.Copy();





        foreach (EquipmentSlot s in validSlots)
        {
            e.AddSlot(s);
        }

        return e;
    }
}
