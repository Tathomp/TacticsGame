using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutsceneActionTileEffect : CutSceneAction
{
    public string tileeffectID;
    public MapCoords spawnPosition;

    public CutsceneActionTileEffect(int x, int y, string id)
    {
        this.tileeffectID = id;
        spawnPosition = new MapCoords(x, y);
    }

    public override CutSceneAction Copy()
    {
        return new CutsceneActionTileEffect(spawnPosition.X, spawnPosition.Y, tileeffectID);
    }

    public override IEnumerator ExecuteAction(CutsceneController controller, bool playNextNode = true)
    {
        TileEffectSprite sprite = Globals.GenerateTileEffectSprite(spawnPosition.X, spawnPosition.Y, tileeffectID);
        sprite.transform.SetParent(controller.spawnPoint);
        controller.tileeffects[spawnPosition.X, spawnPosition.Y] = sprite.gameObject;
        //we made want to keep track of this sprite somewere

        yield return null;
        controller.NextNode();
    }
}
