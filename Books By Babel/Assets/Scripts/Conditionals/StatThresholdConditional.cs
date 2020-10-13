using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatThresholdConditional : Conditional
{
    public StatTypes type;
    public StatContainerType containerType;
    public int threshold;
    public bool lessThan;

    public StatThresholdConditional(StatTypes type, StatContainerType containerType, int threshold, bool lessThan)
    {
        this.type = type;
        this.containerType = containerType;
        this.threshold = threshold;
        this.lessThan = lessThan;
    }

    public override bool ConditionMet(Actor actor, TileNode target, Skill skill)
    {
        if (containerType == StatContainerType.Current)
        {

            return (actor.GetCurrentStats(type) <= threshold) == lessThan || (actor.GetCurrentStats(type) == threshold);
        }
        else
        {
            return (actor.GetMaxStats(type) <= threshold) == lessThan || (actor.GetMaxStats(type) == threshold);
        }
    }

    public override Conditional Copy()
    {
        return new StatThresholdConditional(type, containerType, threshold, lessThan);
    }

    public override string DisplayCondition(Actor actor, TileNode target, Skill skill)
    {
        return "";
    }
}
