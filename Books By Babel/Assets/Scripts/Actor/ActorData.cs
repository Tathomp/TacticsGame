using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ActorData : DatabaseEntry
{
    public string Name { get; set; }

    public int Level { get; set; }
    public int XP { get; set; }
    public int ID { get; set; }

    public string race;
    public string description;


    public string primaryJob;
    public string secondaryJob; // this should be an empty string 

    public string movement;

    public List<string> actorPropertyTags;

    public bool selected;
    public string portraitFilePath;
    public string animationController;

    public int gridPosX, gridPosY;
    public int deathThreshold;
    public bool isAlive, isDying, blockAttack, blockMove;


    public ToolBarData toolbaar;
    public StatsContainer maxStatCollection;
    public StatsContainer currentStatCollection;

    public Equipment equipment;
    public Inventory inventory;

    public ActorController controller;
    public CooldownMap cooldownMap;
    public BuffContainer buffContainer;
    public Relationship Relationships;


    public Direction directionFacing;

    public JobDataState JobDataState;

    //public Flags ActorFlags { get; set; }

    public ActorData(string key) : base(key)
    {
        buffContainer = new BuffContainer();
        isAlive = true;
        equipment = new Equipment();
        Relationships = new Relationship();
        inventory = new Inventory();
        toolbaar = new ToolBarData();
        actorPropertyTags = new List<string>();
        // Relationships = new Relationship();

        blockAttack = false;
        blockMove = false;

        JobDataState = new JobDataState();
        cooldownMap = new CooldownMap();

        primaryJob = "";
        secondaryJob = "";
        description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus vitae nunc eget leo maximus scelerisque commodo ut ex. Nulla iaculis dui erat, a pharetra ipsum pharetra a. Vestibulum sed commodo nisl, non feugiat nunc.";
        deathThreshold = 0;

        currentStatCollection = new StatsContainer();
        maxStatCollection = new StatsContainer();

      
    }

    public void AddXp(int xpamt)
    {
        XP += xpamt;

        ExperienceModel mode = new ExperienceModel();

        // Experience model handles leveling up
        // the loop runs until the unit no longer has enough xp to level up
        //
        while (mode.LeveledUp(this))
        {
            LevelUp();
        }


    }

    public void LevelUp()
    {
        Level++;

        /// So we'll add the stat growths for the primary job now
        /// We could also add the secondary job stats
        /// Or just add half the primary, half the second job?
        ///
        StatsContainer sc = Globals.campaign.GetJobsData().JobDB.GetCopy(primaryJob).statGrowth;
        maxStatCollection.AddStats(sc);

    }

    public List<Job> ProcessJobUnlocks()
    {
        Race r = Globals.campaign.GetJobsData().raceDB.GetCopy(race);
        return r.GetUnlockedJobs(this);
    }

    public override DatabaseEntry Copy()
    {
        ActorData data = new ActorData(key);

        data.Name = Name;
        data.Level = Level;
        data.XP = XP;

        data.blockAttack = blockAttack;
        data.blockMove = blockMove;

        data.cooldownMap = cooldownMap.Copy();

        data.race = race;
        data.primaryJob = primaryJob;
        if (secondaryJob != null)
        {
            data.secondaryJob = secondaryJob;
        }
        data.selected = selected;
        //data.CanAttack = CanAttack;
        //data.CanMove = CanMove;

        data.maxStatCollection = (StatsContainer)maxStatCollection.Copy();
        data.currentStatCollection = (StatsContainer)currentStatCollection.Copy();

        data.equipment = (Equipment)equipment.Copy();
        data.inventory = (Inventory)inventory.Copy();

        data.controller = controller;

        if (Relationships != null)
            data.Relationships = Relationships.Copy();

        data.portraitFilePath = portraitFilePath;
        data.animationController = animationController;
        data.gridPosX = gridPosX;
        data.gridPosY = gridPosY;

        data.buffContainer = buffContainer.Copy();
        data.isAlive = isAlive;
        data.toolbaar = (ToolBarData)toolbaar.Copy();
        data.deathThreshold = deathThreshold;
        data.directionFacing = directionFacing;

        data.description = description;

        foreach (string t in actorPropertyTags)
        {
            data.actorPropertyTags.Add(t);
        }

        data.movement = movement;
        data.JobDataState = JobDataState.Copy();

        return data;
    }

    //Change the stat type by a give amount
    //returns true if the user died because of this change
    //
    public void ChangeStateType(StatTypes type, StatContainerType container, int stateChange)
    {
        if(container == StatContainerType.Both || container == StatContainerType.Current)
        {
            currentStatCollection.ChangeStat(type, stateChange);

        }

        if (container == StatContainerType.Both || container == StatContainerType.Max)
        {
            maxStatCollection.ChangeStat(type, stateChange);

        }

        //should put in a safety net for going over max
        // rework the Actor.ChangeHealth() method to just call this
        //


        if (currentStatCollection.GetValue(StatTypes.Health) <= deathThreshold && container == StatContainerType.Current)
        {

            if(Globals.currState == GameState.Combat)
            {
                Globals.GetBoardManager().spawner.GetActor(this).KillActor();
            }

        }

    }

    
    public MapCoords GetPosition()
    {
        return new MapCoords(gridPosX, gridPosY);
    }


    public void RestoreCurrentStats()
    {
        foreach (StatTypes statTypes in maxStatCollection.GetKeys())
        {
            currentStatCollection.statDict[statTypes] = maxStatCollection.statDict[statTypes];
        }
    }

    public void LearnTalent(string jobKey, string talent)
    {
        Talent t = Globals.campaign.contentLibrary.TalentDB.GetData(talent);

        LearnTalent(jobKey, t);
    }

    public void LearnTalent(string jobKey, Talent talent)
    {
        JobDataState.LearnSkill(jobKey, talent.GetKey());

        if (talent.HasPassive())
        {
            LearnPassiveBuff(talent.PassiveBuff);
        }

        if(talent.HasLearnAnotherSkill())
        {
            talent.jobeEffect.OnApply(this, null);
        }
    }

    public void LearnPassiveBuff(Buff passiveBuff)
    {
        buffContainer.LearnPassiveBuff(this, passiveBuff);
    }

    #region Species stuff
    public void ChangeSpecies(string newSpecies)
    {
        foreach (string rtags in Globals.campaign.GetJobsData().raceDB.GetCopy(race).raceTags)
        {
            int tagLength = actorPropertyTags.Count - 1;

            for (int i = tagLength; i >= 0; i--)
            {
                if(actorPropertyTags[i].Equals(rtags))
                {
                    actorPropertyTags.RemoveAt(i);
                }
            }

            actorPropertyTags.Remove(rtags);
        }

        race = newSpecies;

        foreach (string rtags in Globals.campaign.GetJobsData().raceDB.GetCopy(race).raceTags)
        {
            actorPropertyTags.Add(rtags);
        }
    }

    public void ChangeJobs(string newJob, SwitchJobsEffect.JobCategory jobtype)
    {
        string prevJob = "";

        if(jobtype == SwitchJobsEffect.JobCategory.Primary)
        {
            prevJob = primaryJob;
            primaryJob = newJob;


            foreach (string key in JobDataState.GetLearnedTalent(prevJob))
            {
                buffContainer.RemoveBuff(this, key);

            }

            foreach (string key in JobDataState.GetLearnedTalent(primaryJob))
            {
                buffContainer.LearnPassiveBuff(this, Globals.campaign.contentLibrary.buffDatabase.GetCopy(key));

            }
        }
        else
        {
            prevJob = secondaryJob;
            secondaryJob = newJob;

            if (prevJob != "")
            {
                foreach (string key in JobDataState.GetLearnedTalent(prevJob))
                {
                    buffContainer.RemoveBuff(this, key);

                }
            }

            if (secondaryJob != "")
            {
                foreach (string key in JobDataState.GetLearnedTalent(secondaryJob))
                {
                    buffContainer.LearnPassiveBuff(this, Globals.campaign.contentLibrary.buffDatabase.GetCopy(key));

                }
            }

        }

    }
    #endregion


    public bool HasSecondaryJob()
    {
        return secondaryJob != "";
    }

    public void ResetCharges()
    {
        inventory.ResetCharges();
    }
}
