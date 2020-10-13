using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopContainer
{

    List<Shop> ShopPool;

    public ShopContainer()
    {
        ShopPool = new List<Shop>();
    }

    public List<Shop> GetAvaliableShops()
    {
        List<Shop> temp = new List<Shop>();

        foreach (Shop shop in ShopPool)
        {
            if(shop.Unlocked)
            {
                temp.Add(shop);
            }
        }

        return temp;
    }

    public void AddShop(Shop shop)
    {
        ShopPool.Add(shop);
    }
}
