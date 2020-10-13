using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePanel : MonoBehaviour
{
    public TileButton tileButton;
    public ScrollListScaleableContent buttonContainer;
    public MapEditingPanel mapEditingPanel;

    SavedDatabase<TileTypes> currTileTypes;

    public string tileselected;



    public void InitTilePanel(Campaign c)
    {
        currTileTypes = c.GetTileData().Tiles;
        PrintButtons();
    }

    public void PrintButtons()
    {
        //buttonContainer.CleanUp();

        foreach (string k in currTileTypes.DbKeys())
        {
            // print the 
            TileButton b = Instantiate<TileButton>(tileButton, buttonContainer.contentTransform);
            b.InitTileButton(k, this);
            buttonContainer.AddToList(b);
        } 
    }

    public void TurnOnFillState()
    {
        mapEditingPanel.drawState = MapEditingState.Fill;
    }

    public void TurnOnDrawState()
    {
        mapEditingPanel.drawState = MapEditingState.Draw;
    }
}
