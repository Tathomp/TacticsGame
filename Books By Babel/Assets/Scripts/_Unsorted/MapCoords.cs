using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MapCoords
{
    public int X;
    public int Y;

    public MapCoords(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool IsEqual(MapCoords coords)
    {
        return this.X == coords.X && this.Y == coords.Y;
    }
   
}
