using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuState : BoardInputState
{
    OptionsMenu optionsMenu;

    public OptionsMenuState(BoardManager boardManager) 
        : base(boardManager)
    {
        optionsMenu = boardManager.ui.optionsMenu;
    }


    public override void EnterState()
    {
        optionsMenu.ToggleOn();
    }


    public override void ExitState()
    {
        optionsMenu.ToggleOff();
    }


    public override void ProcessInput()
    {
        if(inputHandler.IsKeyPressed(KeyBindingNames.Cancel))
        {
            inputFSM.SwitchState(new UsersTurnState(boardManager));
        }
    }

}
