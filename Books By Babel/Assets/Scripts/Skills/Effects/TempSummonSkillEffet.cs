using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TempSummonSkillEffet : SummonEffect
{
    private int duration;

    public TempSummonSkillEffet(string summonKey, int duration) 
        : base(summonKey)
    {
        this.duration = duration;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        if (target.actorOnTile != null)
        {
            return;
        }


        ActorData ad = SpawnActorData(source, target);

        TempSummonCombatNode node = new TempSummonCombatNode(source, target, ad, duration);
        combat.actorDamageMap.Add(node);
    }

    public override SkillEffect Copy()
    {
        return new TempSummonSkillEffet(summonKey, duration);
    }
}
