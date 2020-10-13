using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public TileData data;
    public TileTypes type;
    
    public void InitTile(TileData data, TileTypes tileTypes)
    {
        this.data = data;
        type = tileTypes;
    }

    public void ChangeTileType(TileTypes newType)
    {
        type = newType;
        GetComponent<SpriteRenderer>().sprite = Globals.GetSprite(FilePath.TileSetAtlas, newType.spriteFilePath);
    }
}
