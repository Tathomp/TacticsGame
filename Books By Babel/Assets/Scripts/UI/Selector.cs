using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector
{
    GameObject selectionGo;
    Pathfinding pathfindingBoard;
    BattleUIManager SelectionDisplay;


    public TileNode nodeSelected;

    public int mapPosX, mapPosY;

    public bool KeyBoardmovement, posChanged;

    Vector3 prevPos, currPos;
   

    public Selector(BoardManager bm)
    {
        prevPos = Input.mousePosition;
        currPos = Input.mousePosition;

        this.SelectionDisplay = bm.ui;
        pathfindingBoard = bm.pathfinding;

        selectionGo = Resources.Load<GameObject>("BaseObjects/Selector");
        // selectionGo = GameObject.Instantiate(selectionGo, Globals.GridToWorld(pathfindingBoard.GetTileNode(0,0)), Quaternion.identity);
        selectionGo = GameObject.Instantiate(selectionGo, bm.ui.transform);
        MoveTo(1, 1);
    }

    

    public void Step(int x, int y)
    {
        int tempX = mapPosX + x;
        int tempY = mapPosY + y;

        if(pathfindingBoard.InRange(tempX, tempY))
        {
            mapPosX = tempX;
            mapPosY = tempY;
            AdjustPostion();

        }

    }


    public void MoveTo(int x, int y)
    {
        int tempX = x;
        int tempY = y;

        if (pathfindingBoard.InRange(tempX, tempY))
        {
            mapPosX = tempX;
            mapPosY = tempY;
            AdjustPostion();

        }
    }


    public void MoveTo(float x, float y)
    {
        MoveTo((int)x, (int)y);
    }

    void AdjustPostion()
    {
        nodeSelected = pathfindingBoard.GetTileNode(mapPosX, mapPosY);

        selectionGo.transform.position = Globals.GridToWorld(nodeSelected);
        SelectionDisplay.tileselctionPanel.UpdateText(this);
        SelectionDisplay.actorInfoPanel.initinfo(this);
        posChanged = true;
    }

    public void UpdatePosition()
    {
        Vector2 v2 = Globals.MouseToWorld();

        MoveTo((int)v2.x, (int)v2.y);
    }


    public bool ChangedPosition()
    {
        return posChanged;
    }

    public void ProcessKeyboardInput(InputHandler inputHandler)
    {
        KeyBoardmovement = true;

        if (inputHandler.IsKeyPressed(KeyBindingNames.Up))
        {
            Step(0, 1);
        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Right))
        {
            Step(1, 0);
        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Down))
        {
            Step(0, -1);

        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Left))
        {
            Step(-1, 0);
        }
        else
        {
            currPos = Globals.MouseToWorld();

            if(currPos != prevPos)
            {
                prevPos = currPos;
                int x = (int)currPos.x;
                int y = (int)currPos.y;

                MoveTo(x, y);
            }
        }

    }

    public void ProccessInput(InputHandler inputHandler)
    {
        KeyBoardmovement = true;

        if (inputHandler.IsKeyPressed(KeyBindingNames.Up))
        {
            Step(0, 1);
        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Right))
        {
            Step(1, 0);
        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Down))
        {
            Step(0, -1);

        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Left))
        {
            Step(-1, 0);
        }
        else
        {
            currPos = Globals.MouseToWorld();

            if (currPos != prevPos)
            {
                prevPos = currPos;
                int x = (int)currPos.x;
                int y = (int)currPos.y;

                MoveTo(x, y);
            }
        }

    }

    public bool PositionChanged()
    {
        prevPos = currPos;
        currPos = Input.mousePosition;

        return (currPos.Equals(prevPos));
    }
}
