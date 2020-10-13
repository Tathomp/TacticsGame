using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldMapTile : DatabaseEntry
{

    public MapCoords position;
    public string filename;

    public WorldMapTile(string key, int x, int y, string filename) : base(key)
    {
        position = new MapCoords(x, y);
        this.filename = filename;
    }

    public WorldMapTile(string key, MapCoords position, string filename) : base(key)
    {
        this.position = position;
        this.filename = filename;
    }

    public override DatabaseEntry Copy()
    {
        return new WorldMapTile(key, position, filename);
    }
}
