using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllAlliesDeadFailState : ObjectiveComponent
{


    public override ObjectiveComponent Copy()
    {
        return new AllAlliesDeadFailState();
    }


    public override bool ObjectiveComplete(BoardManager bm)
    {
        foreach (Actor actor in bm.spawner.actors)
        {
            if(actor.actorData.controller.PlayerControlled() && actor.actorData.isAlive)
            {
                return false;
            }
        }

        return true;
    }

    public override string PrintProgress()
    {
        return "All allies die.";
    }
}
