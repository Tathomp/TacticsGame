using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SkillCost
{
    //This is what 
    public bool consumeReq;


    public SkillCost(bool consumeReq)
    {
        this.consumeReq = consumeReq;
    }

    public abstract void PayCost(Actor actor);
    public abstract bool CanPayCost(Actor actor);
    public abstract string PrintCost();
    public abstract SkillCost Copy();

}
