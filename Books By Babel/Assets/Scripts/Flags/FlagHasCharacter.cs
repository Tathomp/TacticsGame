using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlagHaveCharacter : Flags
{
    private string actorID;

    public FlagHaveCharacter(string id, string actorID) : base(id)
    {
        this.actorID = actorID;
    }

    public override bool CheckFlagStatus()
    {
        List<ActorData> actors = Globals.campaign.currentparty.partyCharacter;

        foreach (ActorData ad in actors)
        {
            if(ad.GetKey() == actorID)
            {
                return true;
            }
        }

        return false;
    }
}
