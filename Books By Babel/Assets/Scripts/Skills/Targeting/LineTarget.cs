using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineTarget : ITargetable
{
    // Can probably move this to the base abstract class
    //

    public LineTarget(List<string> impassableTiles, bool stopOnOccupied) : base(impassableTiles, stopOnOccupied)
    {
    }


    public LineTarget(List<string> impassableTiles, bool stopOnOccupied, bool pierceUnits, int maxUnitsToPierce) : base(impassableTiles, stopOnOccupied, pierceUnits, maxUnitsToPierce)
    {

    }

    public override List<TileNode> TargetTiles(Actor source, TileNode center)
    {
        int sourceX, sourceY, targetX, targetY, distX, distY;

        sourceX = source.GetPosX();
        sourceY = source.GetPosY();

        targetX = center.data.posX;
        targetY = center.data.posY;



        distX = sourceX - targetX;
        distY = sourceY - targetY;

        BoardManager bm = Globals.GetBoardManager();



        List<TileNode> nodes = new List<TileNode>();
        TileNode currNode = bm.pathfinding.GetTileNode(source);

        currPierce = 0;
        int s = 0;

        while(currNode != center)
        {

            int x = currNode.data.posX;
            int y = currNode.data.posY;
            s++;


            int magX = AbsoluteDistance(currNode.data.posX, targetX);
            int magY = AbsoluteDistance(currNode.data.posY, targetY);


            if (s >=100)
            {
                Debug.Log("WHILE LOOP FUCKED");
                return nodes;
            }


            if (center.data.posX - currNode.data.posX == 0)
            {
                if(center.data.posY - currNode.data.posY < 0)//downward
                {
                    if (AddList(nodes, center, bm, x + 0, y - 1, x + 0, y -1) == false)
                    {
                        //No valid tile
                        return nodes;

                    }
                }
                else
                {
                    if (AddList(nodes, center, bm, x + 0, y + 1, x + 0, y + 1) == false)
                    {
                        //No valid tile
                        return nodes;

                    }
                }
            }
            else if (center.data.posY - currNode.data.posY == 0)
            {
                if (center.data.posX - currNode.data.posX < 0)//downward
                {
                    if (AddList(nodes, center, bm, x - 1, y +0, x - 1, y + 0) == false)
                    {
                        //No valid tile
                        return nodes;

                    }
                }
                else
                {
                    if (AddList(nodes, center, bm, x + 1, y + 0, x + 1, y + 1) == false)
                    {
                        //No valid tile
                        return nodes;

                    }
                }

            }
            else  if (magY >= magX)
            //if(true)
            {
                //ties follow the magX pattern
                if (distX < 0 && distY < 0)
                {
                    if (AddList(nodes, center, bm, x + 0, y + 1, x + 1, y + 0) == false)
                    {
                        //No valid tile
                        return nodes;
                        
                    }

                }
                else if (distX > 0 && distY < 0)
                {
                    if (AddList(nodes, center, bm, x + 0, y + 1, x - 1, y + 0) == false)
                    {
                        return nodes;
                    }
                }
                else if (distX > 0 && distY > 0)
                {
                    if (AddList(nodes, center, bm, x + 0, y - 1, x - 1, y + 0) == false)
                    {
                       
                            //No valid tile
                            return nodes;
                        
                    }

                    }
                else if (distX < 0 && distY > 0)
                {
                    if (AddList(nodes, center, bm, x + 0, y - 1, x + 1, y + 0) == false)
                    {
                            //No valid tile
                            return nodes;
                        
                    }
                }
            }
            else
            {
                //magY is larger
                //ties follow the magX pattern
                if (distX < 0 && distY < 0)
                {
                    if (AddList(nodes, center, bm, x + 1, y + 0,
                                                    x + 0, y + 1) == false)
                    {
                        //No valid tile
                        return nodes;

                    }

                }
                else if (distX > 0 && distY < 0)
                {
                    if (AddList(nodes, center, bm, x - 1, y + 0,
                                                    x - 0, y + 1) == false)
                    {
                        return nodes;
                    }
                }
                else if (distX > 0 && distY > 0)
                {
                    if (AddList(nodes, center, bm, x - 1, y + 0, x + 0, y - 1) == false)
                    {

                        //No valid tile
                        return nodes;

                    }

                }
                else if (distX < 0 && distY > 0)
                {
                    if (AddList(nodes, center, bm, x + 1, y + 0, x + 0, y - 1) == false)
                    {
                        //No valid tile
                        return nodes;

                    }
                }

            }

            currNode = nodes[nodes.Count - 1];

        }

        //nodes.RemoveAt(0);
        nodes.Remove(bm.pathfinding.GetTileNode(source));
        return nodes;
    }

    //If this returns false it's time to stop
    bool AddList(List<TileNode> nodes, TileNode center, BoardManager bm, int x1, int y1, int x2, int y2)
    {
        TileNode temp1 = bm.pathfinding.GetTileNode(x1, y1);
        TileNode temp2 = temp1;


        if (IsTileValid(temp1))
        {
            nodes.Add(temp1);
            return true;

        }

        return false;
    }
}
