using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SummonEffect : SkillEffect
{
    public string summonKey;

    /// Suggested Changes
    /// Just remove this member par, scope it to the effect method
    /// We're no longer deserializing every time we try to make the effect so it doesn't
    /// need to be cached
    /// 

    public SummonEffect(string summonKey)
    {
        this.summonKey = summonKey;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {

        if (target.actorOnTile != null)
        {
            return;
        }

        ActorData ad = SpawnActorData(source, target);

        SummonCombatNode node = new SummonCombatNode(source, target, ad);
        combat.actorDamageMap.Add(node);
    }

    public override SkillEffect Copy()
    {
        SummonEffect se = new SummonEffect(summonKey);
        se.summonKey = summonKey;

        return se;
    }

    public ActorData SpawnActorData(Actor source, TileNode target)
    {
        int x = target.data.posX;
        int Y = target.data.posY;


        ActorData ad = Globals.campaign.contentLibrary.actorDB.GetCopy(summonKey);


        ad = ad.Copy() as ActorData;

        ad.gridPosX = x;
        ad.gridPosY = Y;

        ad.controller = source.ActorsController();

        return ad;
    }
}
