using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RelationshipEntry
{
    public string Actor1;
    public string Actor2;
    public RelationshipLevel RelationshipRequired;  

    public RelationshipEntry(string a1, string a2, RelationshipLevel rl)
    {
        Actor1 = a1;
        Actor2 = a2;
        RelationshipRequired = rl;
    }

}
