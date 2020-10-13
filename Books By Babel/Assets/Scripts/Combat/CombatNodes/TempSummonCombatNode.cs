using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSummonCombatNode : SummonCombatNode
{
    public TempSummonCombatNode(Actor source, TileNode targetedTile, ActorData actorData, int duration) 
        : base(source, targetedTile, actorData)
    {
        Buff buff = new Buff("Temporary Summon", "unkown");
        buff.effects.Add(new BanishEffect(duration));

        actorData.buffContainer.ApplyBuff(actorData, source.actorData, buff);

    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        base.UpDatePreview(panel);
    }
}
