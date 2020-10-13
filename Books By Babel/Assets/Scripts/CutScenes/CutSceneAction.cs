using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class CutSceneAction
{
    public string actorID;

    public abstract CutSceneAction Copy();

    public abstract IEnumerator ExecuteAction(CutsceneController controller, bool playNextNode = true);

    public virtual bool IsChoiceAction()
    {
        return false;
    }

    public virtual bool IsDialogueAction()
    {
        return false;
    }
	
}
