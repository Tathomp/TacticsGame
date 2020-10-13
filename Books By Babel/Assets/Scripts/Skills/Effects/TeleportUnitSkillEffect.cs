using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeleportUnitSkillEffect : SkillEffect
{
       
    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {

        if(target.actorOnTile != null)
        {
            // there's already an actor here, let's throw an expeception for now?
            //
            return;

        }

        TeleportCombatNode node = new TeleportCombatNode(source, target);

        combat.actorDamageMap.Add(node);
    }

    public override SkillEffect Copy()
    {
        return new TeleportUnitSkillEffect();
    }
}
