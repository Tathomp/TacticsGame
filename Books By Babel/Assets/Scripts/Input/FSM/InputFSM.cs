using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFSM
{
    public InputState currentState;
    bool TakeInput; //This probably isn't needed

    public InputFSM (InputState state)
    {
        SwitchState(state);
        TakeInput = true;
    }



    public void ProcessInput()
    {
        if (TakeInput)
        {
            currentState.ProcessInput();
        }
    }

    public void SwitchState(InputState newState)
    {
        if(currentState != null)
        {
            currentState.ExitState();
            newState.inputHandler = currentState.inputHandler;
        }
        else
        {
            newState.inputHandler = new InputHandler();
        }
        currentState = newState;
        currentState.inputFSM = this;

        currentState.EnterState();
        Debug.Log(newState);
    }


}
