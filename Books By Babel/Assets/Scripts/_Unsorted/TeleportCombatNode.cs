using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCombatNode : CombatNode
{
    Actor teleportTarget;
    TileNode destTileNode;


    public TeleportCombatNode(Actor source, TileNode targetedTile) 
        : base(source, targetedTile)
    {
        teleportTarget = source;
        destTileNode = targetedTile;
    }

    public override void ApplyEffect()
    {

        int newX = targetedTile.data.posX;
        int newY = targetedTile.data.posY;

        Pathfinding p = Globals.GetBoardManager().pathfinding;

        p.MoveWithOnMoveBuffs(source, newX, newY);
        source.transform.position = Globals.GridToWorld(newX, newY);
    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        
    }
}
