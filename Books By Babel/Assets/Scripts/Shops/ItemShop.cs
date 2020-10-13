using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemShop : Shop

{
    public ItemShop(string key, string name, List<string> items, bool unlocked = false, string description = "Placeholder Description") 
        : base(key, name, items, unlocked, description)
    {

    }

    public override DatabaseEntry Copy()
    {
        throw new System.NotImplementedException();
    }
}
