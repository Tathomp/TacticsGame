using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractionObjectiveComponent : ObjectiveComponent
{
    public bool triggered;
    public string id; //this id is used to by the interaction 

    public InteractionObjectiveComponent(string id)
    {
        this.id = id;
    }

    public override ObjectiveComponent Copy()
    {
        return new InteractionObjectiveComponent(id) { triggered = triggered};
    }

    public override bool ObjectiveComplete(BoardManager bm)
    {
        return triggered;
    }

    public override string PrintProgress()
    {
        return "Thing has been triggered: ";
    }
}
