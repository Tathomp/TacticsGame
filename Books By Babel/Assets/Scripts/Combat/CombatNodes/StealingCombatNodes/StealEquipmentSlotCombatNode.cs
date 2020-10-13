using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StealEquipmentSlotCombatNode : CombatNode
{
    EquipmentSlot slot;

    public StealEquipmentSlotCombatNode(Actor source, TileNode targetedTile, EquipmentSlot slot) 
        : base(source, targetedTile)
    {
        this.slot = slot;
    }

    public override void ApplyEffect()
    {
        if(target != null)
        {
            string item = target.actorData.equipment.UneqipItem(target.actorData, slot);

            if(source.actorData.inventory.AddItem(item) == false)
            {
                Globals.campaign.currentparty.partyInvenotry.AddItem(item);
            }
        }
    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        panel.damageLabel.text = source.actorData.Name + "steals the " + slot + " from " + target.actorData.Name;
    }

}
