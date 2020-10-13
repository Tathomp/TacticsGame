using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEnchantmentCombatNode : CombatNode
{
    MapEnchantment enchantment;

    public WorldEnchantmentCombatNode(Actor source, TileNode targetedTile, string enchantmentKeyToAdd) 
        : base(source, targetedTile)
    {
        enchantment = Globals.campaign.GetMapDataContainer().MapEnchantmentsDB.GetCopy(enchantmentKeyToAdd);



    }

    public override void ApplyEffect()
    {
        Globals.GetBoardManager().currentMission.AddEnchantment(enchantment);
    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
       // throw new System.NotImplementedException();
    }
}
