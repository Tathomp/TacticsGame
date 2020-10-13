using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Mission : DatabaseEntry
{

    public bool started, completed, reoccuring;
    public string MissionName;
    public string mapName;
    public string worlmapID;
    public List<ActorData> npcs;
    public string descript;
    public MissionType missionType;

    public string int_cutsceneKey, end_cutscenekey;

    public List<string> missionsToUnlock; //this could probably be removed, i don't think it does anything rn

    //public MapDataModel map;
    public Dictionary<MapCoords, string> typeChangeDict; // this redunant? Class: SavedFileMission, Field: TileTypeStates

    public int maxUnitsAllowed;

    public List<ObjectiveComponent> mainObjectives, sideObjectives, secretObjectives, failstate;
    public RewardTable mainReward, sideRewards, secretRewards;
    public List<Event> MissionEvents;

    public List<MapEnchantment> MapEnchantments;

    public List<Tuple<MapCoords, List<string>>> initTileEffects;

    public int currentTurn;


    public Dictionary<string, Interaction> interactionMap;

    #region Init and Copying
    public Mission(string key) : base(key)
    {
        MissionEvents = new List<Event>();
        npcs = new List<ActorData>();
        missionsToUnlock = new List<string>();
        started = false;
        completed = false;
        reoccuring = false;

        interactionMap = new Dictionary<string, Interaction>();
        worlmapID = "";
        descript = "";
        int_cutsceneKey = "";
        end_cutscenekey = "";

        typeChangeDict = new Dictionary<MapCoords, string>();

        mainReward = new RewardTable();
        sideRewards = new RewardTable();
        secretRewards = new RewardTable();


        mainObjectives = new List<ObjectiveComponent>();
        sideObjectives = new List<ObjectiveComponent>();
        secretObjectives = new List<ObjectiveComponent>();
        failstate = new List<ObjectiveComponent>() { new AllAlliesDeadFailState() };


        MapEnchantments = new List<MapEnchantment>();

        currentTurn = 0;

        initTileEffects = new List<Tuple<MapCoords, List<string>>>();
    }

    public override DatabaseEntry Copy()
    {
        Mission temp = new Mission(key);


        foreach (ActorData actorData in npcs)
        {
            temp.npcs.Add(actorData.Copy() as ActorData);
        }


        foreach (string s in missionsToUnlock)
        {
            temp.missionsToUnlock.Add(s);
        }


        temp.started = started;
        temp.completed = completed;
        temp.reoccuring = reoccuring;
        temp.descript = descript;
        temp.worlmapID = worlmapID;

        temp.missionType = missionType;

        temp.int_cutsceneKey = int_cutsceneKey;
        temp.end_cutscenekey = end_cutscenekey; 

        
        temp.mapName = mapName;
        temp.MissionName = MissionName;


        foreach (string k in interactionMap.Keys)
        {
            temp.interactionMap.Add(k, interactionMap[k].Copy());
        }


        foreach (MapCoords coords in typeChangeDict.Keys.ToArray())
        {
            temp.typeChangeDict.Add(coords, typeChangeDict[coords]);
        }

    
        temp.mainReward = mainReward.Copy() as RewardTable;
        temp.sideRewards = sideRewards.Copy() as RewardTable;
        temp.secretRewards = secretRewards.Copy() as RewardTable;

        foreach (ObjectiveComponent oc in mainObjectives)
        {
            temp.mainObjectives.Add(oc.Copy() as ObjectiveComponent);
        }

        foreach (ObjectiveComponent oc in sideObjectives)
        {
            temp.sideObjectives.Add(oc.Copy() as ObjectiveComponent);
        }

        foreach (ObjectiveComponent oc in secretObjectives)
        {
            temp.secretObjectives.Add(oc.Copy() as ObjectiveComponent);
        }

        foreach (ObjectiveComponent oc in failstate)
        {
            temp.failstate.Add(oc.Copy() as ObjectiveComponent);
        }

        foreach (MapEnchantment mapEnchantment in MapEnchantments)
        {
            temp.MapEnchantments.Add(mapEnchantment.Copy() as MapEnchantment);
        }

        temp.currentTurn = currentTurn;


        foreach (Event eve in MissionEvents)
        {
            temp.MissionEvents.Add(eve.Copy() as Event);
        }

        foreach (Tuple<MapCoords, List<string>> item in initTileEffects)
        {
            temp.initTileEffects.Add(new Tuple<MapCoords, List<string>>(item.ele1, item.ele2));
        }


        return temp;

    }
#endregion





    public bool CheckIfComplete(BoardManager bm)
    {
        return CheckIfMainOjbectiveComplete(bm);
    }

    public bool CheckIfFailed(BoardManager bm)
    {
        return CheckIfObjectiveComplete(bm, failstate);
    }

    public bool CheckEvents()
    {
        int length = MissionEvents.Count - 1;
        bool eventFired = false;

        for (int i = length; i >= 0; i--)
        {
            if(MissionEvents[i].EventShouldFire())
            {
                if(MissionEvents[i] is CutsceneEvent)
                {
                    eventFired = true;
                }

                MissionEvents[i].FireEvent();
                MissionEvents.RemoveAt(i);

            }
        }

        return eventFired;

    }

    public string PrintObjectGoals()
    {
        string s = "";

        foreach (ObjectiveComponent objective in mainObjectives)
        {
            s += objective.PrintProgress() + "   " + objective.ObjectiveComplete(Globals.GetBoardManager()) + "\n";
        }

        if (sideObjectives.Count > 0)
        {
            s += "Side Objectives: \n";

            foreach (ObjectiveComponent objective in sideObjectives)
            {
                s += objective.PrintProgress() + "\t\t" + objective.ObjectiveComplete(Globals.GetBoardManager()) + "\n";
            }
        }


        return s;
    }


    #region Reward Distribution and Objective Completion
    public void MissionComplete(BoardManager bm)
    {
        if(CheckIfMainOjbectiveComplete(bm))
        {
            DistributeMainRewards(bm);
        }

        if (CheckIfSideOjbectiveComplete(bm))
        {
            DistributeSecondaryRewards(bm);
        }

        if (CheckIfSecretOjbectiveComplete(bm))
        {
            DistributeSecretRewards(bm);
        }
    }


    ///Generalize method for distribution of a reward table
    ///
    private void DistributeReward(BoardManager bm, RewardTable table)
    {
        if(table != null)
        {
            table.DistributeReward(bm);
        }
    }

    private void DistributeMainRewards(BoardManager bm)
    {
        DistributeReward(bm, mainReward);
    }

    private void DistributeSecondaryRewards(BoardManager bm)
    {
        DistributeReward(bm, sideRewards);
    }

    private void DistributeSecretRewards(BoardManager bm)
    {
        DistributeReward(bm, secretRewards);
    }

    ///The generalized method to check if objectives has been complete
    ///
    private bool CheckIfObjectiveComplete(BoardManager bm, List<ObjectiveComponent> objectives)
    {
        foreach (ObjectiveComponent oc in objectives)
        {
            if (oc.ObjectiveComplete(bm) == false)
            {
                return false;
            }
        }

        return true;
    }

    public bool CheckIfMainOjbectiveComplete(BoardManager bm)
    {
        return CheckIfObjectiveComplete(bm, mainObjectives);
    }

    public bool CheckIfSideOjbectiveComplete(BoardManager bm)
    {
        return CheckIfObjectiveComplete(bm, sideObjectives);
    }
    public bool CheckIfSecretOjbectiveComplete(BoardManager bm)
    {
        return CheckIfObjectiveComplete(bm, secretObjectives);
    }
    #endregion



    public void ScaleUnits(int levelupTimes)
    {
        for (int i = 0; i < levelupTimes; i++)
        {
            foreach (ActorData data in npcs)
            {
                data.LevelUp();
            }

        }
    }

    public Event GetEvent(string id)
    {
        foreach (Event e in MissionEvents)
        {
            if(e.GetKey() == id)
            {
                return e;
            }
        }

        return null;
    }

    public List<ObjectiveComponent> GetAllObjectives()
    {
        List<ObjectiveComponent> temp = new List<ObjectiveComponent>();

        //

        foreach (ObjectiveComponent objectiveComponent in mainObjectives)
        {
            temp.Add(objectiveComponent);
        }

        foreach (ObjectiveComponent objectiveComponent in sideObjectives)
        {
            temp.Add(objectiveComponent);
        }

        foreach (ObjectiveComponent objectiveComponent in secretObjectives)
        {
            temp.Add(objectiveComponent);
        }

        return temp;

    }

    #region Enchantment Handling
    private void InitEnchantment(MapEnchantment enchantment)
    {
        List<Actor> actors = Globals.GetBoardManager().spawner.actors;

        Globals.GetBoardManager().ui.backgroundContoller.NewColor(enchantment.bg_color_gradient_itd);

        foreach (Actor actor in actors)
        {
            enchantment.ApplyActorEffects(actor);
        }

        TileNode[,] tilenodes = Globals.GetBoardManager().pathfinding.tiles;

        foreach (TileNode node in tilenodes)
        {
            enchantment.ApplyTileEffect(node);

        }

    }

    public void RemoveEnchantment(MapEnchantment enchantment)
    {
        List<Actor> actors = Globals.GetBoardManager().spawner.actors;

        Globals.GetBoardManager().ui.backgroundContoller.NewColor(enchantment.bg_color_gradient_itd);

        foreach (Actor actor in actors)
        {
            enchantment.RemoveActorEffects(actor);
        }

        TileNode[,] tilenodes = Globals.GetBoardManager().pathfinding.tiles;

        foreach (TileNode node in tilenodes)
        {
            enchantment.RemoveTileEffect(node);

        }
    }


    public void AddEnchantment(string MapEnchantmentKey)
    {
        MapEnchantment enchantment = Globals.campaign.GetMapDataContainer().MapEnchantmentsDB.GetCopy(MapEnchantmentKey);

        AddEnchantment(enchantment);
    }


    public void AddEnchantment(MapEnchantment ench)
    {
        if(MapEnchantments.Count > 0) //our enchantment limit, we'll remove the oldest one, trigger all the end enchantment stuff
        {
            RemoveEnchantment(MapEnchantments[0]);
            MapEnchantments.RemoveAt(0);
        }

        MapEnchantments.Add(ench);
        InitEnchantment(ench);
    }
    #endregion
}

public enum MissionType
{
    Main,
    Side,
    Party,
    Reoccuring
}

