using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ObjectiveComponent
{
    public abstract bool ObjectiveComplete(BoardManager bm);

    public abstract string PrintProgress();

    public abstract ObjectiveComponent Copy();
}
