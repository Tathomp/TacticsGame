using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WorldMapTileGameObject : MonoBehaviour {

    public WorldMapTile tile;
    public SpriteRenderer sr;

	public void InitWorldMapObject(WorldMapTile tiledata)
    {
        tile = tiledata;

        sr.sprite = Resources.Load<SpriteAtlas>(FilePath.TileSetAtlas).GetSprite(tile.filename);
        transform.position = new Vector3(tile.position.X, tile.position.Y, 0);

    }
}
