using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO
/// Deleted
/// Depricated
/// 
/// </summary>
public class UnitSelectedState : BoardInputState
{
    ActorData data;
    PartyEditPanel characterInfoPanel;

    public UnitSelectedState(BoardManager boardManager, ActorData data) 
        : base(boardManager)
    {
        this.data = data;
        //characterInfoPanel = boardManager.ui.characterInfoPanel;
    }

    public override void EnterState()
    {
        // Initialize some kind of info panel
        // 
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void ProcessInput()
    {
       if( inputHandler.IsKeyPressed(KeyBindingNames.Cancel))
       {
            inputFSM.SwitchState(new UsersTurnState(boardManager));
       }
    }
}
