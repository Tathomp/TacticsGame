using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SAFE TO DELETE
/// PROBABLY
/// </summary>
[System.Serializable]
public class MissionState
{
    public Mission CurrentMission;
    public List<TileEffect>[,] initalTileEffects;


    public MissionState(Mission mission)
    {
        this.CurrentMission = mission;
        //initalTileEffects = new List<TileEffect>[CurrentMission.map.sizeX, CurrentMission.map.sizeY];
    }
}
