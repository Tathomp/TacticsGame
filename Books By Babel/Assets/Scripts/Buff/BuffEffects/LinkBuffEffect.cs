using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinkBuffEffect : BuffEffect
{
    public ActorData linkedUnit;

    public LinkBuffEffect()
    {

    }

    public override BuffEffect Copy()
    {
        LinkBuffEffect e = new LinkBuffEffect();
        e.linkedUnit = linkedUnit;
        return e;
    }

    public override string GetHotbarDescription()
    {
        return "";
    }


    public override void OnRemove(ActorData actor)
    {
        BoardManager bm = Globals.GetBoardManager();
        Actor a = bm.spawner.ActorDataGameObjectMap[linkedUnit];

        //kill the actor
        a.KillActor();

        // Remove the actor data
        bm.pathfinding.GetTileNode(linkedUnit.gridPosX, linkedUnit.gridPosY).actorOnTile = null;

        bm.spawner.actors.Remove(a);
        bm.spawner.ActorDataGameObjectMap.Remove(linkedUnit);

        // destory the sprite
        GameObject.Destroy(a);
        GameObject.Destroy(a.gameObject);
    }


    public override string PrintNameOfEffect()
    {
        return "Linked Buff";

    }
}
