using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EliminateAllObjectiveComponent : ObjectiveComponent
{

    /// <summary>
    /// This Mission Type is for when you need to kill all enemy units
    /// </summary>
    /// 

    public override ObjectiveComponent Copy()
    {
        return new EliminateAllObjectiveComponent();
    }

    public override bool ObjectiveComplete(BoardManager bm)
    {
        foreach (Actor actor in bm.spawner.actors)
        {
            if (!actor.ActorsController().PlayerControlled() && actor.actorData.isAlive)
            {
                //complete
                return false;
            }
        }

        return true;
    }

    public override string PrintProgress()
    {
        int enemiesLeft = 0;

        foreach (Actor actor in Globals.GetBoardManager().spawner.actors)
        {
            if (!actor.ActorsController().PlayerControlled() && actor.actorData.isAlive)
            {
                //complete
                enemiesLeft++;
            }
        }

        return "Enemies left to kill: " + enemiesLeft;
    }
}
