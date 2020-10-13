using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ContentEditingPanel : MonoBehaviour
{
    //Editor
    public CreationSuiteManager creationManager;
    public TextButton button_prefab;

    public ScrollListScaleableContent button_list;

    protected string current_key;

    public void ChangeCurrent_key(string key)
    {
        current_key = key;

        UpdateDataDisplay();
    }


    protected void SpawnButtonAddToListAddChangeMethod(string key, string name)
    {
        SpawnButtonAndAddToList(name, delegate { ChangeCurrent_key(key); });
    }

    protected void SpawnButtonAndAddToList(string name, UnityAction call)
    {
        button_list.AddToList(SpawnNewButton(name, call));
    }

    protected TextButton SpawnNewButton(string name, UnityAction call)
    {
        TextButton button = Instantiate<TextButton>(button_prefab, button_list.contentTransform);
        button.ChangeText(name);


        button.button.onClick.AddListener(call);

        return button;
    }


    protected void ClearList()
    {
        button_list.CleanUp();
    }

    protected abstract void UpdateDataDisplay();
    public abstract void SaveData();
}
