using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineBattlefieldInputState : BoardInputState
{
    Selector selector;
    List<MapCoords> vaidplacement;

    int currIndex, maxIndex;
    ActorData actorData;

    public ExamineBattlefieldInputState(BoardManager bm, ActorData selectedData)
        : base(bm)
    {
        actorData = selectedData;

        currIndex = boardManager.party.partyCharacter.IndexOf(selectedData);
    }

    public ExamineBattlefieldInputState(BoardManager boardManager) 
        : base(boardManager)
    {
        currIndex = 0;

    }

    public override void EnterState()
    {
        if (boardManager.Selector == null)
        {
            boardManager.Selector = new Selector(boardManager);
        }

        selector = boardManager.Selector;
        vaidplacement = boardManager.currMap.playerSpawnLocations;

        maxIndex = boardManager.party.partyCharacter.Count;
        UpdateSelectedIndex();

        boardManager.tileSelection.ClearAvaliable();
        boardManager.tileSelection.PopulatePlacement(boardManager.currMap.playerSpawnLocations);
    }

    public override void ExitState()
    {

    }

    public override void ProcessInput()
    {
        //CameraControls();

        selector.ProccessInput(inputHandler);
         
        
        if (Input.GetMouseButton(0) || inputHandler.IsKeyPressed(KeyBindingNames.Select))
        {           

            Vector2 pos = Globals.MouseToWorld();
            TileNode currNode = selector.nodeSelected;
            MapCoords t = new MapCoords((int)pos.x, (int)pos.y);

            if (inputHandler.MouseButtonClicked() && (t.X == currNode.data.posX && t.Y == currNode.data.posY) == false)
            {
                selector.ProccessInput(inputHandler);
                return;
            }


        }
        else if (Input.GetMouseButton(1))
        {           
            Debug.Log("Right mouse clicked");

            if (selector.nodeSelected.HasActor())
            {
                //prebattle.partyEditPanel.PopulateCurrentDisplay(selector.nodeSelected.actorOnTile.actorData);
            }
        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.CycleRight))
        {
            //down, increase index
            AdjustIndex(1);
        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.CycleLeft))
        {
            //up the list, reduce index
            AdjustIndex(-1);
        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.PlaceUnit))
        {
            TileNode currNode = selector.nodeSelected;

            //Checks to make sure there's a spawn point and that there's not a unit already there, it spawns it
            //If there is a unit there, remove it then place the new unit
            //If the unit is already on the field, remove it then place it
            if (vaidplacement.Contains(new MapCoords { X = currNode.data.posX, Y = currNode.data.posY }))              
            {

                if(actorData.selected)
                {
                    RemoveUnit(boardManager.pathfinding.GetTileNode(actorData.gridPosX, actorData.gridPosY));
                }

                //There's a unit alrady here, replace it
                if(boardManager.pathfinding.tiles[currNode.data.posX, currNode.data.posY].actorOnTile != null)
                {
                    RemoveUnit(currNode);
                }

                boardManager.spawner.GenerateActor(actorData, boardManager, currNode.data.posX, currNode.data.posY);
                actorData.selected = true;

            }
        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.RemoveUnit))
        {
            if( vaidplacement.Contains(new MapCoords(selector.mapPosX, selector.mapPosY)) && selector.nodeSelected.HasActor())
            {
                RemoveUnit(selector.nodeSelected);
            }
        }
    }

    void SetIndex(int x)
    {
        currIndex = x;

        UpdateSelectedIndex();

    }

    void AdjustIndex(int x)
    {
        int newX = currIndex + x;
        newX = newX % maxIndex;
        if(newX < 0)
        {
            newX = maxIndex - 1;
        }

        SetIndex(newX);

        UpdateSelectedIndex();
    }

    void UpdateSelectedIndex()
    {
        actorData = boardManager.party.partyCharacter[currIndex];
        boardManager.ui.selectedCharacterPanel.currentData = actorData;
        Debug.Log(currIndex);

        boardManager.ui.selectedCharacterPanel.InitSelectedCharacterPanel(actorData);
    }



    void RemoveUnit(TileNode data)
    {
        data.actorOnTile.actorData.selected = false;
        Actor a = data.actorOnTile;
        data.actorOnTile = null;

        boardManager.spawner.RemoveActor(a);
    }
    
}
