using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackgroundContoller : MonoBehaviour
{
    public Camera camera;

    int currIndex, maxIdex;

    ColorDatabaseEntry currentEntry;


    public Color goal1, goal2;
    public float speed;

    private Vector3 v1, v2, cameraV;

    public bool changing;

    Coroutine currCoroutine;

    // Start is called before the first frame update
    void Start()
    {

        NewColor("default");

        camera.backgroundColor = currentEntry.colors[0];

    }

    // Update is called once per frame
    void Update()
    {
        if (changing == false)
        {
            if (camera.backgroundColor == goal1)
            {
                currIndex++;
                if (currIndex >= maxIdex)
                {
                    currIndex = 0;
                }


                SwitchColor(currentEntry.colors[currIndex]);
                currCoroutine = StartCoroutine(ChangeToGoal1(v1));

            }
        }
    }


    public void NewColor(string gkey)
    {
        if(currCoroutine != null)
        StopCoroutine(currCoroutine);

        currentEntry = Globals.GetColorDatabaseEntry(gkey);

        goal1 = currentEntry.colors[0];

        maxIdex = currentEntry.colors.Count;

        SwitchColor(goal1);

        currCoroutine = StartCoroutine(ChangeToGoal1(v1));
    }

    private void SwitchColor(Color c)
    {
        goal1 = c;
        v1 = new Vector3(goal1.b, goal1.g, goal1.r);

    }

    private void SwitchColors(Color g1, Color g2)
    {
        goal1 = g1;
        goal2 = g2;
        v1 = new Vector3(goal1.b, goal1.g, goal1.r);
        v2 = new Vector3(goal2.b, goal2.g, goal2.r);
    }

    IEnumerator ChangeToGoal1(Vector3 goal)
    {
        changing = true;

        cameraV = new Vector3(camera.backgroundColor.b, camera.backgroundColor.g, camera.backgroundColor.r);

        float remainingDist = (cameraV - goal).sqrMagnitude;

        while (remainingDist > float.Epsilon)
        {
          //  cameraV = new Vector3(camera.backgroundColor.b, camera.backgroundColor.g, camera.backgroundColor.r);

            cameraV = Vector3.MoveTowards(cameraV, goal, speed * Time.deltaTime);
            remainingDist = (cameraV - goal).sqrMagnitude;

            camera.backgroundColor = new Color(cameraV.z, cameraV.y, cameraV.x);

            yield return null;
        }

        changing = false;
    }

}
