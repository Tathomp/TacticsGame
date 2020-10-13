using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractionRequirements
{
    public int minRange;
    public string actorRequired;

    public InteractionRequirements(int minRange, string actorRequired)
    {
        this.minRange = minRange;
        this.actorRequired = actorRequired;
    }

    public InteractionRequirements(int minRange)
    {
        this.minRange = minRange;
        this.actorRequired = "";
    }

    public InteractionRequirements(string actorRequired)
    {
        this.minRange = -1;
        this.actorRequired = actorRequired;
    }



    public InteractionRequirements Copy()
    {
        return new InteractionRequirements(minRange, actorRequired);
    }

    public bool RequriementsMet(int dist, string actor)
    {
        return CheckActor(actor) && CheckDist(dist);
    }

    bool CheckDist(int dist)
    {
        return (minRange == -1 || minRange == dist);
    }

    bool CheckActor(string actor)
    {
        return (actorRequired == "" || actorRequired == actor);
    }
}
