using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StealEquipmentSkillEffect : SkillEffect
{
    EquipmentSlot slotToStealFrom;

    public StealEquipmentSkillEffect(EquipmentSlot slot)
    {
        this.slotToStealFrom = slot;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        if(target.HasActor())
        {
            combat.actorDamageMap.Add(new StealEquipmentSlotCombatNode(source, target, slotToStealFrom));
        }
    }

    public override SkillEffect Copy()
    {
        return new StealEquipmentSkillEffect(slotToStealFrom);
    }
}
