using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StealCreditsSkillEffect : SkillEffect
{
    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        if(target.HasActor())
        {
            combat.actorDamageMap.Add(new StealCreditsCombatNode(source, target));
        }

    }

    public override SkillEffect Copy()
    {
        return new StealCreditsSkillEffect();
    }
}
