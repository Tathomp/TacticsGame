using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipItemDataPanel : MonoBehaviour
{
    private EquippableItem currItem;
    private StatsContainer currContainer;
    public StatEditPrefab prefab;
    public ScrollListScaleableContent content;

    public void InitEquippableItemPanel(EquippableItem item)
    {
        gameObject.SetActive(true);

        currItem = item;
        currContainer = currItem.bonusStats;

        PrintData();
    }


    void PrintData()
    {
        content.CleanUp();

       

        foreach (StatTypes types in Enum.GetValues(typeof(StatTypes)))
        {

            StatEditPrefab t = Instantiate<StatEditPrefab>(prefab, content.contentTransform);
            content.gos.Add(t.gameObject);
            t.InitConatinerPanel(currContainer, types);
        }

        content.AdjustContentLength();

    }
}
