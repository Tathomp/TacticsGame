using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BanishEffect : BuffEffect
{
    private int baseDuration, currDuration;
    // Rename to Vanish Effect
    // Have the part that 'banishes' in it's own class

    public BanishEffect(int duration)
    {
        baseDuration = duration;
        currDuration = duration;
    }

    public override BuffEffect Copy()
    {
        BanishEffect e = new BanishEffect(baseDuration);
        e.currDuration = currDuration;

        return e;
    }

    public override string GetHotbarDescription()
    {
        return "Unit will be banished in: " + currDuration;
    }



    public override void OnStartTurn(ActorData actor)
    {
        currDuration--;

        if(currDuration <= 0)
        {
            BoardManager bm = Globals.GetBoardManager();
            Actor a = bm.spawner.ActorDataGameObjectMap[actor];

            //kill the actor
            a.KillActor();

            // Remove the actor data
            bm.pathfinding.GetTileNode(actor.gridPosX, actor.gridPosY).actorOnTile = null;

            bm.spawner.actors.Remove(a);
            bm.spawner.ActorDataGameObjectMap.Remove(actor);

            // destory the sprite
            GameObject.Destroy(a);
            GameObject.Destroy(a.gameObject);


            //this part shoudl be interesting
            bm.turnManager.Fastest();
            bm.turnManager.CalculateFastest();
        }
    }


    public override string PrintNameOfEffect()
    {
        return "Banish";

    }
}
