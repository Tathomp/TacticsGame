using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AddObjectiveEvent : Event
{
    private ObjectiveComponent objectiveToAdd;
    private bool sideObjective;

    public AddObjectiveEvent(string id, ObjectiveComponent oc, bool sideOjbective=true) : base(id)
    {
        objectiveToAdd = oc;
        this.sideObjective = sideOjbective;
    }

    public override DatabaseEntry Copy()
    {
        AddObjectiveEvent e = new AddObjectiveEvent(GetKey(), objectiveToAdd, sideObjective);

        CopyFlags(e);

        return e;

    }

    public override string DisplayText()
    {
        return "";
    }

    public override void FireEvent()
    {
        if(EventShouldFire())
        {
           Mission m =  Globals.GetBoardManager().currentMission;

            if(sideObjective)
            {
                m.sideObjectives.Add(objectiveToAdd);
            }
            else
            {
                m.mainObjectives.Add(objectiveToAdd);
            }

        }
    }
}
