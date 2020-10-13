using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventInteraction : Interaction
{
    public string eventId;
    public string flagId;

    public EventInteraction(string eventId, string flagId, string fp) : base(fp)
    {
        this.eventId = eventId;
        this.flagId = flagId;
    }


    public override void ExecuteInteraction(Mission currentMission)
    {
        Event e = currentMission.GetEvent(eventId);
        if (e == null)
            return;

        Flags f = e.GetFlag(flagId);

        if (f == null)
            return;


        //We're just going to 
        ((FlagBool)f).ChangeFlag(true);
    }


    public override Interaction Copy()
    {
        return new EventInteraction(eventId, flagId, fp) { requirements = requirements.Copy()};
    }
}
