using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockTarget : ITargetable
{
    public int height, width;

    public BlockTarget(List<string> impassableTiles, bool stopOnOccupied, int length, int width) : base(impassableTiles, stopOnOccupied)
    {
        this.height = length;
        this.width = width;
    }

    public override List<TileNode> TargetTiles(Actor source, TileNode center)
    {
        List<TileNode> n = new List<TileNode>();

        //to the left of the caster
        //              5               3
        int deltaX = center.data.posX - source.GetPosX();
        int deltaY = center.data.posY - source.GetPosY();

        int tempHeight = 0, tempWdith = 0;

        int targetX = center.data.posX;
        int targetY = center.data.posY;

        int currx = 0;
        int curry = 0;

        int oddx = 0, oddy = 0;

        if(Mathf.Abs(deltaX) == Mathf.Abs(deltaY))
        {

            if(deltaY > 0 & deltaX > 0) //top right
            {
                while(tempWdith < width )
                {
                    tempHeight = 0;

                    while(tempHeight < height)
                    {
                        int x = tempHeight - currx;
                        int y = tempHeight + tempWdith- curry;

                        AddTile(n, targetX + x, targetY + y);
                        AddTile(n, targetX + y,targetY + x);

                                                                                                                                 
                        tempHeight++;
                    }

                    tempWdith++;

                    if (tempWdith % 2 == 0)
                    {
                        curry++;
                        currx++;

                        oddy++;
                    }
                    else
                    {
                        //  currx = 0;
                        oddx = 0;
                    }
                }
            }
            else if (deltaY > 0 & deltaX < 0) //top left
            {
                while (tempWdith < width)
                {
                    tempHeight = 0;

                    while (tempHeight < height)
                    {
                        int x = tempHeight - currx;
                        int y = tempHeight + tempWdith - curry;

                        AddTile(n, targetX - x, targetY + y);
                        AddTile(n, targetX - y, targetY + x);


                        tempHeight++;
                    }

                    tempWdith++;

                    if (tempWdith % 2 == 0)
                    {
                        curry++;
                        currx++;

                        oddy++;
                    }
                    else
                    {
                        //  currx = 0;
                        oddx = 0;
                    }
                }
            }
            else if (deltaY < 0 & deltaX < 0) //bottom left
            {
                while (tempWdith < width)
                {
                    tempHeight = 0;

                    while (tempHeight < height)
                    {
                        int x = tempHeight - currx;
                        int y = tempHeight + tempWdith - curry;

                        AddTile(n, targetX - x, targetY - y);
                        AddTile(n, targetX - y, targetY - x);


                        tempHeight++;
                    }

                    tempWdith++;

                    if (tempWdith % 2 == 0)
                    {
                        curry++;
                        currx++;

                        oddy++;
                    }
                    else
                    {
                        //  currx = 0;
                        oddx = 0;
                    }
                }
            }
            else if (deltaY < 0 & deltaX > 0) //bottom right
            {
                while (tempWdith < width)
                {
                    tempHeight = 0;

                    while (tempHeight < height)
                    {
                        int x = tempHeight - currx;
                        int y = tempHeight + tempWdith - curry;

                        AddTile(n, targetX + x, targetY - y);
                        AddTile(n, targetX + y, targetY - x);


                        tempHeight++;
                    }

                    tempWdith++;

                    if (tempWdith % 2 == 0)
                    {
                        curry++;
                        currx++;

                        oddy++;
                    }
                    else
                    {
                        //  currx = 0;
                        oddx = 0;
                    }
                }
            }
        }
        else if ( deltaX < 0 & Mathf.Abs(deltaX) > Mathf.Abs(deltaY)) // t0 the left
        {
            while(tempHeight < height)
            {
                int tempWidth = 0;

                while (tempWidth < width)
                {


                    AddTile(n, targetX - tempHeight, targetY - tempWidth);
                    AddTile(n, targetX - tempHeight, targetY + tempWidth);
                    tempWidth++;

                }
                tempHeight++;

            }
        }
        else if(deltaX > 0 & Mathf.Abs(deltaX) > Mathf.Abs(deltaY)) // right
        {
            while (tempHeight < height)
            {
                int tempWidth = 0;

                while (tempWidth < width)
                {


                    AddTile(n, targetX + tempHeight, targetY - tempWidth);
                    AddTile(n, targetX + tempHeight, targetY + tempWidth);
                    tempWidth++;

                }
                tempHeight++;

            }
        }
        else if(deltaY > 0 & Mathf.Abs(deltaX) < Mathf.Abs(deltaY))// bottom
        {
            while (tempHeight < height)
            {
                int tempWidth = 0;

                while (tempWidth < width)
                {


                    AddTile(n, targetX + tempWidth, targetY + tempHeight);
                    AddTile(n, targetX - tempWidth, targetY + tempHeight);
                    tempWidth++;

                }
                tempHeight++;

            }
        }
        else if(deltaY < 0 & Mathf.Abs(deltaX) < Mathf.Abs(deltaY)) //top
        {
            while (tempHeight < height)
            {
                int tempWidth = 0;

                while (tempWidth < width)
                {


                    AddTile(n, targetX + tempWidth, targetY - tempHeight);
                    AddTile(n, targetX - tempWidth, targetY - tempHeight);
                    tempWidth++;

                }
                tempHeight++;

            }
        }

        return n;
    }


    private void AddTile(List<TileNode> nodes, int x, int y)
    {
        if(Globals.GetBoardManager().pathfinding.InRange(x,y))
        {
            TileNode n = Globals.GetBoardManager().pathfinding.GetTileNode(x, y);
            if (nodes.Contains(n))
            {
                return;
            }
            nodes.Add(Globals.GetBoardManager().pathfinding.GetTileNode(x, y));
        }
    }
}
