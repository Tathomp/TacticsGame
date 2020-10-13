using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapSelector : MonoBehaviour {

    private WorldMap currentMap;
    private WorldMapManager worldMapManager;

    public bool KeyBoardmovement, posChanged;
    public int mapPosX, mapPosY;

    public WorldMapLocationGameObject CurrentLocationNode;

    private Vector3 prevPos, currPos;

    public void InitSelector(WorldMapManager managet)
    {
        worldMapManager = managet;
        this.currentMap = managet.currWorldMap;
    }

    public bool SelectorHasMoved()
    {
        return prevPos == currPos;
    }

    public void ProcessInput(InputHandler inputHandler)
    {
        prevPos = currPos;
        currPos = Input.mousePosition;

        if (currPos.Equals(prevPos))
        {
            KeyBoardmovement = false;
        }
        else
        {
            KeyBoardmovement = true;
        }

        if (KeyBoardmovement)
        {
            Vector2 v2 = Globals.MouseToWorld();
            int x = (int)v2.x;
            int y = (int)v2.y;

            MoveSelectorTo(x, y);
        }

        if (inputHandler.IsKeyPressed(KeyBindingNames.Up))
        {
            StepSelector(0, 1);
        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Right))
        {
            StepSelector(1, 0);
        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Down))
        {
            StepSelector(0, -1);

        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Left))
        {
            StepSelector(-1, 0);
        }

    }

    public void StepSelector(int x, int y)
    {
        MoveSelectorTo(mapPosX + x, mapPosY + y);
    }

    public void MoveSelectorTo(int x, int y)
    {
        if(currentMap.OutOfRange(x,y))
        {
            return;
        }

        mapPosY = y;
        mapPosX = x;

        transform.position = Globals.GridToWorld(mapPosX, mapPosY);

        UpdateLocationNode();
    }


    public void UpdateLocationNode()
    {
        if(currentMap.locations[mapPosX, mapPosY] != null)
        {
            CurrentLocationNode = worldMapManager.locationDisplay[mapPosX, mapPosY];        
            UpdateDisplay();
        }
        else
        {
            CurrentLocationNode = null;
            UpdateDisplay();
        }

    }

    private void UpdateDisplay()
    {
        worldMapManager.worldMapUIManager.ToggleOnWorldMapLocationMenu(CurrentLocationNode);
        worldMapManager.worldMapUIManager.infoPanel.UpdataLocationNodeInfo(CurrentLocationNode);
    }
}
