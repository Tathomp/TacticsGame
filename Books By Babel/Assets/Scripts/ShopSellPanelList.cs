using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSellPanelList : MonoBehaviour
{
    public SellShopButton sellButtonPrefab;
    public ScrollListScaleableContent shopSellCoontainer;
    public ShopItemStatsPanel itemDetailPanel;
    public JobsAllowedPanel jobsAllowed;

    Shop currShop;

    ItemContainer currItem;
    ActorData itemLocation;

    public void ShopSelected(Shop shop)
    {
        gameObject.SetActive(true);
        this.currShop = shop;

        PopulateItemList();
    }

    public void PopulateItemList()
    {
        shopSellCoontainer.CleanUp();
        SellShopButton SSB = null;
        

        foreach (ActorData actorData in Globals.campaign.currentparty.partyCharacter)
        {


            foreach (EquipmentSlottt es in actorData.equipment.GetAllEquipement())
            {


                SellShopButton i = Instantiate<SellShopButton>(sellButtonPrefab, shopSellCoontainer.contentTransform);
                shopSellCoontainer.gos.Add(i.gameObject);
                Item it = Globals.campaign.GetItemData(es.itemKey);
                i.InitPanel(it, actorData, es);
                i.button.onClick.AddListener(delegate { SellButtonClicked(it, es, actorData); }); //make this take an item etc

                if (SSB == null)
                {
                    SSB = i;
                }
            }

            foreach (ItemContainer item in actorData.inventory.GetAllItems())
            {
                SellShopButton i = Instantiate<SellShopButton>(sellButtonPrefab, shopSellCoontainer.contentTransform);
                shopSellCoontainer.gos.Add(i.gameObject);
                Item it = Globals.campaign.GetItemData(item.itemKey);

                i.InitPanel(it, actorData);
                i.button.onClick.AddListener(delegate { SellButtonClicked(it, item, actorData); }); //make this take an item etc

                if (SSB == null)
                {
                    SSB = i;
                }
            }

        }

        Inventory inventory = Globals.campaign.currentparty.partyInvenotry;

        foreach (ItemContainer item in inventory.GetAllItems())
        {
            SellShopButton i = Instantiate<SellShopButton>(sellButtonPrefab, shopSellCoontainer.contentTransform);
            shopSellCoontainer.gos.Add(i.gameObject);
            Item it = Globals.campaign.GetItemData(item.itemKey);

            i.InitPanel(it, inventory);
            i.button.onClick.AddListener(delegate { SellButtonClicked(it, item, null); }); //make this take an item etc

            if (SSB == null)
            {
                SSB = i;
            }

        }

        if(SSB != null)
        {
            SSB.button.onClick.Invoke();
            SSB.ToggleOn();
        }
    }

    public void SellItem()
    {

        Globals.campaign.currentparty.Credits += Globals.campaign.GetItemData(currItem.itemKey).cost;

        if(currItem is EquipmentSlottt)
        {
            //actor's equipment
            itemLocation.equipment.UnequipItem(itemLocation, currItem as EquipmentSlottt);
        }
        else if(itemLocation == null)
        {
            //party's inventory
            Globals.campaign.currentparty.partyInvenotry.RemoveItem(currItem);
        }
        else
        {
            //actor's inventory
            itemLocation.inventory.RemoveItem(currItem);
        }

        PopulateItemList();
    }


    public void SellButtonClicked(Item it, ItemContainer currContainer, ActorData actor)
    {
        //
        currItem = currContainer;
        itemLocation = actor;

        jobsAllowed.InitPanel(it);
        itemDetailPanel.InitPanel(it);
    }
}
