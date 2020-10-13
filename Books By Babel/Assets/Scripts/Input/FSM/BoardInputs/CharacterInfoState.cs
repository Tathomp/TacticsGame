using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoState : BoardInputState
{
    /// <summary>
    /// TODO Delte probably
    /// </summary>
    PartyEditPanel infopanel;
    Actor currentActor;

    public CharacterInfoState(BoardManager boardManager, Actor actor) 
        : base(boardManager)
    {
        //infopanel = boardManager.ui.characterInfoPanel;
        currentActor = actor;
        infopanel = boardManager.ui.inBattleCharacterInfoPanel;
    }

    public override void EnterState()
    {
        infopanel.InitPanel(currentActor.actorData);
        infopanel.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        infopanel.gameObject.SetActive(false);
    }

    public override void ProcessInput()
    {
        if(inputHandler.IsKeyPressed(KeyBindingNames.Cancel))
        {
            inputFSM.SwitchState(new UsersTurnState(boardManager));
        }
    }


}
