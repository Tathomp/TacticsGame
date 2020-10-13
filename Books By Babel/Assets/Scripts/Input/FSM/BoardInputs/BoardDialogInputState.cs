using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This should only be used to control dialog in the board state
/// </summary>
public class BoardDialogInputState : BoardInputState
{
    private CutScene cs;
    private DialogueAction current_action;

    private DialogPanel dialogpanel;
    private DialogChoicePanel choicePanel;

    public BoardDialogInputState(CutScene cs, BoardManager bm) : base(bm)
    {
        this.cs = cs;

        dialogpanel = bm.ui.dialogPanel;
        choicePanel = bm.ui.dialogChoice;

        GetNextNode();
    }

    public override void EnterState()
    {
        dialogpanel.UpdateDialog(current_action);

    }

    public override void ExitState()
    {
        dialogpanel.gameObject.SetActive(false);
        choicePanel.gameObject.SetActive(false);

        boardManager.ui.ToggleOnBattleUI();
    }

    public override void ProcessInput()
    {

        if (current_action == null)
        {
            boardManager.turnManager.CalculateFastest();
            //inputFSM.SwitchState(new UsersTurnState(boardManager));
        }

        if (current_action is ChoiceAction)
        {
            ChoiceControls();
        }
        else if( current_action is DialogueAction)
        {
            DialogControls();
        }
    }

    private void DialogControls()
    {
        if(inputHandler.IsKeyPressed(KeyBindingNames.Select))
        {
            GetNextNode();
        }
    }

    private void ChoiceControls()
    {

        if(inputHandler.IsKeyPressed(KeyBindingNames.Up))
        {
            choicePanel.ShiftSelectionUp();
        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.Down))
        {
            choicePanel.ShiftSelectionDown();
        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.Select))
        {
            GetNextNode();


            string mf = choicePanel.GetMissionFlag();
            string ef = choicePanel.GetEventFlag();
            string cs = choicePanel.GetNextCutscene();

            if(mf.Equals("") == false)
            {
                ((FlagBool)Globals.campaign.GlobalFlags[mf]).ChangeFlag(true);
            }


            if(ef.Equals("") == false)
            {
                foreach (Event events in boardManager.currentMission.MissionEvents)
                {
                    if (events.GetKey() == ef)
                    {
                        events.FireEvent();

                        //maybe remove the event after its fired
                        Debug.Log("Fire Event");
                    }
                }

            }

            if(cs.Equals("") == false)
            {
                boardManager.inputFSM.SwitchState(new BoardDialogInputState(Globals.campaign.GetCutsceneCopy(cs), boardManager));
            }
        }
    }


    private void GetNextNode()
    {
        if(cs.IsEmpty())
        {
            current_action = null;
            return;
        }

        CutSceneAction a =  cs.NextAction();

        if(a is ChoiceAction)
        {
            current_action = a as ChoiceAction;
            dialogpanel.UpdateDialog(current_action);
            choicePanel.InitDialogChoice(current_action as ChoiceAction);
        }
        else if(a is DialogueAction)
        {
            current_action = a as DialogueAction;
            choicePanel.ToggleOff();
            dialogpanel.UpdateDialog(current_action);
        }
        else
        {
            current_action = null;
        }
    }
}
