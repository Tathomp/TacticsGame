using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReviveActorEffect : SkillEffect
{
  
    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        
        if(target.actorOnTile == null)
        {
            return;
        }    

        if(target.actorOnTile.actorData.isAlive)
        {
            return;
        }

        ReviveEffectCombatNode reviveActorEffect = new ReviveEffectCombatNode(source, target);


        combat.actorDamageMap.Add(reviveActorEffect);

    }

    public override SkillEffect Copy()
    {
        ReviveActorEffect r = new ReviveActorEffect();

        return r;
    }
}
