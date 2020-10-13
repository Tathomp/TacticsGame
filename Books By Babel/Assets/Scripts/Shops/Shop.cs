using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Shop : DatabaseEntry
{
    public string ShopName { get; set; }
    public string Description { get; set; }
    public bool Unlocked { get; set; }

    public List<string> AvaliableItemKeys { get; set; }

    public Shop(string key, string name, List<string> items,
        bool unlocked=false, string description="Placeholder Description") : base(key)
    {
        ShopName = name;
        AvaliableItemKeys = items;
        Unlocked = unlocked;
        Description = description;
    }

    public virtual List<string> GetItems()
    {
        return AvaliableItemKeys;
    }
    
}
