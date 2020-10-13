using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipSceneInputState : BaseInputState
{
    CutScene relationshipScene;
    CutsceneController dialogPanel;

    public RelationshipSceneInputState(BaseManager baseManager, CutScene relationshipScene,
        CutsceneController dialogPanel)
        : base(baseManager)
    {
        this.relationshipScene = relationshipScene;
        this.dialogPanel = dialogPanel;
    }

    public override void EnterState()
    {
        baseManager.baseUI.CollapseAllAndSome();
        dialogPanel.gameObject.SetActive(true);

       // DialogueAction action = relationshipScene.NextAction() as DialogueAction;
        //dialogPanel.UpdateDialog(action);
    }

    public override void ExitState()
    {
        dialogPanel.gameObject.SetActive(false);
        baseManager.baseUI.ExpandMenu();

    }

    public override void ProcessInput()
    {
        if (inputHandler.IsKeyPressed(KeyBindingNames.Select) || Input.GetMouseButtonDown(0))
        {
            if (relationshipScene.IsEmpty())
            {
                // Go back to our main base set up
                // just switching to the block user state will be enough for now
                // because all the behavior is in the exit method in this state
                baseManager.inputFSM.SwitchState(new BlockUserInputState());
            }

            if (relationshipScene.IsDialogue())
            {
                // process
              //  DialogueAction action = relationshipScene.NextAction() as DialogueAction;
               // dialogPanel.UpdateDialog(action);
            }
        }
    }

    
}
