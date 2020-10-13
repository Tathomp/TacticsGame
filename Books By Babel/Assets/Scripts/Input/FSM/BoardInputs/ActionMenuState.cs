using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuState : BoardInputState
{
    Actor currActor;
    ActionMenu actionMenu;

    public ActionMenuState(BoardManager boardManager, Actor selectedActor)
        : base(boardManager)
    {
        currActor = selectedActor;
        actionMenu = boardManager.ui.actionMenu;

    }

    public override void EnterState()
    {
        actionMenu.ToggleOn();
        boardManager.ui.actorInfoPanel.initinfo(boardManager.Selector);
        actionMenu.InitSelection(currActor);

        boardManager.CheckEventsAndCompletion();

    }

    public override void ExitState()
    {
        actionMenu.gameObject.SetActive(false);
    }

    public override void ProcessInput()
    {

        if(inputHandler.IsKeyPressed(KeyBindingNames.Up))
        {
            actionMenu.AdjustMenu(-1);
        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.Down))
        {
            actionMenu.AdjustMenu(1);
        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.Select))
        {
            // actionMenu.InitSelection(currActor);
            // actionMenu.Selection(currActor);
            actionMenu.Selection();
        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.Cancel))
        {
            inputFSM.SwitchState(new UsersTurnState(boardManager));
            
            //go back to movement state or whatever
        }
        else if(Input.GetMouseButtonDown(0))
        {
            Vector2 v2 = Globals.MouseToWorld();

            int x = (int)v2.x;
            int y = (int)v2.y;

            if (boardManager.pathfinding.InRange(x,y))
            {
                boardManager.Selector.MoveTo(x, y);
                inputFSM.SwitchState(new UsersTurnState(boardManager));
            }
        }
    }


    
}
