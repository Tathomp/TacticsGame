using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinkedSummonSkillEffect : SkillEffect
{
    public string summonKey;
    public string buffKey;



    public LinkedSummonSkillEffect(string summonKey, string buffKey)
    {
        this.summonKey = summonKey;
        this.buffKey = buffKey;        
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        if (target.actorOnTile != null)
        {
            return;
        }

        ActorData ad = SpawnActorData(source, target);
        Buff b = Globals.campaign.contentLibrary.buffDatabase.GetCopy(buffKey);

        foreach (LinkBuffEffect linked in b.effects)
        {
            if(linked.linkedUnit == null)
            {
                linked.linkedUnit = ad;
            }
        }

        BuffCombatNode bnode = new BuffCombatNode(source, Globals.GetBoardManager().pathfinding.GetTileNode(source), b);
        SummonCombatNode snode = new SummonCombatNode(source, target, ad);

        combat.actorDamageMap.Add(bnode);
        combat.actorDamageMap.Add(snode);

    }

    public override SkillEffect Copy()
    {
        return new LinkedSummonSkillEffect(summonKey, buffKey);
    }


    private ActorData SpawnActorData(Actor source, TileNode target)
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
