using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldMap
{
    public WorldMapTile[,] worldMapDisplayData;
    public LocationNode[,] locations;


    public int sizeX, sizeY;

    // We'll want to lock these so that they can't hold negative values
    public MapCoords currentPos, defaultStartPos;
    public string playerAvatarFilePath;

    public string worldMapName;
    public string key;


    public WorldMap (int x, int y)
    {
        sizeX = x;
        sizeY = y;


        worldMapDisplayData = new WorldMapTile[sizeX, sizeY];
        locations = new LocationNode[sizeX, sizeY];

        currentPos = new MapCoords(-1, -1);

        key = Globals.GenerateRandomHex();
    }

    public MapCoords GetPositionForAvatar()
    {
        if(currentPos.X == -1)
        {
            return defaultStartPos;
        }

        return currentPos;
    }

    public bool OutOfRange(int x, int y)
    {
        if(x >= sizeX || x < 0 || y >= sizeY || y <0)
        {
            return true;
        }

        return false;
    }

    public void ChangeCurrentPos(int x, int y)
    {
        currentPos = new MapCoords(x, y);
    }
    public void ChangeCurrentPos(MapCoords corrds)
    {
        currentPos = corrds;
    }
    public void AddLocation(LocationNode node)
    {
        locations[node.coords.X, node.coords.Y] = node;
    }

    public List<Bar> GetAllBars()
    {
        List<Bar> b = new List<Bar>();

        foreach (LocationNode item in locations)
        {
            if (item != null)
            {
                foreach (LocationComponent component in item.locationcomponents)
                {
                    if (component is BarLocationComponent)
                    {
                        BarLocationComponent bb = component as BarLocationComponent;

                        b.Add(
                            Globals.campaign.GetcutScenedataContainer().barDatabase.GetCopy(bb.barKey)
                            );
                    }
                }
            }
        }


        return b;
    }

}
