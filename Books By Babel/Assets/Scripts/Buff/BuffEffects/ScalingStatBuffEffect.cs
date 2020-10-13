using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScalingStatBuffEffect : BuffEffect
{
    public StatContainerType containerTypeBasis, containerToChange;
    public StatTypes statTypeToChange, statTypeBasis;
    public float scale_factor;

    public int max_amt_added = 0;
    public int curr_amt_added = 0;

    public bool restore_stats;

    public ScalingStatBuffEffect(StatContainerType toChange, StatContainerType scBasis, StatTypes change, StatTypes basis, float factor, bool restore = false)
    {
        this.containerTypeBasis = scBasis;
        this.containerToChange = toChange;

        this.statTypeToChange = change;
        this.statTypeBasis = basis;
        scale_factor = factor;

        restore_stats = restore;
    }

    public override BuffEffect Copy()
    {
        ScalingStatBuffEffect temp = new ScalingStatBuffEffect(containerToChange, containerTypeBasis, statTypeToChange, statTypeBasis, scale_factor, restore_stats);

        temp.max_amt_added = max_amt_added;
        temp.curr_amt_added = curr_amt_added;

        return temp;
    }

    public override void OnApply(ActorData actor, ActorData source)
    {
        if(containerToChange == StatContainerType.Current || containerToChange == StatContainerType.Both)
        {
            if(containerTypeBasis == StatContainerType.Current)
            {
                // use current stats as basis
                curr_amt_added += Mathf.RoundToInt(actor.currentStatCollection.GetValue(statTypeBasis) * (scale_factor));
            }
            else
            {
                //use max stats as basis
                curr_amt_added += Mathf.RoundToInt(actor.maxStatCollection.GetValue(statTypeBasis) * (scale_factor));

            }
        }

        if(containerToChange == StatContainerType.Max || containerToChange == StatContainerType.Both)
        {
            if (containerTypeBasis == StatContainerType.Current)
            {
                // use current stats as basis
                max_amt_added += Mathf.RoundToInt(actor.currentStatCollection.GetValue(statTypeBasis) * (scale_factor));
            }
            else
            {
                //use max stats as basis
                max_amt_added += Mathf.RoundToInt(actor.maxStatCollection.GetValue(statTypeBasis) * (scale_factor));

            }
        }

        actor.ChangeStateType(statTypeToChange, StatContainerType.Current, curr_amt_added);
        actor.ChangeStateType(statTypeToChange, StatContainerType.Max, max_amt_added);
    }

    public override void OnRemove(ActorData actor)
    {
        if (restore_stats)
        {
            actor.ChangeStateType(statTypeToChange, StatContainerType.Current, -curr_amt_added);    //invers e of amoutn added
            actor.ChangeStateType(statTypeToChange, StatContainerType.Max, -max_amt_added);         //inverse of amount added
        }
    }


    public override string PrintNameOfEffect()
    {
        return "Scaling Stat";

    }
}
