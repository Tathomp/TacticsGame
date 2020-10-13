using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RelationshipReward : Reward
{
    int AmtOfRelship;

    public RelationshipReward(int amt)
    {
        AmtOfRelship = amt;
    }

    public override Reward Copy()
    {
        return new RelationshipReward(AmtOfRelship);
    }

    public override void DistributeReward(BoardManager bm)
    {
        List<ActorData> party = bm.party.GetSelectedAndAliveActors();

        foreach (ActorData actor1 in party)
        {
            foreach (ActorData actor2 in party)
            {
                if(actor1.Relationships.HasRelationship(actor2.GetKey()))
                {
                    actor1.Relationships.AddRelationship(actor2.GetKey(), AmtOfRelship);
                }
            }
        }
    }

    public override string RewardString()
    {
        return "Relationship: " + AmtOfRelship;
    }
}
