using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelectState : BoardInputState
{
    List<IUseable> skills;
    Actor currentActor;
    BattleUIManager ui;

    public AbilitySelectState(BoardManager boardManager, Actor currentActor, List<IUseable> skills) 
        : base(boardManager)
    {
        this.skills = skills;
        ui = boardManager.ui;
        this.currentActor = currentActor;
    }

    public override void EnterState()
    {
        ui.skillPanel.GenerateAbilityList(currentActor, skills);
    }

    public override void ExitState()
    {
        //also remove listners?
        ui.skillPanel.ClearSkillListeners();
        ui.skillPanel.gameObject.SetActive(false);
    }

    public override void ProcessInput()
    {
        if(inputHandler.IsKeyPressed(KeyBindingNames.Up))
        {
            ui.skillPanel.AdjustMenu(1);
        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.Down))
        {
            ui.skillPanel.AdjustMenu(-1);

        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.Select))
        {
            ui.skillPanel.Selection();
        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.Cancel))
        {
            inputFSM.SwitchState(new UsersTurnState(boardManager));
        }
    }
}
