using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreationTile : SpriteTransitionTest
{
    public string currType;
    public int posX, posY;

    public void InitMapTile(string type, int x, int y)
    {
        currType = type;
        DisplaySprite();

        posX = x;
        posY = y;

    }


    public void ChangeTileType(string newType)
    {
        currType = newType;
        DisplaySprite();

    }


    private void DisplaySprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite
            = Globals.GetSprite(FilePath.TileSetAtlas, currType);

        StartEnlarge(1.6f);
    }



}
