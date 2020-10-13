using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WorldMapLocationGameObject : MonoBehaviour {

    public LocationNode location;
    public SpriteRenderer sr;

    public List<Mission> missions;

    public void InitGameObject(LocationNode location)
    {
        this.location = location;
        missions = new List<Mission>();

        sr.sprite = Resources.Load<SpriteAtlas>(FilePath.TileSetAtlas).GetSprite(location.filepath);
        transform.position = new Vector3(location.coords.X, location.coords.Y, 0);
    }


}
