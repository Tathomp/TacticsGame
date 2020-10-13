using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is for  cut scene manager input things
public class CutsceneDialoguInput : InputState
{

    CutsceneController csController;
    CutScene scene;
    CutSceneAction action;

    public CutsceneDialoguInput(CutsceneController controller, 
        CutScene currentScene, CutSceneAction currentAction)
    {
        csController = controller;
        this.scene = currentScene;
        this.action = currentAction;

    }


    public override void EnterState()   
    {
        csController.dialogPanel.gameObject.SetActive(true);
        csController.StartCoroutine(action.ExecuteAction(csController));
    }

    public override void ExitState()
    {

    }

    public override void ProcessInput()
    {
        //the check for a choice is redunant now i think
        //should probably delete this
        //
        if (action is ChoiceAction)
        { 

            if (inputHandler.IsKeyPressed(KeyBindingNames.Up))
            {
                csController.choicePanel.ShiftSelection(-1);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.Down))
            {
                csController.choicePanel.ShiftSelection(1);
            }
            else if (inputHandler.IsKeyPressed(KeyBindingNames.Select))
            {

            }
        }
        else if(action is DialogueAction)
        {
            if(inputHandler.IsKeyPressed(KeyBindingNames.Select) || Input.GetMouseButtonDown(0))
            {
                //go to next node
                csController.dialogPanel.EnterPressed();
            }
        }
    }
}
