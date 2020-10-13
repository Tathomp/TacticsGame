using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//Filters out a particular type of target
//
public abstract class TargetFiltering
{
    public abstract void FilterTargetNodes(List<TileNode> nodes, Actor source, TileNode center);
    public abstract bool ShouldNodeBeFiltered(TileNode nodeToFilter, bool sourceFaction, Actor source);
    public abstract TargetFiltering Copy();
}
