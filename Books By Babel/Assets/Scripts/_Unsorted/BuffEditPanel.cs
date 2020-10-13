using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEditPanel : MonoBehaviour
{
    //Editor
    public ScrollListScaleableContent buffContainer;
    public TextButton buttonPrefab;
    public CreationSuiteManager creationManager;
    public BuffDataPanel datapanel;


    //private
    ButtonListContainer buffList = new ButtonListContainer();
    SavedDatabase<Buff> buffDB;

    public void InitBuffPanel()
    {
        creationManager.SetCurrentActiveObject(this.gameObject);

        buffDB = creationManager.currentCampaign.contentLibrary.buffDatabase;

        PrintList();
    }

    private void BuffButtonClicked(string key)
    {
        datapanel.DisplayBuffData(creationManager.currentCampaign.contentLibrary.buffDatabase.GetData(key));
    }

    private void PrintList()
    {
        CleanrList();

        foreach (string item in buffDB.DbKeys())
        {
            TextButton temp = Instantiate<TextButton>(buttonPrefab, buffContainer.contentTransform);
            temp.ChangeText(item);
            temp.button.onClick.AddListener(delegate { BuffButtonClicked(item); });

            buffList.AddToList(temp);
        }
    }


    private void CleanrList()
    {
        buffList.ClearList();
    }


    private void OnDisable()
    {
        CleanrList();
    }


    
}
