using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEventCombatNode : CombatNode
{

    private Event eventToAdd;


    public AddEventCombatNode(Actor source, TileNode targetedTile, Event eventToAdd) : base(source, targetedTile)
    {
        this.eventToAdd = eventToAdd;


    }

    public override void ApplyEffect()
    {
        if(eventToAdd is SkillUsedEvent)
        {
            SkillUsedEvent e = eventToAdd as SkillUsedEvent;

            e.FillOutEVent(source, targetedTile);

            Globals.GetBoardManager().currentMission.MissionEvents.Add(e);
        }
    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        panel.damageLabel.text = "Add Event";
    }
}
