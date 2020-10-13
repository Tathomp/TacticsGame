using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditorPanel : MonoBehaviour {

    //Editor
    GameObject tilePrefab;

    // private
    private ContentLibrary contentLibrary;
    private TileDatabaseContainer currTileDB;

	public void InitPanel(ContentLibrary library)
    {
        contentLibrary = library;
        currTileDB = Globals.campaign.GetTileData();

        gameObject.SetActive(true);
    }

    internal void ExitPanel()
    {
        throw new NotImplementedException();
    }
}
