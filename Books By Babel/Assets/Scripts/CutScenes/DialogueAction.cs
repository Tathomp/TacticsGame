using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueAction : CutSceneAction
{

    //pointer to a text db?
    //store the text in this one

    public string dialog;

    public override IEnumerator ExecuteAction(CutsceneController controller, bool playNextNode = true)
    {
        controller.ToggleOn();

        controller.dialogPanel.UpdateDialog(dialog, actorID, actorID);

        yield return null;
    }

    public override bool IsDialogueAction()
    {
        return true;
    }

    public override CutSceneAction Copy() 
    {
        DialogueAction dialogueAction = new DialogueAction();

        dialogueAction.dialog = dialog;
        dialogueAction.actorID = actorID;

        return dialogueAction;
    }
}
