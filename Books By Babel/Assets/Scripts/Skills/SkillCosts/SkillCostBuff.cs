using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillCostBuff : SkillCost
{
    public string buffKey;

    public bool lookForTag; //TODO implement this

    public SkillCostBuff(string buffKey, bool consumeReq) : base(consumeReq)
    {
        this.buffKey = buffKey;
    }

    public override bool CanPayCost(Actor actor)
    {
        foreach (Buff buff in actor.actorData.buffContainer.buffList)
        {
            if(buff.GetKey() == buffKey)
            {
                return true;
            }
        }

        return false;
    }

    public override SkillCost Copy()
    {
        return new SkillCostBuff(buffKey, consumeReq);
    }


    public override void PayCost(Actor actor)
    {
        if(consumeReq)
        {
            actor.actorData.buffContainer.RemoveBuff(
                actor.actorData,
                buffKey);
        }
    }

    public override string PrintCost()
    {
        return "Requires: " + buffKey + " to be active";

    }
}
