using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RedirectCombatBuffEffect : BuffEffect
{

    public ActorData newTarget_id;

    public override void OnApply(ActorData actor, ActorData source)
    {
        newTarget_id = source;
    }

    public override void OnTargeted(Combat combat, AnimationData currentData)
    {
        TileNode newTarget = Globals.GetBoardManager().pathfinding.GetTileNode(Globals.GetBoardManager().spawner.GetActor(newTarget_id));
        currentData.DestNode = newTarget;

    }


    public override BuffEffect Copy()
    {
        RedirectCombatBuffEffect r = new RedirectCombatBuffEffect();
        r.newTarget_id = newTarget_id;

        return r;
    }


    public override string PrintNameOfEffect()
    {
        return "Redirct Effect";

    }
}
