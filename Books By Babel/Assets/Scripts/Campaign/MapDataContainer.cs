using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapDataContainer
{
    public SavedDatabase<MapDataModel> mapDB;
    public SavedDatabase<MapEnchantment> MapEnchantmentsDB;

    public MapDataContainer()
    {
        mapDB = new SavedDatabase<MapDataModel>();
        MapEnchantmentsDB = new SavedDatabase<MapEnchantment>();
    }
}
