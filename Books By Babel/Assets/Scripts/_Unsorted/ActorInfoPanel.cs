using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActorInfoPanel : MonoBehaviour
{
    public BuffPanel BuffPanel;
    public TMP_Text selectorText;


    public void initinfo(Selector selector)
    {
        gameObject.SetActive(true);

        string temp = "";

        if (selector.nodeSelected.actorOnTile != null)
        {
            temp += "\n" + "Name: " + selector.nodeSelected.actorOnTile.actorData.Name;
            temp += "\n" + "Health: " + selector.nodeSelected.actorOnTile.GetCurrentStats(StatTypes.Health) + " / " + selector.nodeSelected.actorOnTile.GetMaxStats(StatTypes.Health);
            temp += "\n" + "Action Points: " + selector.nodeSelected.actorOnTile.GetCurrentStats(StatTypes.NumberOfActions);
            temp += "\n" + "Movement Points: " + selector.nodeSelected.actorOnTile.GetCurrentStats(StatTypes.NumberOfMovements);

            StatTypes[] keys = selector.nodeSelected.actorOnTile.actorData.maxStatCollection.GetKeys();

            foreach (StatTypes key in keys)
            {
                //  temp += key.ToString() + " " + selector.nodeSelected.actorOnTile.actorData.maxStatCollection.statDict[key] + "\n";
            }

            selectorText.text = temp;
            UpdateBuffBar(selector.nodeSelected.actorOnTile.actorData);
        }
        else
        {
            ToggleOff();
        }
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
        BuffPanel.ClearList();
    }

    public void UpdateBuffBar(ActorData data)
    {
        BuffPanel.PopulateBar(data.buffContainer.buffList);
    }
}
