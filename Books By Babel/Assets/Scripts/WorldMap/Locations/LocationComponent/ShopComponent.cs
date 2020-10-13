using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopComponent : LocationComponent
{
    public string ShopName { get; protected set; }

    //private Shop shop;

    public ShopComponent(string shopname)
    {
        this.ShopName = shopname;
    }

    public override TextButton GenerateButtion(TextButton button, WorldMapLocationMenu menu)
    {

        Shop shop = Globals.campaign.GetItemDataContainer().GetShopData(ShopName);


        button.button.onClick.AddListener(delegate { ShopButtonClicked(menu, shop); });
        button.ChangeText( shop.ShopName);

        return button;
    }

    public void ShopButtonClicked(WorldMapLocationMenu menu, Shop shop)
    {
        menu.ToggleOffPanels();
        menu.shopPanel.InitMenu(shop);
    }

    public override string GetDescription()
    {
        Shop shop = Globals.campaign.GetItemDataContainer().GetShopData(ShopName);

        return "Shop: "+shop.ShopName + "\n";
    }
}
