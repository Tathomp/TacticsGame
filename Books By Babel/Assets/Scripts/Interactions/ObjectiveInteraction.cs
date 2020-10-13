using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectiveInteraction : Interaction
{
    public string objectiveKey; //this key should match with the objective we want to set

    public ObjectiveInteraction(string objectiveKey, string fp):base (fp)
    {
        this.objectiveKey = objectiveKey;
    }

    public override Interaction Copy()
    {
        

        return new ObjectiveInteraction(objectiveKey, fp) { requirements = requirements.Copy()};
    }

    public override void ExecuteInteraction(Mission currentMission)
    {
        foreach (ObjectiveComponent ioc in currentMission.GetAllObjectives())
        {
            if(ioc is InteractionObjectiveComponent)
            {
                if (((InteractionObjectiveComponent)ioc).id == objectiveKey)
                {
                    ((InteractionObjectiveComponent)ioc).triggered = true;
                }
            }

        }

    }
}
