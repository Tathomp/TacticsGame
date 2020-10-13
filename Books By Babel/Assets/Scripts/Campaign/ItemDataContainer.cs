using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDataContainer
{
    public SavedDatabase<Item> itemDB;
    public SavedDatabase<Shop> ShopDB;

    public ItemDataContainer()
    {
        itemDB = new SavedDatabase<Item>();
        ShopDB = new SavedDatabase<Shop>();
    }

    public Shop GetShopCopy(string key)
    {
        return ShopDB.GetCopy(key);
    }

    public Shop GetShopData(string key)
    {
        return ShopDB.GetData(key);
    }

    public Item GetitemCopy(string key)
    {
        if(key == "")
        {
           
                Item t = new Item("");
            t.Name = "Empty";
            t.DisappearsInventory = true;
                t.equippEffect = new EquippableItem();

                return t;
            

        }

        return itemDB.GetCopy(key);
    }

    public Item GetItemData(string key)
    {
        if (key == "")
        {

            Item t = new Item("empty");

            t.equippEffect = new EquippableItem();

            return t;


        }


        return itemDB.GetData(key);
    }
}
