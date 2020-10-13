using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelectionState : BoardInputState
{
    Selector selector;
    Actor currActor;
    bool[,] selection;

    public MoveSelectionState(BoardManager boardManager, Actor currActor) 
        : base(boardManager)
    {
        selector = boardManager.Selector;
        this.currActor = currActor;
    }

    public override void EnterState()
    {
        selection = boardManager.pathfinding.WeightedBFS(currActor.GetCurrentStats(StatTypes.MovementRange), currActor.GetPosX(), currActor.GetPosY(), currActor.actorData.movement);

        boardManager.tileSelection.PopulateMovementRange(selection);

    }

    public override void ExitState()
    {
        boardManager.tileSelection.ClearAllRange();
    }

    public override void ProcessInput()
    {
        selector.ProccessInput(inputHandler);


        if (inputHandler.IsKeyPressed(KeyBindingNames.Select) || Input.GetMouseButtonDown(0))
        {
            if (selection[selector.mapPosX, selector.mapPosY])
            {
                List<TileNode> path = boardManager.pathfinding.GenerateMovementPath(currActor.GetPosX(), currActor.GetPosY(), selector.mapPosX, selector.mapPosY);

                if (path != null)
                {
                    currActor.MoveAlongPath(path);
                    if(currActor.CanAttack() || currActor.CanMove())
                    {
                        inputFSM.SwitchState(new ActionMenuState(boardManager, currActor));

                    }
                    else
                    {
                        //we should probably still make it pop up the menu
                        inputFSM.SwitchState(new ActionMenuState(boardManager, currActor));

                    }
                }
            }

        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Cancel) || Input.GetMouseButtonDown(1))
        {
            selector.MoveTo(currActor.GetPosX(), currActor.GetPosY());
            inputFSM.SwitchState(new ActionMenuState(boardManager, currActor));

        }


        else if (selector.ChangedPosition())
        {
            List<TileNode> node = boardManager.pathfinding.GenerateMovementPath(currActor.GetPosX(), currActor.GetPosY(), selector.mapPosX, selector.mapPosY);

            if (node != null)
            {
                boardManager.tileSelection.PopulateMovementPath(node, selection);
            }
        }


    }

}
