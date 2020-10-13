using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInteractionCombatNode : CombatNode
{
    private int x, y;
    private Interaction interactionToAdd;

    public AddInteractionCombatNode(int x, int y, Interaction interactionToAdd, Actor source, TileNode target) 
        : base(source, target)
    {
        this.x = x;
        this.y = y;
        this.interactionToAdd = interactionToAdd;
    }

    public override void ApplyEffect()
    {
        Globals.GetBoardManager().currentMission.interactionMap.Add("" + x + "" + y, interactionToAdd);
    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        panel.damageLabel.text = "Add interaction to tile";
    }
}
