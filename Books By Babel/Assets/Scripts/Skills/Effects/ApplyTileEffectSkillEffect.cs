using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ApplyTileEffectSkillEffect : SkillEffect
{

    public string tileeffect_toapply_id;

    public ApplyTileEffectSkillEffect(string tileeffect_toapply_id)
    {
        this.tileeffect_toapply_id = tileeffect_toapply_id;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        ApplyTileEffectCombatNode node = new ApplyTileEffectCombatNode(tileeffect_toapply_id, source, target);

        combat.actorDamageMap.Add(node);
    }

    public override SkillEffect Copy()
    {
        return new ApplyTileEffectSkillEffect(tileeffect_toapply_id);
    }
}
