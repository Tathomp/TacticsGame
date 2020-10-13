using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelationshipButton : MonoBehaviour {

    public Text Description;

    public void InitDisplay(string actor1, string actor2, RelationshipLevel relationshipLevel)
    {
        Description.text = actor1 + " and " + actor2 + ": " + relationshipLevel.ToString();
    }
}
