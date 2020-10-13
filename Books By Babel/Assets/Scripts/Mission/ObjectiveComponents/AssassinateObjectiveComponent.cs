using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssassinateObjectiveComponent : ObjectiveComponent
{
    protected string target;

    public AssassinateObjectiveComponent(string target)
    {
        this.target = target;
    }

    public override ObjectiveComponent Copy()
    {
        return new AssassinateObjectiveComponent(target);
    }

    public override bool ObjectiveComplete(BoardManager bm)
    {
        List<Actor> actors = bm.spawner.actors;


        foreach (Actor actor in actors)
        {
            if(actor.actorData.GetKey() == target)
            {
                if(actor.actorData.isAlive)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        Debug.LogWarning("Actor can not be found to kill, this should never happen");
        return true;        

    }

    public override string PrintProgress()
    {
        //Change this to the actual name of the unit at somet point
        return "Kill: " + target;
    }
}
