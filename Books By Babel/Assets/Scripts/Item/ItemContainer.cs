using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemContainer 
{

    public string itemKey;
    public int currCapcity;

    public ItemContainer(string key, int currCapcity)
    {
        this.itemKey = key;
        this.currCapcity = currCapcity;
    }

    public virtual ItemContainer Copy()
    {
        return new ItemContainer(itemKey, currCapcity);
    }
}

public class EquipmentItemContainer : ItemContainer
{
    public EquipmentItemContainer(string key, int currCapcity) 
        : base(key, currCapcity)
    {

    }
}

public class InventoryItemContainer : ItemContainer
{
    public InventoryItemContainer(string key, int currCapcity, int indexLocatedAt)
        : base(key, currCapcity)
    {

    }


}
