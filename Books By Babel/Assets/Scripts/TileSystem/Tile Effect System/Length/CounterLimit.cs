using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CounterLimit : EffectLengthBehavior
{
    int current, limit;

    public CounterLimit(int limit)
    {
        current = 0;
        this.limit = limit;
    }

    public EffectLengthBehavior Copy()
    {
        return new CounterLimit(limit);
    }

    public bool EndETimerOver()
    {
        current++;

        if (current == limit)
        {
            return true;
        }
        else
            return false;
    }
}
