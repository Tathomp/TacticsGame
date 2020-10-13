using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// WE should be able to remove anything that references the board or base manager
/// in this class, it shouldn't be used any more at this point
/// DELETE
/// </summary>
public class CutsceneChoiceState : BoardInputState
{
    BaseManager baseManager;

    ChoiceAction cutscene;
    BattleUIManager ui;
    CutsceneController csController;

    CinematicStatus prevStatus;

    public CutsceneChoiceState(BoardManager boardManager, ChoiceAction cs,
        CinematicStatus duringbattle)
        : base(boardManager)
    {
        this.cutscene = cs;
        //csController = boardManager.ui.CSController;
        this.prevStatus = duringbattle;
    }

    public CutsceneChoiceState(BaseManager baseManager, ChoiceAction cs,
        CinematicStatus duringbattle)
        : base(null)
    {
        this.cutscene = cs;
        csController = baseManager.baseUI.cutsceneController;
        this.prevStatus = duringbattle;
        this.baseManager = baseManager;
    }

    public CutsceneChoiceState(CutsceneController controller, ChoiceAction cs,
    CinematicStatus duringbattle)
    : base(null)
    {
        this.cutscene = cs;
        csController = controller;
        this.prevStatus = duringbattle;
        this.baseManager = null;
    }


    public override void EnterState()
    {
       csController.StartCoroutine( cutscene.ExecuteAction(csController) );
    }

    public override void ExitState()
    {
    }

    public override void ProcessInput()
    {
        if (inputHandler.IsKeyPressed(KeyBindingNames.Up))
        {
            csController.choicePanel.ShiftSelection(-1);
            //Debug.Log("Selection: " + csController.choicePanel.currSelection);

        }
        else if (inputHandler.IsKeyPressed(KeyBindingNames.Down))
        {
            csController.choicePanel.ShiftSelection(1);
            //Debug.Log("Selection: " + csController.choicePanel.currSelection);

        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.Select))
        {


            string mf = csController.choicePanel.GetMissionFlag();
            string ef = csController.choicePanel.GetEventFlag();
            string cs = csController.choicePanel.GetNextCutscene();

            if (mf.Equals("") == false)
            {
                //set mission flag
                ((FlagBool)Globals.campaign.GlobalFlags[mf]).ChangeFlag(true);

            }
            
            if(ef.Equals("") == false)
            {
                // we should make sure that cutscenes that take place off the battlefield
                // can't have event flags
                // OR
                // do some kind of check in this method

                //checks to make sure this actually happens when is valid
                if(boardManager != null)
                {
                    foreach (Event events in boardManager.currentMission.MissionEvents)
                    {
                      if(events.GetKey() == ef)
                        {
                            events.FireEvent();
                            //maybe remove the event after its fired
                        }
                    } 
                }
                else if(csController != null)
                {
                    csController.NextNode();
                }
            }

            if(cs.Equals("") == false)
            {
                //play next cutscene

                if(boardManager != null)
                {
                    boardManager.inputFSM.SwitchState(new CutsceneInputState(boardManager,
                        Globals.campaign.GetCutsceneCopy(cs),
                        csController,
                        prevStatus));

                    //boardManager.ui.CSController.choicePanel.ToggleOff();
                }
                else if(baseManager != null)
                {
                    //base should never be null at this point
                    baseManager.inputFSM.SwitchState(new CutsceneInputState(baseManager,
                        Globals.campaign.GetCutsceneCopy(cs),
                        csController,
                        prevStatus));

                   // boardManager.ui.CSController.choicePanel.ToggleOff();

                }
                else
                {
                    //cutscene controller
                    csController.DeleteAll();
                    csController.choicePanel.TurnOffChoices();

                    csController.InitCutscene(Globals.campaign.GetCutsceneCopy(cs));

                }

                return;
            }

            // there are no cutscenese to play so we should go back to what we were 
            // doing?
            /*
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
            else if (prevStatus == CinematicStatus.RelationshipScene)
            {
                baseManager.inputFSM.SwitchState(new BlockUserInputState());

                baseManager.baseUI.cutsceneController.ToggleOff();
                baseManager.baseUI.ExpandMenu();
            }
            */
        }
    }
}
