using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileDatabaseContainer
{
    public SavedDatabase<TileTypes> Tiles;
    public SavedDatabase<TileEffect> Effects;

    public TileDatabaseContainer()
    {
        Tiles = new SavedDatabase<TileTypes>();
        Effects = new SavedDatabase<TileEffect>();
    }
}
