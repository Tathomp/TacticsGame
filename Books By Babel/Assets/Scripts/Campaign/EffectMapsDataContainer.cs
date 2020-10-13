using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectMapsDataContainer
{
    public RelationshipMap relationshipMap;

    public EffectMapsDataContainer()
    {
        relationshipMap = new RelationshipMap();
    }
}
