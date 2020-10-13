using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StealInventorySkillEffect : SkillEffect
{
    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        if(target.HasActor())
        {
            combat.actorDamageMap.Add(new StealInventoryItemCombatNode(source, target));
        }
    }

    public override SkillEffect Copy()
    {
        return new StealInventorySkillEffect();
    }
}
