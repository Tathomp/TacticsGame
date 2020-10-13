using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreationPanel : MonoBehaviour
{
    public CreationSuiteManager manager;
    public ScrollListScaleableContent itemList;
    public TextButton button_prefab;
    public ItemEditPanel itemEditPanel;


    public void InitPanel()
    {
        gameObject.SetActive(true);
        ItemList();
    }


    void ItemList()
    {
        foreach (string item in manager.currentCampaign.GetAllItems().DbKeys())
        {
            TextButton t = Instantiate<TextButton>(button_prefab, itemList.contentTransform);
            itemList.AddToList(t);
            t.ChangeText(item);
            t.button.onClick.AddListener(delegate { ItemClicked(item); });

        }
    }

    void ItemClicked(string s)
    {
        itemEditPanel.PopulateItemEditPanel(s);
    }

}