using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoLengthLimit : EffectLengthBehavior
{
    public NoLengthLimit()
    {

    }

    public EffectLengthBehavior Copy()
    {
        return new NoLengthLimit();
    }

    //the is no turn limit for this effect
    //always return false so that the end behoviour wont run
    //just from turns processing
    public bool EndETimerOver()
    {
        return false;
    }
}
