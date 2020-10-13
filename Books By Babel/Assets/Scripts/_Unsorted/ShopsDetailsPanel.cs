using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopsDetailsPanel : MonoBehaviour {

    //ui
    //public TMP_Text ItemDescription;

    public ShopMenuPanel shopMenu;
    public ShopBuyButton buttonPrefab;
    public ShopItemStatsPanel statsPanel;
    public JobsAllowedPanel jobsAllowed;

    public BuyListPanel buylistpanelPrefab;

    public ScrollListScaleableContent buycontent;
    public ScrollListScaleableContent ItemContainer;

    //var
    Shop currShop;
    Item currSelectedItem;
    ShopBuyButton currButton;
    //List<ShopBuyButton> itembuttons;

    public void ShopSelected(Shop shop)
    {
        currShop = shop;

        gameObject.SetActive(true);

        PopulateItemList();

    }

    void PopulateItemList()
    {
        ClearButtons();

        SavedDatabase<Item> itemdb = Globals.campaign.GetAllItems();



        foreach (string key in currShop.AvaliableItemKeys)
        {
            Item i = itemdb.GetCopy(key);
            CreateItemButton(i);
        }

        ItemContainer.AdjustContentLength();
        ItemContainer.buttonConatiner.SelectFirst();

     
    }


    void CreateItemButton(Item item)
    {
        ShopBuyButton temp = Instantiate<ShopBuyButton>(buttonPrefab, ItemContainer.contentTransform);
        temp.ChangeText(item.Name);
        temp.InitButton(null, item);
        temp.button.onClick.AddListener(delegate { ItemButtonClicked(temp, item); });

        //itembuttons.Add(temp);

        ItemContainer.AddToList(temp);
    }


    void ItemButtonClicked(ShopBuyButton b, Item item)
    {
        currSelectedItem = item;
        statsPanel.InitPanel(item);
        jobsAllowed.InitPanel(item);


        b.ToggleOn();

        if (currButton != null)
        {
            currButton.ToggleOff();
        }

        currButton = b;

        currButton.ToggleOn();

        PopulateBuyPanels();
    }

    
    void PopulateBuyPanels()
    {
        buycontent.CleanUp();

        if (currSelectedItem.IsEquippable())
        {
            foreach (ActorData actor in Globals.campaign.currentparty.partyCharacter)
            {

                foreach (EquipmentSlot valid in currSelectedItem.equippEffect.validSlots)
                {
                    BuyListPanel temp = Instantiate<BuyListPanel>(buylistpanelPrefab, buycontent.contentTransform);
                    buycontent.gos.Add(temp.gameObject);
                    temp.panel = this;
                    temp.InitPanel(currSelectedItem, actor.inventory, actor, valid);
                }
            }
        }

        if(currSelectedItem.HasConsumableEFfect())
        {
            foreach (ActorData data in Globals.campaign.currentparty.partyCharacter)
            {

                BuyListPanel temp = Instantiate<BuyListPanel>(buylistpanelPrefab, buycontent.contentTransform);
                buycontent.gos.Add(temp.gameObject);
                temp.panel = this;
                temp.InitInventoryPanel(currSelectedItem, data);

            }
        }
    }



    public bool EnoughCredits()
    {
        int curCredits = Globals.campaign.currentparty.Credits;
        int itemCost = currSelectedItem.cost;

        return (curCredits >= itemCost);
    }

    public void BuyItemButton()
    {
        int curCredits = Globals.campaign.currentparty.Credits;
        int itemCost = currSelectedItem.cost;

        if(curCredits >= itemCost)
        {
            //so instead of adding the item

            Globals.campaign.currentparty.Credits -= itemCost;

            //refresh the UI
            ShopSelected(currShop);
            shopMenu.PrintCredits();

            Debug.Log("Item bought");

        }
        else
        {
            Debug.Log("Item too expensive");
        }

        PopulateItemList();
    }

    void ClearButtons()
    {
        ItemContainer.CleanUp();
        
    }

    private void OnDisable()
    {
        ClearButtons();
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }
}
