using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileButton : TextButton {

    // public MapEditorManager editor;
    public string selectedTileType;
    public TilePanel tilePanel;

    public void TileButtonClick()
    {
        tilePanel.tileselected = selectedTileType;


        tilePanel.mapEditingPanel.manager.inputStateMachine.SwitchState(new EditMapInputState(
            tilePanel.mapEditingPanel.manager, selectedTileType));
    }

    public void InitTileButton(string tileType, TilePanel panel)
    {
        selectedTileType = tileType;
        tilePanel = panel;

        ChangeText( "" );

        transform.GetComponent<Image>().sprite = Globals.GetSprite(FilePath.TileSetAtlas, tileType);

        /*
         */

    }



}
