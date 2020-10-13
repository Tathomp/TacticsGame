using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveEffectCombatNode : CombatNode
{
    

    public ReviveEffectCombatNode(Actor source, TileNode targetedTile) 
        : base(source, targetedTile)
    {
        if (targetedTile.actorOnTile == null)
        {
            return;
        }

        target = targetedTile.actorOnTile;

    }

    public override void ApplyEffect()
    {
        int x = target.GetMaxStats(StatTypes.Health) / 2;

        target.ReviveActor(x);
    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        int x = target.GetMaxStats(StatTypes.Health) / 2;

        panel.damageLabel.text = "Revive";
    }
}
