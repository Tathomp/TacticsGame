using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDialog : MonoBehaviour {

    public MapEditorManager editor;
    public InputField input;


    public void SaveMap()
    {
        MapDataModel currData = editor.currBoard;
        currData.mapName = input.text;
        currData.ChangeKey(currData.mapName);
        Globals.campaign.GetMapDataContainer().mapDB.AddEntry(currData);
    }

    public void LoadMap()
    {
        string mapToLoad = input.text;
        editor.currBoard = Globals.campaign.GetMapDataContainer().mapDB.GetCopy(mapToLoad);
        editor.PrintBoard();
        Cancel();
    }

    public void Cancel()
    {
        this.gameObject.SetActive(false);
    }
}
