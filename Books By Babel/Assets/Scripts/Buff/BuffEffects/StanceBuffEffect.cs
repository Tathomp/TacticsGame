using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StanceBuffEffect : BuffEffect
{
    public Buff transformBuff;
    public string transformCatagory;

    // Current Interation of transform buff should be refactored to stance buff
    // a new transform class should be made to replace stats and skills etc

    public override BuffEffect Copy()
    {
        StanceBuffEffect b = new StanceBuffEffect();
        b.transformBuff = (Buff)transformBuff.Copy();
        b.transformCatagory = transformCatagory;

        return b;
    }

    public override string GetHotbarDescription()
    {
        return transformBuff.GetHotbarDescription();
    }



    public override void OnApply(ActorData actor, ActorData source)
    {
        // Remove buff that belongs to the same family as the one we're trying to apply
        //
        List<Buff> buffs = actor.buffContainer.buffList;

        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            foreach (BuffEffect e in buffs[i].effects)
            {
                if (e is StanceBuffEffect)
                {
                    if (((StanceBuffEffect)e).transformCatagory == transformCatagory)
                    {
                        //actor.buffContainer.RemoveBuff(buffs[i]);
                        actor.buffContainer.RemoveBuff(actor, buffs[i]);
                        actor.buffContainer.ApplyBuff(actor, source, transformBuff);

                        //this might cause a problem if we need to remove two buffs of the same category
                        //its hard to find a use case for tha tho
                        return;
                    }
                }
            }

        }

        // A buff of the same family wasn't found, so we'll apply the new buff 
        actor.buffContainer.ApplyBuff(actor, source, transformBuff);
    }

    public override void OnDamageInflicted(Combat combat, AnimationData currentData)
    {
        transformBuff.OnAttacked(combat, currentData);
    }


    public override void OnDeath(ActorData actor, ActorData killer)
    {
        transformBuff.OnDeath(actor, killer);
    }


    public override void OnRemove(ActorData actor)
    {
        actor.buffContainer.RemoveBuff(actor, transformBuff);
        Debug.LogWarning("buff might not be removed");
    }


    public override void OnStartTurn(ActorData actor)
    {
        transformBuff.OnStartTurn(actor);
    }

    public override string PrintNameOfEffect()
    {
        return "Change Stance";

    }
}
