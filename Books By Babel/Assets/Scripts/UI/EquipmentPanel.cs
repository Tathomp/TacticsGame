using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanel : CharacterInfoDisplayPanel {

    ActorData actorData;
    Party party;

    Inventory playerInventory, partyInventory;
    Equipment actorEquipment;

    ItemContainer playerContainer, newContinaer;

    public TextButton helmButton, primaryWepButotn;

    public TextButton ItemPrefab;
    public ScrollListScaleableContent listTransforom, playerInventoryContainer;
    public TMP_Text itemInfo;

    public TextButton equipButotn;

    public List<TextButton> inventoryLIst, actorInventory = new List<TextButton>();

    public override void PopulatePanel(ActorData data)
    {
        gameObject.SetActive(true);

        ClearButotns();

        inventoryLIst = new List<TextButton>();

        actorData = data;
        actorEquipment = data.equipment;
        playerInventory = data.inventory;

        party = Globals.campaign.currentparty;
        partyInventory = party.partyInvenotry;

        InitSlotButtons();
        PrintInventorySlots();
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        party.partyInvenotry = partyInventory;

        //SaveLoadManager.SavePartyInfo(party);
        List<ActorData> ad = Globals.campaign.currentparty.partyCharacter;

        for (int i = 0; i < ad.Count; i++)
        {
            if(ad[i].ID == actorData.ID)
            {
                ad[i] = actorData;
                break;
            }
        }


        ClearButotns();
    }

    #region Inventory Handlers
    //Party Inventory
    public void PopulateItemList(ItemContainer container)
    {
        ClearInventoryList();

        foreach (ItemContainer newSlot in partyInventory.ItemSlots)
        {
            TextButton b = Instantiate<TextButton>(ItemPrefab, listTransforom.contentTransform);
            b.ChangeText(newSlot.itemKey);
            b.button.onClick.AddListener(delegate { ItemListSlotClicked(container, newSlot); });
            inventoryLIst.Add(b);
        }

        playerInventoryContainer.AdjustContentLength();

    }

    public void ItemListSlotClicked(ItemContainer dest, ItemContainer newSlot)
    {
        PrintStats(dest, newSlot);
    }

    public void InventorySlotClickedOn(ItemContainer container)
    {
        PopulateItemList(container);
    }

    //Player's Inventroy
    //
    public void PrintInventorySlots()
    {
        ClearActorInventoryList();

        foreach (ItemContainer item in actorData.inventory.ItemSlots)
        {
            TextButton b = Instantiate<TextButton>(ItemPrefab, playerInventoryContainer.contentTransform);
            b.ChangeText(item.itemKey);
            b.button.onClick.AddListener(delegate { InventorySlotClickedOn(item); });
            actorInventory.Add(b);
        }

        playerInventoryContainer.AdjustContentLength();

    }

    #endregion
    public void PopulateItemList(EquipmentSlot slot)
    {
        ClearInventoryList();
        List<string> itemlist = partyInventory.GetEquippables(slot);

        foreach (string e in itemlist)
        {
            TextButton temp = Instantiate<TextButton>(ItemPrefab, listTransforom.contentTransform);
            temp.ChangeText(Globals.campaign.GetItemData(e).Name);

            AddDelegate(temp, Globals.campaign.GetItemData(e), slot);

            inventoryLIst.Add(temp);
        }

        listTransforom.AdjustContentLength();

    }

    public void EquipHeadSlot(Item item)
    {
        string temp = actorEquipment.EquipItem(item, actorData);

        if(temp != null)
        {
            partyInventory.AddItem(temp);
        }

        partyInventory.RemoveItem(item.GetKey());
        gameObject.SetActive(false);
        PopulatePanel(actorData);
    }

    void ChangePrimarySlot(Item item)
    {
        string temp = actorEquipment.EquipItem(item, actorData);

        if(temp != null)
        {
            partyInventory.AddItem(temp);
        }

        partyInventory.RemoveItem(item.GetKey());
        gameObject.SetActive(false);
        PopulatePanel(actorData);
    }


    void InitSlotButtons()
    {
        primaryWepButotn.button.onClick.AddListener(delegate { PopulateItemList(EquipmentSlot.MainHand); });
        helmButton.button.onClick.AddListener(delegate { PopulateItemList(EquipmentSlot.Head); });

        if(actorEquipment.GetPrimaryWeapon() != "")
        {
           primaryWepButotn.ChangeText("Primary: " + Globals.campaign.GetItemData(actorEquipment.GetPrimaryWeapon()).Name);
        }
        else
        {
            primaryWepButotn.ChangeText("Primary: ---");

        }

        if (actorEquipment.GetHeadItem() != "")
        {
            helmButton.ChangeText( "Head: " + Globals.campaign.GetItemData(actorEquipment.GetHeadItem()).Name);
        }
        else
        {
            helmButton.ChangeText("Head: ---");

        }

        PrintInventorySlots();
    }

    
    void AddDelegate(TextButton button, Item item, EquipmentSlot slot)
    {
        button.button.onClick.AddListener(delegate { PrintStats(item, slot); });
    }

    void PrintStats(ItemContainer current, ItemContainer newContainer)
    {
        if (current.itemKey != "")
        {
            Item currItem = Globals.campaign.GetItemData(current.itemKey);
        }

        if (newContainer.itemKey != "")
        {
            Item newItem = Globals.campaign.GetItemData(newContainer.itemKey);

            string s = newItem.Name + "\n\n";
            s += newItem.descript + "\n\n";
            //s += "Stat Bonuses:\n" + newItem.GetEquippedItem().GetBonusStats().PrintStats();
            itemInfo.text = s;
        }




        equipButotn.button.onClick.RemoveAllListeners();

        equipButotn.button.onClick.AddListener(delegate { SwapContainers(current, newContainer); });
    }


    void SwapContainers(ItemContainer current, ItemContainer newConatiner)
    {
        ItemContainer temp = new ItemContainer("", 0);

        temp.currCapcity = current.currCapcity;
        temp.itemKey = current.itemKey;

        current.currCapcity = newConatiner.currCapcity;
        current.itemKey = newConatiner.itemKey;


        newConatiner.currCapcity = temp.currCapcity;
        newConatiner.itemKey = temp.itemKey;

        PrintInventorySlots();
        PopulateItemList(current);
    }

    void PrintStats(Item item, EquipmentSlot slot)
    {
        string s =  item.Name + "\n\n";
        s += item.descript +"\n\n";
        s+= "Stat Bonuses:\n" + item.GetEquippedItem().GetBonusStats().PrintStats();
        itemInfo.text = s;

        equipButotn.button.onClick.RemoveAllListeners();

        equipButotn.button.onClick.AddListener(delegate { EquipItem(item, slot); });
    }


    void EquipItem(Item item, EquipmentSlot slot)
    {
        if(slot.Equals(EquipmentSlot.Head))
        {
            EquipHeadSlot(item);
        }
        else
        {
            ChangePrimarySlot(item);
        }

        PopulateItemList(slot);

    }

    void ClearButotns()
    {
        primaryWepButotn.button.onClick.RemoveAllListeners();
        helmButton.button.onClick.RemoveAllListeners();
        ClearInventoryList();
        equipButotn.button.onClick.RemoveAllListeners();
    }


    void ClearInventoryList()
    {
        int count = inventoryLIst.Count - 1;

        for (int i = count; i >= 0; i--)
        {
            inventoryLIst[i].button.onClick.RemoveAllListeners();
            Destroy(inventoryLIst[i].gameObject);
            Destroy(inventoryLIst[i]);
        }

        inventoryLIst = new List<TextButton>();
    }

    void ClearActorInventoryList()
    {
        int count = actorInventory.Count - 1;

        for (int i = count; i >= 0; i--)
        {
            actorInventory[i].button.onClick.RemoveAllListeners();
            Destroy(actorInventory[i].gameObject);
            Destroy(actorInventory[i]);
        }

        actorInventory = new List<TextButton>();
    }
}
