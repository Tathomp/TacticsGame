using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour {

    public GameObject start, end;

    private LineRenderer lr;

    private void Start()
    {

    }

    public void SetEndPoints(GameObject start, GameObject end)
    {
        this.start = start;
        this.end = end;
    }

    public void InitLine()
    {
        lr = this.gameObject.GetComponent<LineRenderer>();

        lr.SetWidth(0.05f, 0.05f);

        lr.SetVertexCount(2);

        lr.SetPosition(0, start.transform.position);
        lr.SetPosition(1, end.transform.position);
    }

}
