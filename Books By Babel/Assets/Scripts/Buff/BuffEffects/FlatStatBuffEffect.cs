using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlatStatBuffEffectPerTurn : BuffEffect
{

    private StatBuff statComponent;

    public FlatStatBuffEffectPerTurn(StatBuff statComponent)
    {
        this.statComponent = statComponent;
    }

    public override void OnStartTurn(ActorData actor)
    {        
        statComponent.OnApply(actor, actor);
    }


    public override void OnRemove(ActorData actor)
    {
        statComponent.OnRemove(actor);
    }

    public override BuffEffect Copy()
    {
        FlatStatBuffEffectPerTurn fsbe = new FlatStatBuffEffectPerTurn(statComponent.Copy() as StatBuff);

        CopyConditionals(fsbe);

        return fsbe;
    }

    public override string PrintNameOfEffect()
    {
        return "Flat Stat Change";

    }
}
