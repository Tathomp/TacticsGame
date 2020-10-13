using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterReward : Reward
{
    string characterKey;

    public CharacterReward( string characterKey)
    {
        this.characterKey = characterKey;
    }

    public override Reward Copy()
    {
        CharacterReward r = new CharacterReward(characterKey);

        return r;
    }

    public override void DistributeReward(BoardManager bm)
    {

        ActorData m = Globals.campaign.contentLibrary.actorDB.GetCopy(characterKey);

        bm.party.partyCharacter.Add(m);


    }

    public override string RewardString()
    {
        string s = "Unlock character: " + characterKey;

        return s;
    }
}
