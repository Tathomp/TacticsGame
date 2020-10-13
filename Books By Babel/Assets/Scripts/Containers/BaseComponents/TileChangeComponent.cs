using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChangeComponent
{
    public string newTile, prevTile;

    public TileChangeComponent(string newTile, string prevTile="")
    {
        this.newTile = newTile;
        this.prevTile = prevTile;
    }


    public void ApplyEffect(TileNode node)
    {

    }

    public void RemoveEffect(TileNode node)
    {

    }


    public TileChangeComponent Copy()
    {
        return new TileChangeComponent(newTile, prevTile);
    }
}
