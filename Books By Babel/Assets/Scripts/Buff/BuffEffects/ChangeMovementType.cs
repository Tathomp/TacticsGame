using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChangeMovementType : BuffEffect
{
    public string newMovementType, oldMovementType;

    public ChangeMovementType(string newMovementType, string oldMovementType = "")
    {
        this.newMovementType = newMovementType;
        this.oldMovementType = oldMovementType;
    }

    public override BuffEffect Copy()
    {
        return new ChangeMovementType(newMovementType, oldMovementType);
    }

    public override string GetHotbarDescription()
    {
        return "";
    }

    public override void OnApply(ActorData actor, ActorData source) 
    {
        oldMovementType = actor.movement;
        actor.movement = newMovementType;
    }
    
    public override void OnRemove(ActorData actor)
    {
        actor.movement = oldMovementType;
    }

    public override string PrintNameOfEffect()
    {
        return "Change Movement Type";

    }
}
