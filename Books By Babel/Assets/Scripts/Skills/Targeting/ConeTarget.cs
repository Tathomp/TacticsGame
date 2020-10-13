using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConeTarget : ITargetable
{
    public int raidus;

    public ConeTarget(List<string> impassableTiles, bool stopOnOccupied, int radius)
        : base(impassableTiles, stopOnOccupied)
    {
        this.raidus = radius;
    }

    public override List<TileNode> TargetTiles(Actor source, TileNode center)
    {
        int tempX, tempY, x, y;

        int StartX = source.GetPosX() - center.data.posX;
        int StartY = source.GetPosY() - center.data.posY;

        x = center.data.posX;
        y = center.data.posY;



        List<TileNode> nodes = new List<TileNode>();
        BoardManager bm = Globals.GetBoardManager();
        HashSet<TileNode> visited = new HashSet<TileNode>();

        Queue<TileNode> nodeQueue = new Queue<TileNode>();
        nodeQueue.Enqueue(center);


        while(nodeQueue.Count > 0)
        {
            TileNode currNode = nodeQueue.Dequeue();

            if( ((Mathf.Abs(currNode.data.posX - x) > raidus) || (Mathf.Abs(currNode.data.posY - y)) > raidus ) == false)
            {

                if (IsTileValid(currNode))
                {
                    nodes.Add(currNode);

                    tempX = currNode.data.posX;
                    tempY = currNode.data.posY;


                    if (
                        Mathf.Abs( Mathf.Abs(Mathf.Abs(x) - Mathf.Abs(source.GetPosX()))) == 
                        Mathf.Abs( Mathf.Abs(Mathf.Abs(y) - Mathf.Abs(source.GetPosY())))
                        )
                    {
                        if(ValidTile(x, y, tempX, tempY))
                        {

                            if (StartX < 0 && StartY < 0)
                            {
                                //top right 
                                AddToQueue(nodeQueue, visited, tempX + 0, tempY + 1);
                                AddToQueue(nodeQueue, visited, tempX + 1, tempY + 0);

                            }
                            else if(StartX > 0 && StartY < 0)
                            {
                                AddToQueue(nodeQueue, visited, tempX + 0, tempY + 1);
                                AddToQueue(nodeQueue, visited, tempX - 1, tempY + 0);
                            }
                            else if(StartX > 0 && StartY > 0)
                            {
                                AddToQueue(nodeQueue, visited, tempX + 0, tempY - 1);
                                AddToQueue(nodeQueue, visited, tempX - 1, tempY + 0);
                            }
                            else if(StartX < 0 && StartY > 0)
                            {
                                AddToQueue(nodeQueue, visited, tempX + 0, tempY - 1);
                                AddToQueue(nodeQueue, visited, tempX + 1, tempY + 0);
                            }
                        }


                    }
                    else
                    {

                        int magx = Mathf.Abs(StartX);
                        int magY = Mathf.Abs(StartY);

                        if(magx > magY || magY == 0)
                        {
                            if (StartX < 0) // move right on the x position [x+1, y-1,y-0y+1]
                            {

                                AddToQueue(nodeQueue, visited, tempX + 1, tempY + 1);
                                AddToQueue(nodeQueue, visited, tempX + 1, tempY + 0);
                                AddToQueue(nodeQueue, visited, tempX + 1, tempY - 1);

                            }
                            else if (StartX > 0) // move left
                            {
                                AddToQueue(nodeQueue, visited, tempX - 1, tempY + 1);
                                AddToQueue(nodeQueue, visited, tempX - 1, tempY + 0);
                                AddToQueue(nodeQueue, visited, tempX - 1, tempY - 1);

                            }
                        }
                        else
                        {
                            if (StartY < 0)
                            {
                                //up
                                AddToQueue(nodeQueue, visited, tempX + 1, tempY + 1);
                                AddToQueue(nodeQueue, visited, tempX + 0, tempY + 1);
                                AddToQueue(nodeQueue, visited, tempX - 1, tempY + 1);

                            }
                            else if (StartY > 0)
                            {
                                //down
                                AddToQueue(nodeQueue, visited, tempX + 1, tempY - 1);
                                AddToQueue(nodeQueue, visited, tempX + 0, tempY - 1);
                                AddToQueue(nodeQueue, visited, tempX - 1, tempY - 1);

                            }
                        }
                    }
                }
            }            
        }
        

        return nodes;

    }




    public bool ValidTile(int sourceX, int sourceY, int tileX, int tileY)
    {



        int x = Mathf.Abs(Mathf.Abs(tileX) - Mathf.Abs(sourceX));
        int y = Mathf.Abs(Mathf.Abs(tileY) - Mathf.Abs(sourceY));

        // if tilex - sourcex == tiley - soucey, then it's a diagonal tile
        // if tilex - 1 <= radius, then it's valid
        //
        if (x == y)
        {
            //directly diagonal
            if((x - 1) < raidus)
            {
                return true;
            }
        }
        else
        {
            // take absolute value of tilex, tiley
            // (tilex - sourcex) + (tiley - sourcey) <= radius

            if ((x + y) < raidus)
            {
                return true;
            }
        }

        return false;
    }
}
