using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditMapInputState : CreationSuiteInputState
{
    CreationSuiteManager creationSuite;
    string newTileType;

    MapEditingPanel mapEditingPanel;

    public EditMapInputState(CreationSuiteManager mep, string tileTypeChange)
    {
        creationSuite = mep;
        newTileType = tileTypeChange;

        mapEditingPanel = creationSuite.mapEditingPanel;
    }

    public override void EnterState()
    {

    }


    public override void ExitState()
    {
        
    }

    public override void ProcessInput()
    {
        if(Input.GetMouseButton(0))
        {
           Vector2 v2 = Globals.MouseToWorld();
            int x = (int)v2.x;
            int y = (int)v2.y;

            Debug.Log(x + " " + y + " ");

            if (mapEditingPanel.InBounds(x, y))
            {
                if(mapEditingPanel.drawState == MapEditingState.Fill)
                {
                    mapEditingPanel.FillMap(x, y, newTileType);
                }
                else if(mapEditingPanel.drawState == MapEditingState.Draw)
                {
                    mapEditingPanel.tileBoard[x, y].ChangeTileType(newTileType);
                    mapEditingPanel.mapDataModel.tileBoard[x, y] = newTileType;
                }


            }
        }
    }
}
