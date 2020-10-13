using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWaitDirectionState : InputState
{
    private Actor currActor;
    private Direction newDirection;
    private DirectionSelector directionArrow;


    public SelectWaitDirectionState(Actor currActor)
    {
        this.currActor = currActor;
        newDirection = currActor.actorData.directionFacing;
        directionArrow = Globals.GetBoardManager().ui.dirSelector;
    }

    public override void EnterState()
    {
        directionArrow.UpdatePosition(currActor.GetPosX(), currActor.GetPosY());
    }

    public override void ExitState()
    {
        currActor.actorData.directionFacing = newDirection;
        directionArrow.ToggleOff();

    }

    public override void ProcessInput()
    {
        if(inputHandler.IsKeyPressed(KeyBindingNames.Up))
        {
            newDirection = Direction.Up;
            directionArrow.UpdateArrows(newDirection);

        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Left))
        {
            newDirection = Direction.Left;
            directionArrow.UpdateArrows(newDirection);

        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Down))
        {
            newDirection = Direction.Down;
            directionArrow.UpdateArrows(newDirection);

        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Right))
        {
            newDirection = Direction.Right;
            directionArrow.UpdateArrows(newDirection);

        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Select))
        {
            currActor.actorData.directionFacing = newDirection;
            currActor.Wait();
            Globals.GetBoardManager().turnManager.CalculateFastest();
        }
        //write cancel selections


    }

}
