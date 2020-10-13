using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    public TileNode[,] tiles;
    public int sizeX, sizeY;

    Dijkstra dijkstra;
    BFS bfs;

    public Pathfinding(int sizeX, int sizeY)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;

        tiles = new TileNode[sizeX, sizeY];
        
        dijkstra = new Dijkstra(tiles);
        bfs = new BFS(tiles);
    }


    public void PopulateNieghbors()
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if(x > 0)
                {
                    tiles[x, y].neighbors.Add(tiles[x - 1, y]);
                }

                if (x < sizeX -1)
                {
                    tiles[x, y].neighbors.Add(tiles[x + 1, y]);
                }

                if (y > 0)
                {
                    tiles[x, y].neighbors.Add(tiles[x, y - 1]);
                }

                if (y < sizeY - 1)
                {
                    tiles[x, y].neighbors.Add(tiles[x, y + 1]);
                }
            }
        }
    }

    /// <summary>
    ///     Moves the actor the input coords
    ///     Removes the actor from where it previous was
    ///     Add stats based on the destination
    ///     Removes stats based on where it was
    /// </summary>
    /// <param name="actor"> actor to move</param>
    /// <param name="posX"> new postion to move to </param>
    /// <param name="posY"> new y position to move to</param>
    /// 
    public void MoveWithOnMoveBuffs(Actor actor, int posX, int posY)
    {
        int prevX = actor.GetPosX();
        int prevY = actor.GetPosY();

        TileNode prevTile = tiles[prevX, prevY];
        TileNode currTile = tiles[posX, posY];

        actor.actorData.buffContainer.OnMove(actor.actorData, prevTile, currTile);

        MoveWithOutOnMoveBuffs(actor, posX, posY);
    }

    public void MoveWithOutOnMoveBuffs(Actor actor, int posX, int posY)
    {
        int prevX = actor.GetPosX();
        int prevY = actor.GetPosY();

        TileNode prevTile = tiles[prevX, prevY];
        TileNode currTile = tiles[posX, posY];



        //remove stat bonuses and actor from tile
        if (prevTile.actorOnTile != null)
        {
            //deprication
            actor.ReduceStats(prevTile.type.tileBonuses);

            prevTile.ExitTileEffects();
        }

        prevTile.actorOnTile = null;

        //add stat bonues, actor to tile, and update pos
        currTile.actorOnTile = actor;


        actor.SetPosX(posX);
        actor.SetPosY(posY);


        actor.AddStats(currTile.type.tileBonuses);

        currTile.EnterTileEffects();
    }

    /// <summary>
    /// Removes all bonuses provided by tiles 
    /// </summary>
    public void RemoveAllActorStats()
    {
        foreach (TileNode item in tiles)
        {
            if(item.actorOnTile != null)
            {
                item.actorOnTile.ReduceStats(
                    tiles[item.actorOnTile.GetPosX(), item.actorOnTile.GetPosY()].type.tileBonuses);
            }
        }
    }

    //Undo the REmoveAllActorStats methods
    public void AddAllStats()
    {
        foreach (TileNode item in tiles)
        {
            if (item.actorOnTile != null)
            {
                item.actorOnTile.AddStats(
                    tiles[item.actorOnTile.GetPosX(), item.actorOnTile.GetPosY()].type.tileBonuses);
            }
        }
    }

    public List<TileNode> GenerateMovementPath(Actor actor, int destX, int destY)
    {
        return GenerateMovementPath(actor.GetPosX(), actor.GetPosY(), destX, destY);
    }

    public List<TileNode> GenerateMovementPath(Actor actor, int destX, int destY, int range)
    {
        List<TileNode> nodes = GenerateMovementPath(actor.GetPosX(), actor.GetPosY(), destX, destY);
        List<TileNode> pathWithRange = new List<TileNode>();

        if(nodes == null)
        {
            return null;
        }

        if(nodes.Count <= range)
        {
            return nodes;
        }

        for (int i = 0; i <= range; i++)
        {
            pathWithRange.Add(nodes[i]);
        }
        


        return pathWithRange;
    }

    public List<TileNode> GenerateMovementPath(int sourceX, int sourceY, int destX, int destY)
    {
        return dijkstra.GeneratePath(sourceX, sourceY, destX, destY);
    }

    public bool[,] GetMovementRange(ActorData data)
    {
        return WeightedBFS(data.currentStatCollection.GetValue(StatTypes.MovementRange), data.gridPosX, data.gridPosY, data.movement);
    }
    public bool[,] WeightedBFS(int range, int sourceX, int sourceY, string movementType)
    {
        return bfs.WeightedBFS(range, sourceX, sourceY, movementType);
    }


    public bool[,] UnWeightedBFS(int maxRange, int minRange, int sourceX, int sourceY)
    {
        return bfs.UnWeightedBFS(maxRange, minRange, sourceX, sourceY);
    }

    public bool[,] ValidBFS(int maxRange, int minRange, int sourceX, int sourceY, List<string> tilesToBloack, bool stopOnOccupy)
    {
        return bfs.ValidBFS(maxRange, minRange, sourceX, sourceY, tilesToBloack, stopOnOccupy);
    }

    public TileNode GetTileNode(MapCoords coord)
    {
        return GetTileNode(coord.X, coord.Y);
    }

    public TileNode GetTileNode(int x, int y)
    {
        if(InRange(x,y))
        {
            return tiles[x, y];

        }

        return null;
    }

    public Actor GetActorOnTile(int x, int y)
    {
        return tiles[x, y].actorOnTile;
    }

    public TileNode GetTileNode(Actor actor)
    {
        return tiles[actor.GetPosX(), actor.GetPosY()];
    }

    public bool InRange(int x, int y)
    {
        if(x >= 0 && y >= 0 && x < tiles.GetLength(0) && y < tiles.GetLength(1))
        {
            return true;
        }

        return false;
    }

    public bool HasActor(int x, int y)
    {
        if(tiles[x,y].actorOnTile == null)
        {
            return false;
        }

        return true;
    }

    public void AddTile(Tile tile, int x, int y)
    {
        tiles[x, y] = new TileNode(tile);
        tiles[x, y].InitTileEffectsVisuals();
    }

    public List<TileNode> GetNodes(bool[,] valid)
    {
        List<TileNode> nodes = new List<TileNode>();


        for (int x = 0; x < valid.GetLength(0); x++)
        {
            for (int y = 0; y < valid.GetLength(1); y++)
            {
                if(valid[x,y])
                {
                    nodes.Add(tiles[x, y]);
                }
            }
        }

        return nodes;
    }

    public bool[,] ValidBFS(IUseable currentSkill, TileNode startNode, Actor currentActor)
    {

        return ValidBFS(
            currentSkill.GetMaxRange(currentActor),
            currentSkill.GetMinRange(currentActor),
            startNode.data.posX,
            startNode.data.posY,
            ((Skill)currentSkill).targetType.immpassableTiles,
            ((Skill)currentSkill).targetType.stopOnOccupied); ;
    }


    public bool[,] GetSkillRange(IUseable skil, Actor d, int startX, int startY)
    {
       // List<TileNode> nodes = new List<TileNode>();

        bool[,] validMap = ValidBFS(
            skil.GetMaxRange(d),
            skil.GetMinRange(d),
            startX,
            startY,
            (skil).GetTargetType().immpassableTiles,
            (skil).GetTargetType().stopOnOccupied);

        if (skil.GetTargetFiltering() != null)
        {
            for (int x = 0; x < validMap.GetLength(0); x++)
            {
                for (int y = 0; y < validMap.GetLength(1); y++)
                {
                    if (validMap[x, y])
                    {
                        TileNode node = GetTileNode(x, y);

                        if (skil.FilterTileNode(d, node) == true)
                        {
                            validMap[x, y] = false;
                        }
                        else
                        {
                           // nodes.Add(node);
                        }
                    }
                }
            }
        }




        return validMap;
    }
}


