using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AIGoal 
{
    public abstract IEnumerator CalculateActions(List<AIAction> validActions, BoardManager bm, Actor ai);

    protected float attack_ally_score = 0.02f;


    public List<TileNode> GetTileNodeMovementRange(BoardManager bm, Actor ai)
    {
        //get a list of all the tiles in range of the actor
        //might possible to generalize this more but whatever
        List<TileNode> nodes;

        bool[,] valid = bm.pathfinding.WeightedBFS(ai.GetCurrentStats(StatTypes.MovementRange),
            ai.GetPosX(), ai.GetPosY(), ai.actorData.movement);

        nodes = bm.pathfinding.GetNodes(valid);

        return nodes;
    }
    
    public MapCoords PosOfClosestEnemy(Actor source, BoardManager bm)
    {
        int startX = source.GetPosX();
        int startY = source.GetPosY();
        int newX = 0;
        int newY = 0;
        float currDistance = float.MaxValue;


        for (int x = 0; x < bm.pathfinding.sizeX; x++)
        {
            for (int y = 0; y < bm.pathfinding.sizeY; y++)
            {
                if(bm.pathfinding.GetTileNode(x,y).HasActor() && bm.pathfinding.GetTileNode(x,y).actorOnTile.actorData.controller.PlayerControlled())
                {
                    //do the dist calc
                    float tempDistance = Mathf.Pow((x - startX),2) + Mathf.Pow((y - startY),2);
                    if(currDistance > tempDistance)
                    {
                        //we'll move to the closet player controlled unit
                        newX = x;
                        newY = y;

                        currDistance = tempDistance;
                    }
                }
            }
        }

        return new MapCoords(newX, newY);
    }

}
