using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ITargetable
{
    public List<string> immpassableTiles;
    
    public bool stopOnOccupied;


    protected bool pierceUnits;
    protected int maxUnitsToPierce, currPierce;

    public ITargetable(List<string> impassableTiles, bool stopOnOccupied)
    {
        this.immpassableTiles = impassableTiles;
        this.stopOnOccupied = stopOnOccupied;


        this.pierceUnits = false;
        this.maxUnitsToPierce = 0;
        currPierce = 0;
    }

    public ITargetable(List<string> impassableTiles, bool stopOnOccupied, bool pierceUnits, int maxUnitsToPierce)
    {
        this.immpassableTiles = impassableTiles;
        this.stopOnOccupied = stopOnOccupied;

        this.pierceUnits = pierceUnits;
        this.maxUnitsToPierce = maxUnitsToPierce;
        currPierce = 0;
    }


    public abstract List<TileNode> TargetTiles(Actor source, TileNode center);
    public virtual List<TileNode> FinalTargetTiles(Actor source, TileNode center) { return TargetTiles(source, center); }


    public void AddToQueue(Queue<TileNode> queue, HashSet<TileNode> visited, int x, int y)
    {
        BoardManager bm = Globals.GetBoardManager();

        if (bm.pathfinding.InRange(x, y))
        {
            TileNode node = bm.pathfinding.GetTileNode(x, y);

            if (visited.Contains(node) == false && immpassableTiles.Contains(node.type.GetKey()) == false)
            {
                queue.Enqueue(node);
                visited.Add(node);
            }
        }
    }

    public bool IsTileValid(TileNode node)
    {
        if(node == null)
        {
            return false;
        }
        

        if((stopOnOccupied == true && node.actorOnTile != null))
        {
            return false;    
        }

        if(immpassableTiles.Contains(node.type.GetKey()))
        {
            return false;
        }



        if (pierceUnits && node.actorOnTile != null)
        {

            Debug.Log("Current: " + currPierce + " Max: " + maxUnitsToPierce);

            if (currPierce < maxUnitsToPierce)
            {
                currPierce++;
            }
            else
            {
                return false;
            }
        }



        return true;
    }

    public int AbsoluteDistance(int x, int y)
    {
        return Mathf.Abs(Mathf.Abs(x) - Mathf.Abs(y));
    }


}
