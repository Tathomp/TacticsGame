 using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MissionHandler
{
    //private List<Mission> avaliableMissions;

    private SavedDatabase<Mission> missionDB;

    //mission id, list of flags that need to be true for mission to be avalible
    private Dictionary<string, List<string>> flags;

    private List<string> completedMissions;
    //Might move this to a new class

    public List<string> MissionsAccepted;



    public MissionHandler()
    {
        //avaliableMissions = new List<Mission>();
        missionDB = new SavedDatabase<Mission>();
        flags = new Dictionary<string, List<string>>();
        completedMissions = new List<string>();

        MissionsAccepted = new List<string>();
    }

    public void RemoveNullList()
    {
        //avaliableMissions.RemoveAll(mission => mission == null || mission.completed);
    }

    public List<string> PopulateAvaliableMissions()
    {
        //RemoveNullList();

        List<string> avaliableMissions = new List<string>();

        string[] keys = flags.Keys.ToArray();

        foreach (string key in keys)
        {
            if(CheckFlag(key) && CheckCompletion(key) == false && CheckIfAccepted(key) == false)
            {
                avaliableMissions.Add(key);
            }
        }

        return avaliableMissions;

    }


    public bool CheckIfAccepted(string key)
    {
        return MissionsAccepted.Contains(key);
    }

    public bool CheckCompletion(string key)
    {
        return completedMissions.Contains(key);
    }

    public void MissionHasBeenCompleted(string key)
    {
        completedMissions.Add(key); //we may want to reduce reduantcy (i.e the same mission getting added again and again if the mission is repeated
    }

    #region Random Mission Stuff

    public void GenerateRandomMissions(WorldMap currMap)
    {
        List<Bar> bars = currMap.GetAllBars();

        foreach (Bar bar in bars)
        {
                       
            int max = bar.maxrandommisison;
            int min = bar.minrandommissions;
            int avaliableSlots = max - bar.activeSpawns.Count;

            if (avaliableSlots == 0)
            {
                return; //no new spawns
            }

            List<RandomSpawnData> validOptions = new List<RandomSpawnData>();

            foreach (RandomSpawnData data in bar.randomspawnsPool)
            {
                if (CheckFlag(data.missionID) & MissionsAccepted.Contains(data.missionID) == false)
                {
                    validOptions.Add(data.Copy());
                }
            }

            if (validOptions.Count <= avaliableSlots)
            {
                bar.AddRandomSpawnData(validOptions);
                return;
            }


            List<RandomSpawnData> dataToAdd = new List<RandomSpawnData>();


            while (avaliableSlots > 0)
            {

                int index = UnityEngine.Random.Range(0, validOptions.Count - 1);

                dataToAdd.Add(validOptions[index]);
                validOptions.RemoveAt(index);

                avaliableSlots--;
            }

            bar.AddRandomSpawnData(dataToAdd);

        }
    }


    #endregion

    private bool RequirementsMet(List<string> requirements)
    {
        Dictionary<string, Flags> flag = Globals.campaign.GlobalFlags;
 
        foreach (string  k in requirements)
        {
            if(flag[k].CheckFlagStatus() == false)
            {
                return false;
            }
        }

        return true;
    }

    bool CheckFlag(string key)
    {

        return RequirementsMet(flags[key]);


    }


    #region Adders
    public void AddMission(Mission mission)
    {
        // We'll add a flag for if the mission has been completed
        Globals.campaign.GlobalFlags.Add(mission.GetKey(), new FlagBool("", false));
        missionDB.AddEntry(mission);
        flags[mission.GetKey()] = new List<string>();

    }

    
    public void AddFlag(string missionID, string flagName)
    {
        flags[missionID].Add(flagName);
    }
    #endregion



    #region Flag Getters
    List<string> GetFlags(string missionID)
    {
        return flags[missionID];
    }



    #endregion

    public List<string> GetAvaliableMissions()
    {
        return PopulateAvaliableMissions();
    }

    public List<string> GetAvaliableMissions(Bar b)
    {
        List<string> missionID = PopulateAvaliableMissions();


        foreach (RandomSpawnData data in b.activeSpawns)
        {
            if(MissionsAccepted.Contains(data.missionID) == false)
            {
                missionID.Add(data.missionID);
            }
        }

        return missionID;
    }

    public Mission GetEntryData(string key)
    {
        return missionDB.GetData(key);
    }

}
