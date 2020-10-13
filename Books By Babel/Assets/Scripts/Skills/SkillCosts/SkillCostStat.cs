using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SkillCostStat : SkillCost
{
    public StatTypes type;
    public int cost;

    public SkillCostStat(StatTypes type, int cost, bool consumeReq) : base(consumeReq)
    {
        this.type = type;
        this.cost = cost;

    }

    public override bool CanPayCost(Actor actor)
    {
        int curValue = actor.GetCurrentStats(type);

        return curValue - cost >= 0;
    }

    public override SkillCost Copy()
    {
        return new SkillCostStat(type, cost, consumeReq);
    }

    public override void PayCost(Actor actor)
    {
        if (consumeReq)
        {
            actor.actorData.ChangeStateType(type, StatContainerType.Current, - cost);
        }
    }

    public override string PrintCost()
    {
        return type + ": " + cost;
    }
}
