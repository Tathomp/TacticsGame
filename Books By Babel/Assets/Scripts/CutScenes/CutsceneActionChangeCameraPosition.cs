using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutsceneActionChangeCameraPosition : CutSceneAction
{
    private MapCoords newPos;
    private bool snapCamera;


    public CutsceneActionChangeCameraPosition(MapCoords newPos, bool snapCamera=false)
    {
        this.newPos = newPos;
        this.snapCamera = snapCamera;
    }

    public CutsceneActionChangeCameraPosition(int x, int y)
    {
        this.newPos = new MapCoords(x, y);
    }

    public override CutSceneAction Copy()
    {
        return new CutsceneActionChangeCameraPosition(newPos, snapCamera);
    }

    public override IEnumerator ExecuteAction(CutsceneController controller, bool playNextNode = true)
    {
        if (snapCamera)
        {
            yield return controller.StartCoroutine(SnapCamera(controller, playNextNode));
        }
        else
        {
            yield return controller.StartCoroutine(SmoothMovement(controller, controller.cutsceneCamera, playNextNode));
        }
    }

    public IEnumerator SnapCamera(CutsceneController cs, bool playNextNode)
    {
        cs.cutsceneCamera.transform.position = new Vector3(newPos.X, newPos.Y, cs.cutsceneCamera.transform.position.z);
        yield return null;
        if(playNextNode)
        {
            cs.NextNode();
        }
    }

    public IEnumerator SmoothMovement(CutsceneController controller, Camera mainCamera, bool playNextNode)
    {
        Vector3 target = new Vector3(newPos.X, newPos.Y, mainCamera.transform.position.z);
        float remainingDist = (mainCamera.transform.position - target).sqrMagnitude;

        while (remainingDist > float.Epsilon)
        {

            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, target, 5 * Time.deltaTime);
            remainingDist = (mainCamera.transform.position - target).sqrMagnitude;
            yield return null;
        }

        if(playNextNode)
        controller.NextNode();

    }
}
