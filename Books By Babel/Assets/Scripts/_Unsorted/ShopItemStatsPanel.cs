using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItemStatsPanel : MonoBehaviour
{
    private Item currentItem;

    public TMP_Text equip, consume, activate, itemDescription;

   public void InitPanel(Item curr)
    {
        this.currentItem = curr;

        string s = "Credits: " + Globals.campaign.currentparty.Credits + "\n\n\n\n" + currentItem.descript;

        itemDescription.text = s;

        if(currentItem.IsEquippable())
        {
            equip.text = "Equipable" + "\n";
            string statData = currentItem.equippEffect.bonusStats.PrintStats();

            equip.text = statData;

        }

        if (currentItem.HasConsumableEFfect())
        {
            consume.gameObject.SetActive(true);

            s = "Consumable" + "\n";

            s += currentItem.consumeableEffect.ActivatableSkill.GetName();
            s += "\n" + currentItem.consumeableEffect.ActivatableSkill.descript;

            consume.text = s;
        }

        if (currentItem.HasActivationEffect())
        {
            activate.gameObject.SetActive(true);

            s = "Activation" + "\n";
            s += currentItem.activationEffect.ActivatableSkill.GetName();
            s += "\n" + currentItem.activationEffect.ActivatableSkill.descript;

            activate.text = s;
        }

        equip.gameObject.SetActive(currentItem.IsEquippable());
        consume.gameObject.SetActive(currentItem.HasConsumableEFfect());
        activate.gameObject.SetActive(currentItem.HasActivationEffect());
    }


}
