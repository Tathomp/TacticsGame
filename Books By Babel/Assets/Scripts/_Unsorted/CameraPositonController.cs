using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositonController : MonoBehaviour
{
    public float minX, maxX, minY, maxY;

    private void Start()
    {
        //this.transform.position = new Vector3(minX, minY, transform.position.z);
    }

    public void InitCameraPosition(int mapsizeX, int mapsizeY)
    {

    }


    public void Step(int x, int y)
    {
        MoveMap((int)this.transform.position.x + x, (int)this.transform.position.y + y);
    }

    public void MoveMap(int x, int y)
    {
        if(x <= maxX & x >= minX & y <= maxY & y>= minY)
        this.transform.position = new Vector3(x, y, this.transform.position.z);
    }



    public void StepLeft()
    {
        Step(-1, 0);
    }

    public void StepRight()
    {
        Step(1, 0);
    }


    public void StepUp()
    {
        Step(0, 1);
    }


    public void StepDown()
    {
        Step(0 ,-1);
    }


}
