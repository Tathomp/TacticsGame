using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyListPanel : MonoBehaviour
{
    public ShopsDetailsPanel panel;

    [HideInInspector]
    public Inventory inv;

    [HideInInspector]
    public ActorData actor;

    [HideInInspector]
    public Item item;

    public TextButton addEquipment, addInventory;
    
    // Data labels
    public TMP_Text statChange, actorName, slotLabel, newWepLabel, currWepLabel;
    public Image actorIcon, newWepIcon, currentWepIcon;

    public void InitPanel(Item i, Inventory inv, ActorData data, EquipmentSlot slot)
    {

        ClearText();

        this.inv = inv;
        this.actor = data;
        this.item = i;
        newWepLabel.text = item.Name;
        actorName.text = data.Name;
        //addEquipment.gameObject.SetActive(addEquipment != null);
        //addInventory.gameObject.SetActive(inv != null);
        addEquipment.gameObject.SetActive(true);

        PrintComparison(slot);
    }

    public void PrintComparison(EquipmentSlot slot)
    {
        ///////////////////////////
        ///Label Data
        slotLabel.text = slot.ToString();


        /////////////////////////////////////
        ///Calculate and store change
        Dictionary<StatTypes, int> change = new Dictionary<StatTypes, int>();

        Item currItem;

        string currItemKey = actor.equipment.GetItemEquipped(slot);


  
        currItem = Globals.campaign.GetItemData(currItemKey);
        
        currWepLabel.text = currItem.Name;

        foreach (StatTypes st in Enum.GetValues(typeof(StatTypes)))
        {
            //A negative delta represnts the new item being weaker than the current ones
            int i = -currItem.GetEquippedItem().bonusStats.GetValue(st) +
                item.GetEquippedItem().bonusStats.GetValue(st);

            if(i != 0) //if I is zero then there is no change in stats
            {
                change.Add(st, i);
            }

        }

        ////////////////////////////
        /// Print the change
        statChange.text = "";
        foreach (StatTypes st in change.Keys.ToArray())
        {
            statChange.text = st.ToString() + ": " + change[st] + "\n";

        }
    }

    public void EquipItemToActor()
    {

        if (panel.EnoughCredits())
        {
            string prevItem = actor.equipment.EquipItem(item, actor);



            if (Globals.campaign.currentparty.partyInvenotry.AddItem(prevItem))
            {
                panel.BuyItemButton();
            }
            else 
            {
                //Basically there's no room for the old equipment in the party's inventory
                //I should just write a method to figure out if a particular item can be add to inventory or somethings
                Debug.Log("oops", this);

            }



        }

    }


    /// <summary>
    /// Inventory Panels
    /// 

    public void InitInventoryPanel(Item i, ActorData data)
    {
        ClearText();
        item = i;
        inv = data.inventory;
        actor = data;

        actorName.text = data.Name;
        addInventory.gameObject.SetActive(true);

        slotLabel.text = "Player's Inventory";
    }
    
    public void ClearText()
    {
        statChange.text = "";
        actorName.text = "";
        slotLabel.text = "";
        newWepLabel.text = "";
        currWepLabel.text = "";

        addInventory.gameObject.SetActive(true);
        addEquipment.gameObject.SetActive(true);

    }


    public void AddItemToInventory()
    {
        if(panel.EnoughCredits())
        {
            if (actor.inventory.AddItem(item.GetKey()))
            {
                panel.BuyItemButton();

            }
            else
            {
                //Basically there's no room for the old equipment in the party's inventory
                //I should just write a method to figure out if a particular item can be add to inventory or somethings
                Debug.Log("oops", this);

            }
        }

    }

}
