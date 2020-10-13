using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StealInventoryItemCombatNode : CombatNode
{
    public StealInventoryItemCombatNode(Actor source, TileNode targetedTile) 
        : base(source, targetedTile)
    {

    }

    public override void ApplyEffect()
    {
        if(target != null)
        {

            List<ItemContainer> contains = target.actorData.inventory.ItemSlots;

            if (contains.Count == 0)
            {
                return;
            }

            int index = Random.Range(0, contains.Count - 1);
            string itemToRemove = contains[index].itemKey;

            targetedTile.actorOnTile.actorData.inventory.RemoveItem(itemToRemove);
            source.actorData.inventory.AddItem(itemToRemove);

        }

    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        panel.damageLabel.text = source.actorData.Name + " steals from " + target.actorData.Name;
    }
}
