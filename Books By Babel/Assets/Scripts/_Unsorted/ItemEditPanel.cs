using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ItemEditPanel : MonoBehaviour
{
    public CreationSuiteManager manager;

    private Item currItem;

    public TMP_InputField itemName, description, cost, maxstacks;
    public TMP_Dropdown itemTypeDropdown;
    public Toggle chargeItem, disappearsInventory;

    public EquipItemDataPanel equippanel;

    public void PopulateItemEditPanel(string s)
    {
        Debug.Log("Clicked:");
        currItem = manager.currentCampaign.GetItemData(s);

        itemName.text = currItem.Name;
        description.text = currItem.descript;
        cost.text = currItem.cost + "";
        maxstacks.text = currItem.maxStack + "";

        chargeItem.isOn = currItem.ChargeItem;
        disappearsInventory.isOn = currItem.DisappearsInventory;

        ItemType();

        if(currItem.IsEquippable())
        equippanel.InitEquippableItemPanel(currItem.equippEffect);
    }

    void ItemType()
    {
        itemTypeDropdown.ClearOptions();
        List<string> list = new List<string>();

        foreach (ItemType s in Enum.GetValues(typeof(ItemType)))
        {
            list.Add(s.ToString());
        }

        itemTypeDropdown.AddOptions(list);
    }


    void SaveItem()
    {

    }
}
