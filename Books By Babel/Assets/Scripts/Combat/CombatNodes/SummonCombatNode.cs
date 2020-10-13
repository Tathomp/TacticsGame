using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCombatNode : CombatNode
{

    ActorData actorData;

    public SummonCombatNode(Actor source, TileNode targetedTile, ActorData actorData) 
        : base(source, targetedTile)
    {
        this.actorData = actorData;

    }

    public override void ApplyEffect()
    {
        Globals.SpawnMonster(actorData);

    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        panel.damageLabel.text = "Summon";
        panel.targetLabel.text = actorData.Name + "\n";
        panel.targetLabel.text += actorData.currentStatCollection.statDict[StatTypes.Health] + " / " + actorData.maxStatCollection.statDict[StatTypes.Health];

    }
}
