using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapDataModel : DatabaseEntry
{
    public string[,] tileBoard;
    public List<MapCoords> playerSpawnLocations, enemySpawnLocations;

    public int sizeX, sizeY;

    public string mapName;


    public MapDataModel(string key, int x, int y) : base(key)
    {
        sizeX = x;
        sizeY = y;

        tileBoard = new string[x, y];

        tileBoard = new string[sizeX, sizeY];
        playerSpawnLocations = new List<MapCoords>();
        enemySpawnLocations = new List<MapCoords>();
    }


    public override DatabaseEntry Copy()
    {
        MapDataModel map = new MapDataModel(key, sizeX, sizeY);
        map.mapName = mapName;


        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                map.tileBoard[x, y] = tileBoard[x, y];
            }
        }

        foreach (MapCoords coords in enemySpawnLocations)
        {
            map.enemySpawnLocations.Add(coords);
        }

        foreach (MapCoords coords in playerSpawnLocations)
        {
            map.playerSpawnLocations.Add(coords);
        }

        return map;
    }

    public void ResizeMap(int newSizeX, int newSizeY)
    {
        string[,] newMap = new string[newSizeX, newSizeY];


    }
    

    public void InitAllEnchantments()
    {
        // used to init 
        throw new NotImplementedException();
    }


    #region Unit Restrictions
    public int GetMaxNumberOfPlayerSpawns()
    {
        return playerSpawnLocations.Count;
    }

    public int GetMaxNumberOfEnemySpawns()
    {
        return enemySpawnLocations.Count;
    }

    #endregion
}

