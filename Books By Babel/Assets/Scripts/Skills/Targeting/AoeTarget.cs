using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AoeTarget : ITargetable
{
    public int radius;

    public AoeTarget(List<string> impassableTiles, bool stopOnOccupied, int radius) 
        : base(impassableTiles, stopOnOccupied)
    {
        this.radius = radius;
    }

    public override List<TileNode> TargetTiles(Actor source, TileNode center)
    {
        List<TileNode> inRange = new List<TileNode>();

        Pathfinding pathfinding = GameObject.Find("BoardManager").GetComponent<BoardManager>().pathfinding;

        bool[,] temp = pathfinding.ValidBFS(radius, 0, center.data.posX, center.data.posY, immpassableTiles, stopOnOccupied);

        for (int x = 0; x < pathfinding.sizeX; x++)
        {
            for (int y = 0; y < pathfinding.sizeY; y++)
            {
                if (temp[x, y])
                {
                    inRange.Add(pathfinding.GetTileNode(x, y));
                }
            }
        }


        return inRange;
    }

}
