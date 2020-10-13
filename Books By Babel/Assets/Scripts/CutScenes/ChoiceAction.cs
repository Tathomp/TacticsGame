using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChoiceAction : DialogueAction
{
    //we could maybe reword this to trigger map events
    public string[] choices;

    //
    public string[] missionFlagToSet;
    public string[] eventFlagMission;
    public string[] nextCutscene;


    public ChoiceAction()
    {
        choices = new string[2] { "",""};
        missionFlagToSet = new string[2] { "",""};
        eventFlagMission = new string[2] { "",""};
        nextCutscene = new string[2] { "",""};
    }


    public override CutSceneAction Copy()
    {
        ChoiceAction action = new ChoiceAction();

        action.dialog = dialog;
        action.actorID = actorID;

        action.choices = choices;
        action.missionFlagToSet = missionFlagToSet;
        action.nextCutscene = nextCutscene;
        action.eventFlagMission = eventFlagMission;

        return action;

    }

    public override bool IsChoiceAction()
    {
        return true;
    }

    public override bool IsDialogueAction()
    {
        return true;
    }

    public override IEnumerator ExecuteAction(CutsceneController controller, bool playNextNode = true)
    {
        controller.StartCoroutine(base.ExecuteAction(controller));
        controller.choicePanel.InitDialogChoice(this);
        // Next we need to display the choices

        yield return null;
    }
}
