using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjureItemInventoryCombatNode : CombatNode
{
    private string item_key;

    public ConjureItemInventoryCombatNode(Actor source, TileNode targetTile, string itemKey) : base(source, targetTile)
    {
        this.item_key = itemKey;
    }


    public override void ApplyEffect()
    {        
        if(targetedTile.actorOnTile.actorData.inventory.AddItem(item_key) == false)
        {
            //Item wasn't added to inventory;
            //
            if(target.actorData.controller.PlayerControlled())
            {
                //we can add it to the party's inventory;
                Globals.campaign.currentparty.AddItemToIventory(item_key);
            }
        }

    }


    public override void UpDatePreview(PreviewUIPanel panel)
    {
        panel.damageLabel.text = "Produces: " + item_key + " for " + targetedTile.actorOnTile.name;

    }
}
