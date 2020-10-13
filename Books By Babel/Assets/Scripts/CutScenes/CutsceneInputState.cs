using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DELETE maybe
/// Check if this is even being used any more
/// </summary>
public class CutsceneInputState : BoardInputState
{
    public CutScene cutscene;

    BaseManager baseMange;

    CutsceneController csController;
    CinematicStatus prevStatus;

    public CutsceneInputState(BoardManager boardManager, CutScene cs,
        CutsceneController controller, CinematicStatus status) 
        : base(boardManager)
    {
        cutscene = Globals.campaign.GetCutsceneCopy(cs.GetKey());
        csController = controller;
        prevStatus = status;

    }

    public CutsceneInputState(BaseManager baseManager, CutScene cs,
        CutsceneController controller, CinematicStatus status)
        : base(null)
    {
        cutscene = Globals.campaign.GetCutsceneCopy(cs.GetKey());
        csController = controller;
        prevStatus = status;

        baseMange = baseManager;

    }


    public override void EnterState()
    {
        //build a board and populate with actors
        csController.ToggleOn();

        CutSceneAction action = cutscene.NextAction();

        if(action is ChoiceAction)
        {
            // swap input states here;
            if(boardManager == null)
            {
                baseMange.inputFSM.SwitchState(new
                    CutsceneChoiceState(baseMange, action as ChoiceAction,
                    prevStatus));
            }
            else
            {
                boardManager.inputFSM.SwitchState(new 
                    CutsceneChoiceState(boardManager, action as ChoiceAction, 
                    prevStatus));

            }

        }
        else
        {
            csController.StartCoroutine(action.ExecuteAction(csController));
        }

    }


    public override void ExitState()
    {


    }


    public override void ProcessInput()
    {
        if(inputHandler.IsKeyPressed(KeyBindingNames.Select) || Input.GetMouseButtonDown(0))
        {
            if (cutscene.IsEmpty())
            {
                if (prevStatus == CinematicStatus.DuringBattle)
                {
                    boardManager.turnManager.ActorTakesTurn();

                    csController.ToggleOff();
                    boardManager.ui.ToggleOnBattleUI();
                }
                else if (prevStatus == CinematicStatus.Prebattle)
                {
                    boardManager.InitBattledata();

                    csController.ToggleOff();
                    boardManager.ui.ToggleOnBattleUI();
                }
                else if(prevStatus == CinematicStatus.RelationshipScene)
                {
                    baseMange.inputFSM.SwitchState(new BlockUserInputState());

                    baseMange.baseUI.cutsceneController.ToggleOff();
                    baseMange.baseUI.ExpandMenu();
                }
            }
            else
            {
                CutSceneAction currentNode = cutscene.NextAction();

                if (currentNode is ChoiceAction)
                {
                    if (boardManager == null)
                    {
                        inputFSM.SwitchState(new CutsceneChoiceState(baseMange,
                            (ChoiceAction)currentNode, prevStatus));
                    }
                    else
                    {
                        inputFSM.SwitchState(new CutsceneChoiceState(boardManager,
                            (ChoiceAction)currentNode, prevStatus));
                    }
                }
                else
                {
                    csController.StartCoroutine(currentNode.ExecuteAction(csController));
                }
            }
        }
    }

}
