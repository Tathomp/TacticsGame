using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SellShopButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Image selectImage;
    public TMP_Text itemName, location, price;
    public Button button;

    Inventory inv;
    ActorData data;
    Item currItem;

    //For player equipment
    public void InitPanel(Item item, ActorData data, EquipmentSlottt slot)
    {
        this.data = data;
        this.currItem = item;

        ItemInit(item);

        location.text = data.Name + ": " + slot.slotName;
    }

    //For player inventory
    public void InitPanel(Item item, ActorData data)
    {
        this.data = data;
        this.inv = data.inventory;
        ItemInit(item);

        location.text = data.Name + ": Inventory";

    }
    
    

    //For party inventory
    public void InitPanel(Item item, Inventory inv)
    {
        this.inv = inv;

        ItemInit(item);

        location.text = "Party Inventory";
    }

    public void ItemInit(Item item)
    {
        this.currItem = item;

        itemName.text = item.Name;
        price.text = item.cost + "";

    }

    public void ToggleOn()
    {
        selectImage.enabled = true;
    }

    public void ToggleOff()
    {
        selectImage.enabled = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        ToggleOn();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        ToggleOff();
    }
}
