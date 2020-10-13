using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardInputState : InputState
{
    protected BoardManager boardManager;


    public BoardInputState(BoardManager boardManager)
    {
        this.boardManager = boardManager;

        // THis is hacked together and is probably fucked
        //
        if(boardManager != null)
        {
            inputFSM = boardManager.inputFSM;

        }
    }


    public void CameraControls()
    {
        if (inputHandler.IsKeyPressed(KeyBindingNames.Left))
        {
            boardManager.ui.cameraPositonController.StepLeft();
        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Right))
        {
            boardManager.ui.cameraPositonController.StepRight();
        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Up))
        {
            boardManager.ui.cameraPositonController.StepUp();
        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Down))
        {
            boardManager.ui.cameraPositonController.StepDown();
        }
    }
}
