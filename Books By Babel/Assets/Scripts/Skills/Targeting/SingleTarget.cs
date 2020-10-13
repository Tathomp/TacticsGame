using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SingleTarget : ITargetable
{

    public SingleTarget(List<string> impassableTiles, bool stopOnOccupied = true) 
        : base(impassableTiles, stopOnOccupied)
    {

    }

    public override List<TileNode> TargetTiles(Actor Source, TileNode center)
    {
        List<TileNode> nodes = new List<TileNode>();


        nodes.Add(center);
        return nodes;
    }
}
