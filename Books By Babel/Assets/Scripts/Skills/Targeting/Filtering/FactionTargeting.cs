using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FactionTargeting : TargetFiltering
{ //really should rename this class


    public FilterType filterType;

    //If true, filter out tiles that dont share a movement type with the source
    public bool filterOutMovementType;


    public FactionTargeting(FilterType filterType, bool filterOutMovementType)
    {
        this.filterType = filterType;
        this.filterOutMovementType = filterOutMovementType;
    }

    public override TargetFiltering Copy()
    {
        return new FactionTargeting(filterType, filterOutMovementType);
    }

    public override void FilterTargetNodes(List<TileNode> nodes, Actor source, TileNode center)
    {
        bool sameFaction = source.actorData.controller.PlayerControlled();

        for (int i = nodes.Count - 1; i >= 0; i--)
        {
            if(ShouldNodeBeFiltered(nodes[i], sameFaction, source))
            {
                nodes.RemoveAt(i);
            }
        }

    }

    public override bool ShouldNodeBeFiltered(TileNode nodeToFilter, bool sourceFaction, Actor source)
    {

        if(filterOutMovementType)
        {
            if(nodeToFilter.type.GetMovementTypes().Contains(source.actorData.movement) == false)
            {
                return true;
            }
        }

        if(filterType == FilterType.Ally)
        {
            return AllyFilter(nodeToFilter, sourceFaction);
        }
        else if(filterType == FilterType.Enemy)
        {
            //Our base case here is just to check enemy filter
            //later this should be expaneded
            return EnemyFilter(nodeToFilter, sourceFaction);
        }
        else
        {
            //The only other two options are Empty and Unit here
            return TileOccupancyFilter(nodeToFilter);
        }

        //Base case: don't filter out the node
    }


    #region Filters
    public bool AllyFilter(TileNode nodeToFilter, bool sourceFaction)
    {
        if(nodeToFilter.actorOnTile != null)
        {
            //Check if it has the same faction at the caster
            if (nodeToFilter.actorOnTile.ActorsController().PlayerControlled() == sourceFaction)
            {
                return true;
            }
        }

        return false;
    }


    public bool EnemyFilter(TileNode nodeToFilter, bool sourceFaction)
    {
        if (nodeToFilter.actorOnTile != null)
        {
            //Check if it has the same faction at the caster
            if (nodeToFilter.actorOnTile.ActorsController().PlayerControlled() != sourceFaction)
            {
                return true;
            }
        }

        return false;
    }

    public bool TileOccupancyFilter(TileNode nodeToFilter)
    {
        if(nodeToFilter.actorOnTile != null && filterType == FilterType.Occupied)
        {
            //So the tile has someone on it and it's not a valid target because of that
            //right? so filter it out?
            return true;
        }
        else if(nodeToFilter.actorOnTile == null && filterType == FilterType.Empty)
        {
            return true;
        }

        return false;
    }
    #endregion
}

public enum FilterType
{
    Ally,
    Enemy,
    Empty,
    Occupied,
}

