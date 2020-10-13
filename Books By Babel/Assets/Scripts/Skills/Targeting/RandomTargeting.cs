using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomTargeting : ITargetable
{
    public int maxTargets;
    public int min;
    public ITargetable baseTargeting;


    public RandomTargeting(List<string> impassableTiles, bool stopOnOccupied) : base(impassableTiles, stopOnOccupied)
    {
    }

    public RandomTargeting(List<string> impassableTiles, bool stopOnOccupied, bool pierceUnits, int maxUnitsToPierce) : base(impassableTiles, stopOnOccupied, pierceUnits, maxUnitsToPierce)
    {
    }

    public override List<TileNode> TargetTiles(Actor source, TileNode center)
    {
        return baseTargeting.TargetTiles(source, center);
    }

    public override List<TileNode> FinalTargetTiles(Actor source, TileNode center)
    {
        List<TileNode> nodes = new List<TileNode>();
        List<TileNode> basenodes =  baseTargeting.TargetTiles(source, center);

        int currentMax = basenodes.Count;

        while(currentMax > 0 & nodes.Count < maxTargets)
        {
            int i = Random.Range(0, currentMax - 1);

            nodes.Add(basenodes[i]);
            basenodes.RemoveAt(i);

            currentMax--;
        }

        return nodes;
    }
}
