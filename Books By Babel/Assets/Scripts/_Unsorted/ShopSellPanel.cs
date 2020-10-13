using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//TODO: DELETE
public class ShopSellPanel : MonoBehaviour
{
    //ui
    public ScrollListScaleableContent sourceContainer;
    public ScrollListScaleableContent itemContainer;
    public TMP_Text itemName, price;

    //prefab
    public TextButton buttonPrefab;

    //var
    private Shop currShop;
    private List<TextButton> buttonList = new List<TextButton>();
    private List<TextButton> itemButtonList = new List<TextButton>();

    private ActorData currData;
    private SellItemType type;
    private ItemContainer itemKey;
    private int indexToRemove;

    public void InitShopSellPanel(Shop curr)
    {
        gameObject.SetActive(true);

        currShop = curr;

        PrintPartyList();
    }


    public void PrintPartyList()
    {
        ClearPartyButtons(buttonList);
        buttonList = new List<TextButton>();
        PrintPartyInventory();

        foreach (ActorData data in Globals.campaign.currentparty.partyCharacter)
        {
            TextButton temp = Instantiate<TextButton>(buttonPrefab, sourceContainer.contentTransform);
            temp.ChangeText(data.Name);
            temp.button.onClick.AddListener(delegate { PrintItemsForActors(data); });
            buttonList.Add(temp);

        }

        sourceContainer.AdjustContentLength();
    }

    public void PrintItemsForActors(ActorData data)
    {
        currData = data;
        ClearPartyButtons(itemButtonList);
        itemButtonList = new List<TextButton>();
        //pritn items for equipement, personal inventory
        int i = 0;

        foreach (EquipmentSlottt it in data.equipment.GetAllEquipement())
        {
            string key = it.itemKey;

            i++;
            TextButton t = Instantiate<TextButton>(buttonPrefab, itemContainer.contentTransform);
            t.ChangeText(it.itemKey);
            t.button.onClick.AddListener(delegate { ItemClicked(it, SellItemType.Equipment, i); });
            itemButtonList.Add(t);

        }

        i = 0;
        //inventory
        foreach (ItemContainer key in data.inventory.ItemSlots)
        {

            i++;
            TextButton t = Instantiate<TextButton>(buttonPrefab, itemContainer.contentTransform);
            t.ChangeText(key.itemKey);
            t.button.onClick.AddListener(delegate { ItemClicked(key, SellItemType.Personal, i); });
            itemButtonList.Add(t);
        }

        itemContainer.AdjustContentLength();
    }

    public void SellButtonClicked()
    {
        Globals.campaign.currentparty.Credits += SellItemValue(itemKey.itemKey);

        switch (type)
        {
            case SellItemType.Equipment:
                {
                    currData.equipment.UnequipItem(currData, itemKey as EquipmentSlottt);
                    break;
                }
            case SellItemType.Personal:
            case SellItemType.Party:
                {
                    itemKey.itemKey = "";
                    break;
                }
        }

        PrintItemsForActors(currData);
    }

    public void PrintPartyInventory()
    {
        currData = null;
    }

    public void ItemClicked(ItemContainer key, SellItemType typeToSet, int index)
    {


        if (key.itemKey != "")
        {
            indexToRemove = index;
            type = typeToSet;
            itemKey = key;

            itemName.text = key.itemKey;
            price.text = "" + SellItemValue(key.itemKey);
        }
    }

    public void ClearPartyButtons(List<TextButton> btns)
    {      

        int count = btns.Count - 1;

        for (int i = count; i >= 0; i--)
        {
            Destroy(btns[i].gameObject);
            Destroy(btns[i]);
        }


    }


    public int SellItemValue(string key)
    {
        return Formulas.SellValue(key);
    }
}


public enum SellItemType
{
    Personal,
    Equipment,
    Party
}

