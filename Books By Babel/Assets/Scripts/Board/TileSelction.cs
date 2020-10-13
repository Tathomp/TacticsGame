using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelction
{

    GameObject[,] movementRangeSelection;
    GameObject[,] attackrangeSelection;


    List<GameObject> movementPath;
    List<GameObject> avaliablePlacemetnStats;
    List<GameObject> aoeSelection;

    GameObject movementSelection;
    GameObject attackSelection;
    GameObject movementPathSelection;
    GameObject placementSelection;


    Pathfinding pathfinding;


    int sizeX, sizeY;


    public TileSelction(int x, int y, Pathfinding pathfinding)
    {
        this.pathfinding = pathfinding;

        movementRangeSelection = new GameObject[x, y];
        attackrangeSelection = new GameObject[x, y];
        avaliablePlacemetnStats = new List<GameObject>();
        aoeSelection = new List<GameObject>();
        movementPath = new List<GameObject>();

        sizeX = x;
        sizeY = y;

        movementSelection = Resources.Load<GameObject>("BaseObjects/MovementTile");
        attackSelection = Resources.Load<GameObject>("BaseObjects/AttackTile");
        movementPathSelection = Resources.Load<GameObject>("BaseObjects/AttackTile");
        placementSelection = Resources.Load<GameObject>("BaseObjects/PlacementTile");
    }


    public void PopulateMovementRange(List<TileNode> path)
    {
        bool[,] selection = new bool[sizeX, sizeY];

        foreach (TileNode node in path)
        {
            selection[node.data.posX, node.data.posY] = true;
        }

        PopulateMovementRange(selection);
    }

    public void PopulateMovementRange(bool[,] selection)
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                if(selection[x,y])
                {
                    movementRangeSelection[x, y] = GameObject.Instantiate<GameObject>(movementSelection, Globals.GridToWorld(pathfinding.GetTileNode(x,y)), Quaternion.identity);
                }
            }
        }
    }

    public void PopulateAttackRange(List<TileNode> path)
    {
        bool[,] selection = new bool[sizeX, sizeY];

        foreach (TileNode node in path)
        {
            selection[node.data.posX, node.data.posY] = true;
        }

        PopulateAttackRange(selection);
    }

    public void PopulateAttackRange(bool[,] selection)
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                if (selection[x, y])
                {
                    attackrangeSelection[x, y] = GameObject.Instantiate<GameObject>(attackSelection, Globals.GridToWorld(pathfinding.GetTileNode(x, y)), Quaternion.identity);
                }
            }
        }
    }


    public void PopulateMovementPath(List<TileNode> path, bool[,] seelction)
    {
        ClearPath();

        for (int i = 0; i < path.Count; i++)
        {
            if (seelction[path[i].data.posX, path[i].data.posY])
            {
                movementPath.Add(GameObject.Instantiate<GameObject>(movementPathSelection, Globals.GridToWorld(pathfinding.GetTileNode(path[i].data.posX, path[i].data.posY)), Quaternion.identity));
            }
        }
    }


    public void PopulatePlacement(List<MapCoords> ava)
    {
        foreach (MapCoords coords in ava)
        {
            avaliablePlacemetnStats.Add(
                GameObject.Instantiate<GameObject>(
                    placementSelection,
                    Globals.GridToWorld(coords.X, coords.Y),
                    Quaternion.identity));

        }

    }


    public void PopulateSkillEffect(List<TileNode> nodes)
    {
        ClearAoE();

        foreach (TileNode node in nodes)
        {
            aoeSelection.Add(GameObject.Instantiate<GameObject>(attackSelection, Globals.GridToWorld(node.data.posX, node.data.posY), Quaternion.identity));

        }
    }

    public void ClearAoE()
    {
        foreach (GameObject go in aoeSelection)
        {
            GameObject.Destroy(go);
        }
    }

    public void ClearAllRange()
    {
        ClearAttackRange();
        ClearMovementRange();
        ClearPath();
        ClearAvaliable();
    }



    public void ClearAvaliable()
    {
        foreach (GameObject go in avaliablePlacemetnStats)
        {
            GameObject.Destroy(go);
        }
    }

    public void ClearPath()
    {
        foreach (GameObject go in movementPath)
        {
            GameObject.Destroy(go);
        }
    }


    public void ClearMovementRange()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                if (movementRangeSelection[x, y] != null)
                {
                    GameObject.Destroy(movementRangeSelection[x, y]);
                }
            }
        }
    }


    public void ClearAttackRange()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                if(attackrangeSelection[x,y] != null)
                {
                   GameObject.Destroy(attackrangeSelection[x, y]);
                }
            }
        }
    }

}
