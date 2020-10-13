using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ApplyBuffEffect : SkillEffect
{
    public string buffToApply;

    public ApplyBuffEffect(string buffKey)
    {
        buffToApply = buffKey;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target) 
    {
        if(target.actorOnTile == null)
        {
            return;
        }

        Buff b = Globals.campaign.contentLibrary.buffDatabase.GetCopy(buffToApply);

        BuffCombatNode node = new BuffCombatNode(source, target, b);

        combat.actorDamageMap.Add(node);
    }

    public override SkillEffect Copy()
    {
        ApplyBuffEffect be = new ApplyBuffEffect(buffToApply);


        return be;
    }
}
