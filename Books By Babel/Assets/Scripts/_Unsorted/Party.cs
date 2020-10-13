using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Party
{
    //this should be loaded via saved data at some point
    public List<ActorData> partyCharacter;
    public Inventory partyInvenotry;

    public int Credits;

    public Party()
    {
        partyCharacter = new List<ActorData>();
        partyInvenotry = new Inventory(50);

        Credits = 1000;
    }

    public int NumberOfSelected()
    {
        int count = 0;

        foreach (ActorData actor in partyCharacter)
        {
            if (actor.selected)
                count++;
        }

        return count;
    }

    public List<ActorData> GetSelectedAndAliveActors()
    {
        List<ActorData> actors = new List<ActorData>();

        foreach (ActorData a in partyCharacter)
        {
            if (a.selected && a.isAlive)
            {
                actors.Add(a);
            }
        }

        return actors;
    }

    public bool AddItemToIventory(string key)
    {
        if(Globals.campaign.GetItemData(key).DisappearsInventory)
        {
            return false;
        }
        

        return partyInvenotry.AddItem(key);
    }

    public ActorData KeyToData(string key)
    {
        foreach (ActorData actorData in partyCharacter)
        {
            if(actorData.GetKey() == key)
            {
                return actorData;
            }
        }

        return null;
    }

    public int GetAvgLevel()
    {
        float total = 0;
        float numOfActors = 0;

        foreach (ActorData actor in partyCharacter)
        {
            total += actor.Level;
            numOfActors++;
        }

        return Mathf.RoundToInt( total / numOfActors);
    }
}
