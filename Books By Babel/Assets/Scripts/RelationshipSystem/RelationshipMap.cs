using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RelationshipMap
{
    /// <summary>
    /// Tuple one will be the two actors that are in the relationship
    /// Tulple two will be the relationship level 
    /// </summary>
    Dictionary<RelationshipEntry, string> RelationshipDict;
    int[] thresholds;

    public RelationshipMap()
    {
        RelationshipDict = new Dictionary<RelationshipEntry, string>();
        thresholds = new int[] { 100, 200, 300, 400 };
    }

    public void AddRelationship(string actorkey, string actorkey2, RelationshipLevel relationshipThreshold, string cutscenekey)
    {
        RelationshipDict[new RelationshipEntry(actorkey, actorkey2, relationshipThreshold)] = cutscenekey;
    }

    public RelationshipLevel GetRelationshipLevel(int k)
    {
        if (k > thresholds[3])
        {
            return RelationshipLevel.Bound;
        }

        if (k > thresholds[2])
        {
            return RelationshipLevel.A;
        }

        if (k > thresholds[1])
        {
            return RelationshipLevel.B;
        }

        return RelationshipLevel.C;
    }

    public string GetCutSceneKey(string actor1, string actor2, int v)
    {
        return RelationshipDict[new RelationshipEntry(actor1, actor2, GetRelationshipLevel(v))];

    }

}
