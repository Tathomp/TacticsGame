using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra
{

    public TileNode[,] board;

    ActorController currFaction;
    string movement;

    public Dijkstra(TileNode[,] board)
    {
        this.board = board;
    }

    public List<TileNode> GeneratePath(int sourceX, int sourceY, int targetX, int targetY)
    {
        currFaction = board[sourceX, sourceY].actorOnTile.actorData.controller;
        movement = board[sourceX, sourceY].actorOnTile.actorData.movement;

        if (UnitCanEnterTile(targetX, targetY) == false)
        {
            Debug.Log("Can't enter");
            return null;
        }

        Dictionary<TileNode, float> dist = new Dictionary<TileNode, float>();
        Dictionary<TileNode, TileNode> prev = new Dictionary<TileNode, TileNode>();

        List<TileNode> path = new List<TileNode>();
        List<TileNode> unvistied = new List<TileNode>();

        TileNode source = board[sourceX, sourceY];
        TileNode target = board[targetX, targetY];



        if (target.data.posX == source.data.posX && target.data.posY == source.data.posY)
        {
            path.Add(board[targetX, targetY]);
            return path;
        }

        dist[source] = 0;
        prev[source] = null;

        foreach (TileNode v in board)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvistied.Add(v);
        }

        while (unvistied.Count > 0)
        {
            TileNode u = null;

            foreach (TileNode possibleU in unvistied)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvistied.Remove(u);

            foreach (TileNode v in u.neighbors)
            {
                float alt = dist[u] + CoastToEnterTile(u.data.posX, u.data.posY, v.data.posX, v.data.posY);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        if (prev[target] == null)
        {
            return null;
        }

        TileNode curr = target;

        while (curr != null)
        {
            path.Add(curr);
            curr = prev[curr];
        }


        //change this to do somekind of BFS instead to find the nearest tile that's in range? idk
        

        while(path[0].actorOnTile != null)
        {
            path.RemoveAt(0);
            if(path.Count == 0)
            {
                break;
            }
        }
        

        path.Reverse();


        return path;
    }


    private bool UnitCanEnterTile(int targetX, int targetY)
    {
        if(board[targetX,targetY] == null)
        {
            return false;
        }

        if(board[targetX, targetY].type.UnitCanTravelHere(movement))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private float CoastToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {
        float cost = 0.0f;

        if(board[sourceX, sourceY].type.MovementTypeCostMap.ContainsKey(movement))
        {
            cost += board[sourceX, sourceY].type.MovementTypeCostMap[movement];
        }
        else
        {
            return Mathf.Infinity;
        }

        if (!UnitCanEnterTile(targetX, targetY))
        {
            return Mathf.Infinity;
        }
        else if (board[targetX, targetY].actorOnTile != null)
        {
            if (board[targetX, targetY].actorOnTile.actorData.controller != currFaction)
            {
                return cost += 100;
            }
        }

        if (sourceX != targetX && sourceY != targetY)
        {
            cost += 0.001f;
        }

        return cost;
    }
}
