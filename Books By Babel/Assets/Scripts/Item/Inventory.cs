using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    List<ItemContainer> items;
    public int capcity;

    int defaultCap;

	public Inventory(int defaultCap = 5)
    {
        capcity = defaultCap;
        items = new List<ItemContainer>();
        this.defaultCap = defaultCap;

        for (int i = 0; i < defaultCap; i++)
        {
            items.Add(new ItemContainer("", 0));
        }
    }

    public void UseItem(string item)
    {
        Item temp = Globals.campaign.GetItemData(item);

        if(temp.ChargeItem)
        {
            foreach (ItemContainer itemContainer in items)
            {
                if(itemContainer.itemKey == item && itemContainer.currCapcity >0)
                {
                    itemContainer.currCapcity--;
                    return;
                }
            }
        }
        else
        {
            RemoveItem(item);
        }
    }

    public bool IsThereSpaceInventory()
    {

        return GetAvaliableSlot() != null;
    }

    public void AddToEmptySlot(ItemContainer c)
    {
        ItemContainer openslot = GetAvaliableSlot();

        openslot.itemKey = c.itemKey;
        openslot.currCapcity = c.currCapcity;

    }

    public ItemContainer GetAvaliableSlot()
    {
        foreach (ItemContainer item in items)
        {
            if (item.itemKey == "")
            {
                return item;
            }
        }

        return null;
    }

    public void RemoveItem(ItemContainer container)
    {
        foreach (ItemContainer item in items)
        {
            if(item == container)
            {
                item.currCapcity--;
                if(item.currCapcity <= 0)
                {
                    item.itemKey = "";
                }
            }
        }
    }

    public void RemoveItem(string item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].itemKey == item)
            {

                items[i].currCapcity--;
                if(items[i].currCapcity < 1)
                {
                    items.Remove(items[i]);

                }
                return;
            }
        }

    }

    public List<ItemContainer> GetAllItems()
    {
        List<ItemContainer> t = new List<ItemContainer>();

        foreach (ItemContainer itemContainer in items)
        {
            if(itemContainer.itemKey != "")
            {
                t.Add(itemContainer);
            }
        }

        return t;
    }


    public List<ItemContainer> ItemSlots => items;

    public List<string> GetEquippables(EquipmentSlot slot)
    {
        List<string> temp = new List<string>();

        foreach (ItemContainer item in items)
        {
            string key = item.itemKey;

            if (key != "")
            {
                Item itemObj = Globals.campaign.GetItemCopy(key);

                if (itemObj.IsEquippable())
                {
                    EquippableItem te = itemObj.GetEquippedItem();

                    if (te.ValidSlot(slot))
                    {
                        temp.Add(key);
                    }
                }
            }
        }

        return temp;

    }

    //Was item successfully added?
    public bool AddItem(string item)
    {
        Item temp = Globals.campaign.GetItemCopy(item);


        if (temp.DisappearsInventory == true)
        {
            return true;
        }


        if(temp.StackableItem())
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].itemKey == item && items[i].currCapcity < temp.maxStack)
                {
                    items[i].currCapcity++;
                    return true;
                }
            }
        }
               

        if (IsThereSpaceInventory())
        {
            if (temp.ChargeItem)
            {
                //we never want to stack items with charges on top of 
                AddToEmptySlot(new ItemContainer(item, temp.maxStack));
                return true;

            }
            else
            {
                AddToEmptySlot(new ItemContainer(item, 1));
                return true;

            }
        }

        return false;

        // items.Add(item);
    }

    public Inventory Copy()
    {
        Inventory i = new Inventory();
        i.items = new List<ItemContainer>();
        foreach (ItemContainer itemContainer in items)
        {
            i.items.Add(itemContainer.Copy());
        }

        i.capcity = capcity;
        i.defaultCap = defaultCap;

        return i;
    }

    public List<IUseable> GetAllUseableItems()
    {
        List<IUseable> useable = new List<IUseable>();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].currCapcity > 0)
            {
                Item item = Globals.campaign.GetItemDataContainer().itemDB.GetData(items[i].itemKey) as Item;



                if (item.HasConsumableEFfect())
                {
                    useable.Add(item.GetConsumableEffect());
                }

                if (item.HasActivationEffect())
                {
                    useable.Add(item.GetActivateableEffect());
                }
                
            }
        }

        return useable;
    }


    public void ResetCharges()
    {

        foreach (ItemContainer itemContainer in items)
        {
            if (itemContainer.itemKey != "")
            {
                Item temp = Globals.campaign.GetItemData(itemContainer.itemKey);

                if (temp.ChargeItem)
                {
                    itemContainer.currCapcity = temp.maxStack;
                }
            }
        }

    }

}
