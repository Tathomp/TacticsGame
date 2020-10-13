using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinkedBuffBuffEffect : BuffEffect
{
    public ActorData target;
    public Buff buffToRemove;

    private bool locked;

    public LinkedBuffBuffEffect(ActorData target, Buff buffToRemove, bool locked = false)
    {
        this.target = target;
        this.buffToRemove = buffToRemove;
        this.locked = locked;
    }

    public override void OnRemove(ActorData actor)
    {
               
        target.buffContainer.RemoveBuff(target, buffToRemove);
        
    }

    public override string  GetHotbarDescription()
    {
        return "Linked: " + target.Name + " Buff: " + buffToRemove.buffName;
    }

    public override BuffEffect Copy()
    {
        return new LinkedBuffBuffEffect(target, buffToRemove, locked);
    }


    public override string PrintNameOfEffect()
    {
        return "Change Stance";

    }
}
