using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Interaction 
{
    public int maxRangeToInteract;
    public abstract void ExecuteInteraction(Mission currentMission);

    public InteractionRequirements requirements;

    public string fp;

    public Interaction(string fp)
    {
        this.fp = fp;
    }


    public bool MeetsRequirement(int dist, string actor)
    {
        if(requirements == null)
        {
            return true;
        }

        return requirements.RequriementsMet(dist, actor);
    }

    public abstract Interaction Copy();
}
