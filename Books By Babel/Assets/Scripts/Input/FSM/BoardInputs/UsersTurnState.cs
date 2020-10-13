using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersTurnState : BoardInputState
{
    Selector selector;
    Actor currentActor;
    Pathfinding pathfindingBoard;
    TurnManager turnManager;

    ToolbarPanel pane;
    PartyEditPanel infopanel;


    public UsersTurnState(BoardManager boardManager)
        : base(boardManager)
    {
        if (boardManager.Selector == null)
        {
            boardManager.Selector = new Selector(boardManager);
        }

        selector = boardManager.Selector;

        pathfindingBoard = boardManager.pathfinding;
        turnManager = boardManager.turnManager;

        pane = boardManager.ui.hotbarPanel;
        infopanel = boardManager.ui.characterInfoPanel;
        currentActor = turnManager.currFastest;
    }


    public override void EnterState()
    {
        boardManager.tileSelection.ClearAllRange();
        
        if (turnManager.currFastest != null)
        {
            if (turnManager.currFastest.ActorsController().PlayerControlled() 
                && !turnManager.currFastest.CanMove() 
                && !turnManager.currFastest.CanAttack())
            {
                // turnManager.currFastest.Wait();
                // turnManager.CalculateFastest();
            }

            boardManager.ui.hotbarPanel.GenerateDefaultHotBar(currentActor.actorData);
        }

        boardManager.CheckEventsAndCompletion();
       // boardManager.CheckEventsAndCompletion();
    }


    public override void ExitState()
    {

    }


    public override void ProcessInput()
    {

        CameraControls();

        if (Input.GetMouseButtonDown(0))
        {
            selector.ProccessInput(inputHandler);

            UnitSelected();


        }
        else if (inputHandler.SelectionPressed())
        {
            UnitSelected();
        }
        else if(Input.GetMouseButton(1))
        {
            //This is our alt click 
            //We need to get the actor at this position
            /*
            if(selector.nodeSelected.actorOnTile != null)
            {
                inputFSM.SwitchState(new CharacterInfoState(boardManager,
                    selector.nodeSelected.actorOnTile));

            }*/
            selector.ProccessInput(inputHandler);
            
            inputFSM.SwitchState(new ActionMenuState(boardManager, currentActor)); //this might cause problems in the future idk

            boardManager.ui.actionMenu.InitAltSelection(selector.nodeSelected, boardManager.currentMission);

        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.Cancel))
        {
            inputFSM.SwitchState(new OptionsMenuState(boardManager));
        }

        AttackBasedInputs();

        MovementBaseInputs();
    }





    private void UnitSelected()
    {
        if (selector.nodeSelected.actorOnTile == turnManager.currFastest)
        {
            Debug.Log("it's this unit's turn");
            //display movement range
            //shift to a new state?
            inputFSM.SwitchState(new ActionMenuState(boardManager, selector.nodeSelected.actorOnTile));

        }
        else
        {
            Debug.Log("it's not this unit's turn");
            //  inputFSM.boardManager.DisplaySelectionRange(selector.nodeSelected.actorOnTile);
        }
    }


    void MovementBaseInputs()
    {
        if(currentActor.CanMove())
        {
            if(inputHandler.IsKeyPressed(KeyBindingNames.MovementHotKey))
            {
                inputFSM.SwitchState(new MoveSelectionState(boardManager, currentActor));
            }
        }
    }


    void AttackBasedInputs()
    {
        if(currentActor.CanAttack())
        {
            if (inputHandler.IsKeyPressed(KeyBindingNames.SkillKey1))
            {
                SkillHotBarUsed(0);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.SkillKey2))
            {
                SkillHotBarUsed(1);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.SkillKey3))
            {
                SkillHotBarUsed(2);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.SkillKey4))
            {
                SkillHotBarUsed(3);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.SkillKey5))
            {
                SkillHotBarUsed(4);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.SkillKey6))
            {
                SkillHotBarUsed(5);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.SkillKey7))
            {
                SkillHotBarUsed(6);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.SkillKey8))
            {
                SkillHotBarUsed(7);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.SkillKey9))
            {
                SkillHotBarUsed(8);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.SkillKey10))
            {
                SkillHotBarUsed(9);
            }
            else if(inputHandler.IsKeyPressed(KeyBindingNames.InventoryKey1))
            {
                UseInventory(0);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.InventoryKey2))
            {
                UseInventory(1);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.InventoryKey3))
            {
                UseInventory(2);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.InventoryKey4))
            {
                UseInventory(3);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.InventoryKey5))
            {
                UseInventory(4);
            }
        }

        if(currentActor.CanMove())
        {
            if(inputHandler.IsKeyPressed(KeyBindingNames.MovementHotKey))
            {
                inputFSM.SwitchState(new MoveSelectionState(boardManager, currentActor));
            }
        }

        if(inputHandler.IsKeyPressed(KeyBindingNames.WaitHotKey))
        {
            currentActor.Wait();
            turnManager.CalculateFastest();
        }
    }


    void SkillHotBarUsed(int pos)
    {
        if(pane.skillhotbar[pos].useable != null)
        {
            Debug.Log("hotkey pressed");

            inputFSM.SwitchState(new AbilityInUseState(boardManager, currentActor, pathfindingBoard.GetTileNode(currentActor), pane.skillhotbar[pos].useable));

        }
    }

    void UseInventory(int pos)
    {
        if(pane.inventoryhotbar[pos].useable != null)
        {
            Debug.Log("hotkey pressed");

            inputFSM.SwitchState(new AbilityInUseState(boardManager, currentActor, pathfindingBoard.GetTileNode(currentActor), pane.inventoryhotbar[pos].useable));

        }
    }

}
