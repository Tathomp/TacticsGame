using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutsceneActionList : CutSceneAction
{
    //Breaks down in certain situations because of how 'next node' is called.
    //Basically next node will be called once for each of the actions
    //We could fix that more or less but it still might get weird with nodes that use coroutines
    //

    public List<CutSceneAction> actions;

    public CutsceneActionList(List<CutSceneAction> actions)
    {
        this.actions = actions;
    }

   public CutsceneActionList()
   {
        actions = new List<CutSceneAction>();
   }

    public override CutSceneAction Copy()
    {
        CutsceneActionList temp = new CutsceneActionList();

        foreach (CutSceneAction action in actions)
        {
            temp.actions.Add(action.Copy());
        }

        return temp;
    }

    public override IEnumerator ExecuteAction(CutsceneController controller, bool playNextNode = true)
    {
        foreach (CutSceneAction action in actions)
        {
            yield return controller.StartCoroutine(action.ExecuteAction(controller, false));
        }

        controller.NextNode();
    }

    public void ActionComplete(CutSceneAction actionToRemove)
    {

    }
}
