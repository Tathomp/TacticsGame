using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS{

    public TileNode[,] board;

    // Map Size
    int sizeX, sizeY;


	public BFS(TileNode[,] board)
    {
        this.board = board;

        sizeX = board.GetLength(0);
        sizeY = board.GetLength(1);
    }


    // BFS that takes into account the cost to move in to a tile node
    //
    public bool[,] WeightedBFS(int range, int sourceX, int sourceY, string movementType)
    {
        bool[,] visited = new bool[sizeX, sizeY];
        bool[,] reachable = new bool[sizeX, sizeY];
        int[,] costMap = new int[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                costMap[x, y] = int.MaxValue;
            }
        }

        Queue<TileNode> tileQueue = new Queue<TileNode>();

        tileQueue.Enqueue(board[sourceX, sourceY]);
        visited[sourceX, sourceY] = true;
        reachable[sourceX, sourceY] = true;
        costMap[sourceX, sourceY] = 0;

        do
        {
            TileNode currNode = tileQueue.Dequeue();


            foreach (TileNode tile in currNode.neighbors)
            {
                //check if the cost to get to this is fine
                //new tile's cost would be the previous tile's cost plus the current tiles cost
                //check to see if that sum is less that the current, it is replace the old sum
                int x = tile.data.posX;
                int y = tile.data.posY;


                int totalcost = costMap[currNode.data.posX, currNode.data.posY];

                //change this to check factions, units of the same faction be able to move through each other
                if (tile.actorOnTile != null || !tile.type.UnitCanTravelHere(movementType))
                {
                    totalcost = int.MaxValue;
                }
                else
                {
                    totalcost += tile.type.MovementTypeCostMap[movementType];
                }


                if (costMap[x, y] > totalcost)
                {
                    costMap[x, y] = totalcost;
                }
                //then we can visit this tile if we want, queue it and look at it's neighbors
                if (totalcost <= range && !visited[x, y])
                {
                    reachable[x, y] = true;
                    tileQueue.Enqueue(tile);
                    visited[x, y] = true;

                }


            }

        } while (tileQueue.Count > 0);

        return reachable;
    }



    public bool[,] ValidBFS(int maxRange, int minRange, int sourceX, int sourceY, List<string> tilesToBlack, bool blockOccupiedTiles)
    {
        bool[,] visited = new bool[sizeX, sizeY];
        bool[,] reachable = new bool[sizeX, sizeY];
        int[,] costMap = new int[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                costMap[x, y] = int.MaxValue;
            }
        }

        Queue<TileNode> tileQueue = new Queue<TileNode>();

        tileQueue.Enqueue(board[sourceX, sourceY]);
        visited[sourceX, sourceY] = true;
        reachable[sourceX, sourceY] = true;
        costMap[sourceX, sourceY] = 0;

        do
        {
            TileNode currNode = tileQueue.Dequeue();


            foreach (TileNode tile in currNode.neighbors)
            {
                //check if the cost to get to this is fine
                //new tile's cost would be the previous tile's cost plus the current tiles cost
                //check to see if that sum is less that the current, it is replace the old sum
                int x = tile.data.posX;
                int y = tile.data.posY;


                int totalcost = costMap[currNode.data.posX, currNode.data.posY] + 1;

                if(tilesToBlack.Contains(tile.type.GetKey()))
                {
                    totalcost = int.MaxValue;
                }


                if (costMap[x, y] > totalcost)
                {
                    costMap[x, y] = totalcost;
                }
                //then we can visit this tile if we want, queue it and look at it's neighbors
                if (totalcost <= maxRange && !visited[x, y])
                {
                    if (totalcost > minRange)
                    {
                        reachable[x, y] = true;


                        if(((blockOccupiedTiles == true && tile.actorOnTile != null) || tilesToBlack.Contains(tile.type.GetKey())) == false)
                        {
                            tileQueue.Enqueue(tile);

                        }
                    }
                    else
                    {
                        //we still want to look at the neighbors of tiles that are in the minimum range
                        //
                        reachable[x, y] = false;
                        tileQueue.Enqueue(tile);

                    }
                }

                visited[x, y] = true;

            }

        } while (tileQueue.Count > 0);

        return reachable;
    }



    /// A breadth first search that ignores the movement cost (the weights) 
    /// of each node. 
    public bool[,] UnWeightedBFS(int maxRange, int minRange, int sourceX, int sourceY)
    {
        bool[,] visited = new bool[sizeX, sizeY];
        bool[,] reachable = new bool[sizeX, sizeY];
        int[,] costMap = new int[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                costMap[x, y] = int.MaxValue;
            }
        }

        Queue<TileNode> tileQueue = new Queue<TileNode>();

        tileQueue.Enqueue(board[sourceX, sourceY]);
        visited[sourceX, sourceY] = true;
        reachable[sourceX, sourceY] = true;
        costMap[sourceX, sourceY] = 0;

        do
        {
            TileNode currNode = tileQueue.Dequeue();


            foreach (TileNode tile in currNode.neighbors)
            {
                //check if the cost to get to this is fine
                //new tile's cost would be the previous tile's cost plus the current tiles cost
                //check to see if that sum is less that the current, it is replace the old sum
                int x = tile.data.posX;
                int y = tile.data.posY;


                int totalcost = costMap[currNode.data.posX, currNode.data.posY] + 1;

                if (costMap[x, y] > totalcost)
                {
                    costMap[x, y] = totalcost;
                }
                //then we can visit this tile if we want, queue it and look at it's neighbors
                if (totalcost <= maxRange && !visited[x, y])
                {
                    if(totalcost > minRange)
                    reachable[x, y] = true;
                    tileQueue.Enqueue(tile);
                }

                visited[x, y] = true;

            }

        } while (tileQueue.Count > 0);

        return reachable;
    }

}
