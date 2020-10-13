using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTileEffectCombatNode : CombatNode
{
    string newTileType;

    public ChangeTileEffectCombatNode(string newTileType, Actor source, TileNode targetedTile) : base(source, targetedTile)
    {
        this.newTileType = newTileType;
    }

    public override void ApplyEffect()
    {

        int x = targetedTile.data.posX;
        int y = targetedTile.data.posY;


        string newtype = newTileType;


        //Ajdust ActorStats
        TileTypes newType = Globals.campaign.GetTileData().Tiles.GetCopy(newtype);

        if (targetedTile.actorOnTile != null)
        {
            targetedTile.actorOnTile.ReduceStats(targetedTile.type.tileBonuses);
            targetedTile.actorOnTile.AddStats(newType.tileBonuses);
        }

        Globals.GetBoardManager().currMap.tileBoard[x, y] = newtype;
        Globals.GetBoardManager().pathfinding.tiles[x, y].type = newType;

        targetedTile.tileGO.GetComponent<SpriteRenderer>().sprite = Globals.GetSprite(FilePath.TileSetAtlas,
            Globals.campaign.GetTileData().Tiles.GetData(newtype).spriteFilePath);
    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        panel.damageLabel.text = "Change tile to: " + newTileType;
    }
}
