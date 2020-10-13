using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SummonUnitEvent : Event
{
    private string ActorID;
    private MapCoords SpawnPosition;


    public SummonUnitEvent(string id, string actorToSummon, MapCoords coords) : base(id)
    {
        this.ActorID = actorToSummon;
        this.SpawnPosition = coords;
    }

    public override DatabaseEntry Copy()
    {
        SummonUnitEvent e = new SummonUnitEvent(key, ActorID, SpawnPosition);

        foreach (Flags flag in flags)
        {
            e.flags.Add(flag);
        }

        return e;
    }

    public override string DisplayText()
    {
        return "";
    }

    public override void FireEvent()
    {
        Globals.SpawnMonster(ActorID, SpawnPosition);
    }
}
