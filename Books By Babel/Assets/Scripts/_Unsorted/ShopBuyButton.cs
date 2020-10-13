using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopBuyButton : TextButton
{
    public Inventory playerInv;

    public TMP_Text itemName, held, owned, price;
    public Image selectionIcon;

    private Item currItem;

    public void InitButton(Inventory inv, Item item)
    {
        playerInv = inv;
        this.currItem = item;

        PrintText();
    }

    public void PrintText()
    {
        itemName.text = currItem.Name;
        price.text = ""+ currItem.cost;

        int inventoryCount = 0;

        foreach (ActorData item in Globals.campaign.currentparty.partyCharacter)
        {
            foreach (EquipmentSlottt t in item.equipment.GetAllEquipement())
            {
                if(t.itemKey == currItem.GetKey())
                {
                    inventoryCount++;
                }
            }

            foreach (ItemContainer inv in item.inventory.ItemSlots)
            {
                if(inv.itemKey == currItem.GetKey())
                {
                    inventoryCount++;
                }
            }
        }

        held.text = "" + inventoryCount;



        foreach (ItemContainer container in Globals.campaign.currentparty.partyInvenotry.ItemSlots)
        {
            if(container.itemKey == currItem.GetKey())
            {
                inventoryCount++;
            }
        }

        owned.text = "" + (inventoryCount);


    }
    public void ToggleOn()
    {
        selectionIcon.enabled = true;
    }

    public void ToggleOff()
    {
        selectionIcon.enabled = false;
    }
}
