using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionSelector : MonoBehaviour
{
    public DirectionArrow up, down, left, right;


    public void UpdatePosition(int x, int y)
    {
        this.transform.position = Globals.GridToWorld(x,y);
        ToggleOn();
    }


    public void UpdateArrows(Direction dir)
    {
        ToggleAllOff();
        SelectArrow(dir);
    }


    private void ToggleAllOff()
    {
        up.DeSelect();
        down.DeSelect();
        left.DeSelect();
        right.DeSelect();
    }


    private void SelectArrow(Direction arrow)
    {
        switch (arrow)
        {
            case Direction.Up:
                {
                    up.Select();
                    break;
                }
            case Direction.Down:
                {
                    down.Select();
                    break;
                }
            case Direction.Left:
                {
                    left.Select();
                    break;
                }
            case Direction.Right:
                {
                    right.Select();
                    break;
                }
        }

    }


    private void ToggleOn()
    {
        gameObject.SetActive(true);
    }


    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }
}
