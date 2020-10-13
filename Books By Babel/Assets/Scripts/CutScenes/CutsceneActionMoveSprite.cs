using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutsceneActionMoveSprite : CutSceneAction
{
    List<MapCoords> path;
    string uid;


    public CutsceneActionMoveSprite(string uid, List<MapCoords> path)
    {
        this.path = path;
        this.uid = uid;
    }

    public override CutSceneAction Copy()
    {
        List<MapCoords> temp = new List<MapCoords>();

        foreach (MapCoords item in path)
        {
            temp.Add(item);
        }

        return new CutsceneActionMoveSprite(uid, temp);
    }

    public override IEnumerator ExecuteAction(CutsceneController controller, bool playNextNode = true)
    {
        controller.dialogPanel.gameObject.SetActive(false);
        yield return controller.StartCoroutine(MoveAlong(controller, playNextNode));
    }

    IEnumerator MoveAlong(CutsceneController obj, bool playNext)
    {
        Transform trans = obj.uidGameObjectMap[uid].transform;

        foreach (MapCoords coords in path)
        {
            obj.StartCoroutine(SmoothMovement(Globals.GridToWorld(coords.X, coords.Y), trans));
            yield return new WaitForSeconds(.2f);

        }

        //this is where we switch the next action
        if(playNext)
        obj.NextNode();

    }

    public IEnumerator SmoothMovement(Vector3 target, Transform transform)
    {
        float remainingDist = (transform.position - target).sqrMagnitude;

        int movementspeed = 5;

        while (remainingDist > float.Epsilon)
        {

            transform.position = Vector3.MoveTowards(transform.position, target, movementspeed * Time.deltaTime);
            remainingDist = (transform.position - target).sqrMagnitude;
            yield return null;
        }
    }
    // coroutine to move
    // do we want to do pathfinding our what idk
    // call next action on the controller once movement is done
}
