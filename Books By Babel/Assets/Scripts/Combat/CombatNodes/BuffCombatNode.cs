using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCombatNode : CombatNode
{
    public Buff buffToApply;

    public BuffCombatNode(Actor source, TileNode targetedTile, Buff buff) 
        : base(source, targetedTile)
    {
        buffToApply = buff;

        target = targetedTile.actorOnTile;
    }

    public override void ApplyEffect()
    {
        target.ApplyBuff(source.actorData, buffToApply);
    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        panel.damageLabel.text = "Apply: " + buffToApply.buffName;
    }
}
