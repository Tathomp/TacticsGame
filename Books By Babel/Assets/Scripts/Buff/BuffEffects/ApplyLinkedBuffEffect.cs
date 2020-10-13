using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ApplyLinkedBuffEffect : SkillEffect
{
    public string buffForCaster;
    public string buffForTarget;

    public ApplyLinkedBuffEffect(string buffForCaster, string buffForTarget)
    {
        this.buffForCaster = buffForCaster;
        this.buffForTarget = buffForTarget;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
       if(target.HasActor())
       {
            Buff casterBuff = Globals.campaign.contentLibrary.buffDatabase.GetCopy(buffForCaster);
            Buff targetBuff = Globals.campaign.contentLibrary.buffDatabase.GetCopy(buffForTarget);

            targetBuff.ID = Globals.GenerateRandomHex();

            ActorData t = target.actorOnTile.actorData;

            TileNode casterNode = Globals.GetBoardManager().pathfinding.GetTileNode(source);

            casterBuff.effects.Add(new LinkedBuffBuffEffect(t, targetBuff));
            targetBuff.effects.Add(new LinkedBuffBuffEffect(source.actorData, casterBuff));

            //apply the actual buffs
            BuffCombatNode castNode = new BuffCombatNode(source, casterNode, casterBuff);
            BuffCombatNode targetNode = new BuffCombatNode(source, target, targetBuff);


            combat.actorDamageMap.Add(castNode);
            combat.actorDamageMap.Add(targetNode);
        }
    }

    public override SkillEffect Copy()
    {
        return new ApplyLinkedBuffEffect(buffForCaster, buffForTarget);
    }
}
