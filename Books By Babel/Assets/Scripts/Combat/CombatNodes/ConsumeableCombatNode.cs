using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeableCombatNode : CombatNode
{
    ConsumableItem item;
    Inventory invent;

    public ConsumeableCombatNode(Actor source, TileNode targetedTile, ConsumableItem consumableItem) 
        : base(source, targetedTile)
    {
        item = consumableItem;
        invent = source.actorData.inventory;
    }

    public override void ApplyEffect()
    {
        RemoveItem();

    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        //throw new System.NotImplementedException();
    }

    void RemoveItem()
    {
        List<ItemContainer> items = invent.ItemSlots;

        for (int i = 0; i < items.Count; i++)
        {
           // if(items[i] != null)
            {
                Item temp = Globals.campaign.GetItemData(items[i].itemKey);

                if(temp.HasConsumableEFfect())
                {
                    if(items[i].itemKey == item.itemParentKey)
                    {
                        //here's the key to 
                        invent.UseItem(items[i].itemKey);
                    }
                }
            }
        }
    }
}
