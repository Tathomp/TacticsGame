using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//Heal or damage over time
public class ChangePerTurnBuffEffect : BuffEffect
{
    public ScalingStatBuffEffect effect;
    // change to change a given stat or set of stats by a given amount per turn


    // dont we just have a change stat effect?
    //we can just use that here

    public ChangePerTurnBuffEffect(ScalingStatBuffEffect effect)
    {
        this.effect = effect;
    }

    public override BuffEffect Copy()
    {
        return new ChangePerTurnBuffEffect(effect.Copy() as ScalingStatBuffEffect);
    }

    public override string GetHotbarDescription()
    {
        return "";
    }

    public override void OnRemove(ActorData actor)
    {
        effect.OnRemove(actor);
    }

    public override void OnStartTurn(ActorData actor)
    {
        effect.OnApply(actor, actor);
    }

    public override string PrintNameOfEffect()
    {
        return "Change Per Turn (might be depricated)";

    }
}
