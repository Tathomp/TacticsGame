using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomSpawnData
{
    public string missionID;
    //public int posX, posY;
    public List<string> flags;
    public string locationNodeSpawnKey, currentLocationNodeKey;

    public RandomSpawnData(string s, string location)
    {
        this.missionID = s;
        flags = new List<string>();
        locationNodeSpawnKey = location;
        currentLocationNodeKey = location;
    }

    public RandomSpawnData(string s, string location, List<string> requirements)
    {
        this.missionID = s;
        this.currentLocationNodeKey = location;
        this.currentLocationNodeKey = location;
        this.flags = requirements;
    }


    public RandomSpawnData Copy()
    {
        RandomSpawnData data = new RandomSpawnData(missionID, locationNodeSpawnKey);

        data.flags = flags;

        data.locationNodeSpawnKey = locationNodeSpawnKey;
        data.currentLocationNodeKey = currentLocationNodeKey;

        return data;
    }
    
}
