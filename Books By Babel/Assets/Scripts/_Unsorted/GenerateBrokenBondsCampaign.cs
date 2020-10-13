using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBrokenBondsCampaign
{

    public Campaign campaign;
    private Party party;
    private ContentLibrary contentContainer;

    public GenerateBrokenBondsCampaign()
    {
        GenerateAllDefaults();

        campaign.contentLibrary = contentContainer;


        contentContainer.SaveFile();


        SaveStateBase sf = new SaveStateBase(campaign);

        SaveLoadManager.Savecampaign(campaign);
    }


    #region Defaults
    void GenerateAllDefaults()
    {
        Debug.Log("Demo generated");

        campaign = new Campaign("Ice Cave Fight", "bb");
        campaign.RespecModel = new RespectCostCredits(5);

        campaign.CampaignDescrtiption = "There's some vampires or something. Maybe the real vampires were the friends we made along the way.";
        campaign.thumbnailName = "vampire_port";

        Globals.campaign = campaign;

        contentContainer = new ContentLibrary("ice_cave_content");

        Properties();
        MovementTypes();
        Tiles();
        Buff();
        Skills();
        TalentNodes();
        Disciplines();
        Jobs();
        Races();
        Items();
        Actors();
        BuildRelationships();
        CutsceneDB();
        TileEffects();
        Map();
        Mission();
        Party();
        Shops();
        MapEnchantments();
        SkillPropertyMap();
        WorldMaps();
        Bars();

    }

    public void MovementTypes()
    {
        campaign.movementTypes = new List<string>()
        {
            "walking",
            "flying",
            "levitate"
        };

    }

    private void Properties()
    {
        campaign.properties = new List<string>()
        {
            "flammable",
            "on fire"
        };

    }

    private void Bars()
    {
        Bar bar = new Bar("test_bar", "The Rusty Spoon");


        bar.maxrandommisison = 2;
        bar.minrandommissions = 1;


        bar.randomspawnsPool.Add(new RandomSpawnData("testmission1", "neighbor_2"));
        bar.randomspawnsPool.Add(new RandomSpawnData("testmission2", "neighbor_3"));
        bar.randomspawnsPool.Add(new RandomSpawnData("testmission3", "neighbor_4"));



        campaign.GetcutScenedataContainer().barDatabase.AddEntry(bar);
    }

    private void BuildRelationships()
    {
        Relationship rel = new Relationship();
        rel.SetRelationship("abagail", 150);



        ActorData a = contentContainer.actorDB.GetCopy("winston");
        a.Relationships = rel;
        contentContainer.actorDB.UpdateEntry(a);

        rel = new Relationship();
        rel.SetRelationship("winston", 150);

        ActorData w = contentContainer.actorDB.GetCopy("abagail");
        w.Relationships = rel;
        contentContainer.actorDB.UpdateEntry(w);


        RelationshipMap m = new RelationshipMap();

        m.AddRelationship("winston", "abagail", RelationshipLevel.C, "choice_test");
        campaign.relationshipMap = m;
    }

    private void TalentNodes()
    {
        SavedDatabase<Talent> talentDb = new SavedDatabase<Talent>();

        Talent t = new Talent("attack_skill", "Attack Talent", contentContainer.skillDatabase.GetCopy("attack_skill"));
        talentDb.AddEntry(t);


        t = new Talent("aura_talent", "Aura Skill Talent Buff", contentContainer.skillDatabase.GetCopy("aura_skill"));
        talentDb.AddEntry(t);

        //t = new Talent("aura_talent", "Passive: Aura Skill Talent Buff", contentContainer.buffDatabase.GetCopy("aura_buff"));
        //talentDb.AddEntry(t);

        t = new Talent("heal", "Heal Talent", contentContainer.skillDatabase.GetCopy("heal"));
        talentDb.AddEntry(t);

        t = new Talent("summon_skeleton", "Summon Skeletons Talent", contentContainer.skillDatabase.GetCopy("summon_skeleton"));
        talentDb.AddEntry(t);

        t = new Talent("fire_ball", "Fireball Talent", contentContainer.skillDatabase.GetCopy("fire_ball"));
        talentDb.AddEntry(t);

        t = new Talent("temp_health_buff", "Buff Health Talent", contentContainer.skillDatabase.GetCopy("temp_health_buff"));
        talentDb.AddEntry(t);

        t = new Talent("fire_attack", "Fire Sword Talent", contentContainer.skillDatabase.GetCopy("fire_attack"));
        talentDb.AddEntry(t);

        t = new Talent("revive_unit", "Revive Talent", contentContainer.skillDatabase.GetCopy("revive_unit"));
        talentDb.AddEntry(t);

        t = new Talent("double_tap", "Double Tap Talent", contentContainer.skillDatabase.GetCopy("double_tap"));
        talentDb.AddEntry(t);

        t = new Talent("curse", "Curse Talent", contentContainer.skillDatabase.GetCopy("curse"));
        talentDb.AddEntry(t);

        t = new Talent("guardian_mech", "Guardian Mech Talent", contentContainer.skillDatabase.GetCopy("guardian_mech"));
        talentDb.AddEntry(t);

        t = new Talent("knife_mech", "Knife Mech Talent", contentContainer.skillDatabase.GetCopy("knife_mech"));
        talentDb.AddEntry(t);

        t = new Talent("buff_skill", "Increase Health Passive", contentContainer.buffDatabase.GetCopy("500_health_buff"));
        talentDb.AddEntry(t);

        t = new Talent("pale_eclipse", "Pale Eclipse", contentContainer.skillDatabase.GetCopy("pale_eclipse"));
        talentDb.AddEntry(t);

        t = new Talent("vampiric_bite", "Vampire Bite", contentContainer.skillDatabase.GetCopy("vampiric_bite"));
        talentDb.AddEntry(t);

        t = new Talent("lich", "Transform: Lich", contentContainer.buffDatabase.GetCopy("lich_buff"));
        talentDb.AddEntry(t);

        t = new Talent("skeltonize", "Skeletonize", contentContainer.skillDatabase.GetCopy("skeltonize"));
        talentDb.AddEntry(t);

        t = new Talent("turret", "Deploy Turret", contentContainer.skillDatabase.GetCopy("turret"));
        talentDb.AddEntry(t);

        t = new Talent("levitate_talent", "Levitation", contentContainer.skillDatabase.GetCopy("levitate_skill"));
        talentDb.AddEntry(t);

        t = new Talent("tempest_aura", "Tempest Aura", contentContainer.skillDatabase.GetCopy("tempest_aura"));
        talentDb.AddEntry(t);

        t = new Talent("lightning_bolt", "Lightning Bolt", contentContainer.skillDatabase.GetCopy("lightningbolt"));
        talentDb.AddEntry(t);

        t = new Talent("conjure_talent", "Conjure Weapon", contentContainer.skillDatabase.GetCopy("conjure"));
        talentDb.AddEntry(t);

        t = new Talent("inferno", "inferno", contentContainer.skillDatabase.GetCopy("inferno"));
        talentDb.AddEntry(t);

        t = new Talent("blizzard", "blizzard", contentContainer.skillDatabase.GetCopy("blizzard"));
        talentDb.AddEntry(t);

        t = new Talent("archon_buff_skill", "Archon", contentContainer.skillDatabase.GetCopy("archon_buff_skill"));
        talentDb.AddEntry(t);

        t = new Talent("archon_buff_off", "Archon off", contentContainer.skillDatabase.GetCopy("archon_buff_off"));
        talentDb.AddEntry(t);

        t = new Talent("guardian_stance", "Guard Ally", contentContainer.skillDatabase.GetCopy("guardian_stance"));
        talentDb.AddEntry(t);

        t = new Talent("shield_bash", "Shield Bash", contentContainer.skillDatabase.GetCopy("shield_bash"));
        talentDb.AddEntry(t);

        t = new Talent("smash", "Smash Wall", contentContainer.skillDatabase.GetCopy("smash"));
        talentDb.AddEntry(t);

        t = new Talent("brutal_kill", "Kick", contentContainer.skillDatabase.GetCopy("brutal_kill"));
        talentDb.AddEntry(t);

        t = new Talent("bear_trap", "Bear Trap", contentContainer.skillDatabase.GetCopy("bear_trap"));
        talentDb.AddEntry(t);

        t = new Talent("dtrait", "Desperate Times", contentContainer.buffDatabase.GetCopy("dtrait"));
        talentDb.AddEntry(t);

        t = new Talent("blood_maigc", "Blood Magic", contentContainer.buffDatabase.GetCopy("blood_maigc"));
        talentDb.AddEntry(t);

        t = new Talent("prepare_posion", "Prepare Posion", contentContainer.skillDatabase.GetCopy("prepare_posion"));
        talentDb.AddEntry(t);

        t = new Talent("bottle_posion", "Bottle Posion", contentContainer.skillDatabase.GetCopy("bottle_posion"));
        talentDb.AddEntry(t);

        t = new Talent("charge", "Charge!", contentContainer.skillDatabase.GetCopy("charge"));
        talentDb.AddEntry(t);

        t = new Talent("tempest_learn_aura", "Inferno (Archon Skill)");
        t.jobeEffect = new LearnSkillBuffEffect("Archon", "inferno");

        talentDb.AddEntry(t);

        t = new Talent("power_shot", "Power Shot", contentContainer.skillDatabase.GetCopy("future_fireball"));
        talentDb.AddEntry(t);

        t = new Talent("animate_wall", "Animate Rock", contentContainer.skillDatabase.GetCopy("animate_wall"));
        talentDb.AddEntry(t);

        t = new Talent("teleport", "Blink", contentContainer.skillDatabase.GetCopy("teleport"));
        talentDb.AddEntry(t);
        t = new Talent("flower", "Healing Flowers", contentContainer.skillDatabase.GetCopy("flower"));
        talentDb.AddEntry(t);

        t = new Talent("manabattery", "SUmmone Skeleton on Attack", contentContainer.buffDatabase.GetCopy("mana_battery"));
        talentDb.AddEntry(t);
        contentContainer.TalentDB = talentDb;

        

    }

    void Mission()
    {
        MissionHandler missionDB = new MissionHandler();

        //string targets = "base_enemy";

        MapCoords position = new MapCoords(1, 5);



        //AssassinateMission testKillMission = new AssassinateMission("test_mission_00", targets);
        Mission testKillMission = new Mission("test_mission_00");

        testKillMission.mapName = "testlevel";
        testKillMission.MissionName = "Raid the Vampire Whatever";
        testKillMission.maxUnitsAllowed = 4;
        //testKillMission.map = campaign.GetMapDataContainer().mapDB.GetCopy("testlevel") as MapDataModel;

        ActorData enemy = contentContainer.actorDB.GetCopy("base_enemy");
        enemy.gridPosX = 12;
        enemy.gridPosY = 14;
        testKillMission.npcs.Add(enemy);

        enemy = (ActorData)enemy.Copy();
        enemy.gridPosX = 10;
        enemy.gridPosY = 18;
        //testKillMission.npcs.Add(enemy);

        enemy = (ActorData)enemy.Copy();
        enemy.gridPosX = 15;
        enemy.gridPosY = 16;
        //testKillMission.npcs.Add(enemy);

        enemy = (ActorData)enemy.Copy();
        enemy.gridPosX = 10;
        enemy.gridPosY = 12;
        //testKillMission.npcs.Add(enemy);

      //  testKillMission.int_cutsceneKey = "test_cutscene";
       // testKillMission.end_cutscenekey = "choice_test";

        //testKillMission.objectives.Add(new SiegePointObjectComponent(position));
        //testKillMission.objectives.Add(new AssassinateObjectiveComponent(targets));
        //AddObjectiveEvent aoe = new AddObjectiveEvent("", new SiegePointObjectComponent(position));
        //aoe.flags.Add(new FlagMissionTurn("", 2));
        //testKillMission.MissionEvents.Add(aoe);


        testKillMission.mainObjectives.Add(new EliminateAllObjectiveComponent());
        testKillMission.int_cutsceneKey = "test_cutscene";



        //Reward table
        RewardTable rt = new RewardTable();
        List<string> items = new List<string>();
        items.Add(("skele_jar"));

        ItemReward ir = new ItemReward(items);
        CreditReward cr = new CreditReward(5000);
        ExperienceReward xp = new ExperienceReward(50);
        MissionReward mr = new MissionReward("training_map", "test_mission_00_complete");
        //CharacterReward ar = new CharacterReward("isabella");
        JPReward jpr = new JPReward(200);

        rt.AddReward(ir);
        rt.AddReward(jpr);
        rt.AddReward(cr);
        rt.AddReward(xp);
        rt.AddReward(mr);
        //rt.AddReward(ar);

        testKillMission.mainReward = rt;

        testKillMission.descript = "Kill the enemies";

        missionDB.AddMission(testKillMission);


        testKillMission = testKillMission.Copy() as Mission;
        testKillMission.ChangeKey("testmission1");
        missionDB.AddMission(testKillMission);

        testKillMission = testKillMission.Copy() as Mission;
        testKillMission.ChangeKey("testmission2");
        missionDB.AddMission(testKillMission);

        testKillMission = testKillMission.Copy() as Mission;
        testKillMission.ChangeKey("testmission3");
        missionDB.AddMission(testKillMission);

        /// Practice Dummy mission
        /// 
        Mission testMission = new Mission("training_map");
        testMission.mapName = "testlevel";
        testMission.MissionName = "Practice Dummy";
        testMission.maxUnitsAllowed = 4;
        testMission.mainReward = rt;

        enemy = contentContainer.actorDB.GetCopy("traingdummy");
        enemy.gridPosX = 5;
        enemy.gridPosY = 6;



        testMission.initTileEffects.Add(
            new Tuple<MapCoords, List<string>>(
                new MapCoords(7, 7),
                new List<string>() { "flame" }));

        testMission.npcs.Add(enemy);

        enemy = enemy.Copy() as ActorData;
        enemy.gridPosX -= 1;
        testMission.npcs.Add(enemy);
        enemy = enemy.Copy() as ActorData;
        enemy.gridPosX -= 1;
        testMission.npcs.Add(enemy);
        /*
        enemy = enemy.Copy() as ActorData;
        enemy.gridPosY -= 1;
        testMission.npcs.Add(enemy);

        enemy = enemy.Copy() as ActorData;
        enemy.gridPosY += 5;
        enemy.gridPosX += 5;
        testMission.npcs.Add(enemy);

        enemy = enemy.Copy() as ActorData;
        enemy.gridPosY = 10;
        enemy.gridPosX = 13;
        testMission.npcs.Add(enemy);

        enemy = enemy.Copy() as ActorData;
        enemy.gridPosY = 13;
        enemy.gridPosX = 10;
        testMission.npcs.Add(enemy);

        enemy = enemy.Copy() as ActorData;
        enemy.gridPosY = 13;
        enemy.gridPosX = 13;
        testMission.npcs.Add(enemy);


        enemy = enemy.Copy() as ActorData;
        enemy.gridPosY = 10;
        enemy.gridPosX = 10;
        testMission.npcs.Add(enemy);*/

        testMission.descript = "Kill the practice dummy";

        //testMission.mainObjectives.Add(new EliminateAllObjectiveComponent());
        testMission.mainObjectives.Add(new TurnNumberObjectiveComponent(5));
        //testMission.sideObjectives.Add(new SiegePointObjectComponent(1, 3));

        InteractionObjectiveComponent obj = new InteractionObjectiveComponent("test");
        //testMission.sideObjectives.Add(obj);

        testMission.sideRewards = new RewardTable();
        testMission.sideRewards.rewards.Add(new CreditReward(500));

        /////////////////
        ///// Event /////
        /////////////////
        FlagBool fb = new FlagBool("interaction_flag");
        Event e = new CutsceneEvent("test_event_1", "choice_test");
        e.flags.Add(fb);
        testMission.MissionEvents.Add(e);

        EventInteraction eint = new EventInteraction("test_event_1", "interaction_flag", "");
        testMission.interactionMap.Add("11", eint);

        ObjectiveInteraction objectiveInteraction = new ObjectiveInteraction("test", "");
        testMission.interactionMap.Add("12", objectiveInteraction);


        FlagMissionTurn turnTrigger = new FlagMissionTurn("", 3);
        /*
        e = new SummonUnitEvent("summon_event", "skeleton", new MapCoords(8, 8));
        e.flags.Add(turnTrigger);
        testMission.MissionEvents.Add(e);
        */

        turnTrigger = new FlagMissionTurn("", 2);
        e = new CutsceneEvent("test_event_2", "choice_test");
        e.flags.Add(turnTrigger);
        testMission.MissionEvents.Add(e);

        /// COMMENT OUT IF YOU WANT TO TEST THE EVENTS ON THE TRAINING DUMMY LEVEL
        /// 
       // testMission.MissionEvents = new List<Event>();

        //testMission.reward = (RewardTable)toolbox.rewardDatabase.GetEntry("test_reward");
        missionDB.AddMission(testMission);




        Mission flaggedMission = testKillMission.Copy() as Mission;
        flaggedMission.ChangeKey("choice_test_Mission");
        flaggedMission.MissionName = "Mission Flag Worked";
        FlagBool flaggedMissionTest = new FlagBool("flag_test_mission");

        missionDB.AddMission(flaggedMission);
        missionDB.AddFlag(flaggedMission.GetKey(), flaggedMissionTest.GetFlagID());
        Globals.campaign.GlobalFlags.Add(flaggedMissionTest.GetFlagID(), flaggedMissionTest);

        //missionDB.AddFlag(testMission.Getkey(), new FlagBool("test_mission_00_complete"));


        testMission = new Mission("test_threshold_mission");
        testMission.MissionName = "Kill the rebels";
        testMission.mapName = "testlevel";
        testMission.maxUnitsAllowed = 4;
        enemy = (ActorData)enemy.Copy();
        enemy = (ActorData)enemy.Copy();
        testMission.mainObjectives.Add(new EliminateAllObjectiveComponent());
        testMission.npcs.Add(enemy);
        testMission.descript = "Kill the bandit leader";


        missionDB.AddMission(testMission);

        FlagWithinLevelRange f = new FlagWithinLevelRange(testMission.GetKey() + "_level", 2, 5);
        missionDB.AddFlag(testMission.GetKey(), f.GetFlagID());


        testMission = (Mission)testMission.Copy();
        testMission.ChangeKey("flag_test");
        testMission.MissionName = "Flag Test Mission";
        testMission.mapName = "trainingmap";
        missionDB.AddMission(testMission);

        FlagHasItem item = new FlagHasItem(testMission.GetKey() + "_item", "skele_jar", 5);
        missionDB.AddFlag(testMission.GetKey(), item.GetFlagID());

        // Add the flags that missions will check 
        //
        campaign.GlobalFlags.Add(item.GetFlagID(), item);
        campaign.GlobalFlags.Add(f.GetFlagID(), f);



        campaign.SetMissionDatabase(missionDB);

        campaign.GetcutScenedataContainer().missionHandler.MissionsAccepted.Add("test_mission_00");
        campaign.GetcutScenedataContainer().missionHandler.MissionsAccepted.Add("training_map");

        //campaign.initalCombat = "test_mission_00";
    }


    void Actors()
    {
        SavedDatabase<ActorData> actorDatabase = new SavedDatabase<ActorData>();

        //core
        ActorData actordata = new ActorData("abagail");
        actordata.Name = "Abagail";
        actordata.Level = 1;
        actordata.movement = "walking";
        actordata.race = "human";
        actordata.actorPropertyTags = campaign.GetJobsData().raceDB.GetCopy(actordata.race).raceTags;
        actordata.primaryJob = "dev";
        actordata.portraitFilePath = "player_0";
        actordata.animationController = "player";
        actordata.controller = new PlayerController();

        //base jobs
        actordata.JobDataState.LearnSkill("generic_skills", ("attack_skill"));
        actordata.JobDataState.AddJobKeys(actordata.race);
        actordata.JobDataState.AddJobKeys(campaign.GetJobsData().raceDB.GetCopy(actordata.race).avaliablejobs);

        actordata.maxStatCollection = StatsContainer.AddSC(campaign.GetJobsData().raceDB.GetData(actordata.race).baseStats,
            campaign.GetJobsData().JobDB.GetData(actordata.primaryJob).baseStats);
        actordata.currentStatCollection = StatsContainer.AddSC(campaign.GetJobsData().raceDB.GetData(actordata.race).baseStats,
            campaign.GetJobsData().JobDB.GetData(actordata.primaryJob).baseStats);


        //items
        actordata.inventory.AddItem("skele_jar");
        actordata.inventory.AddItem("health_potion");
        actordata.equipment.EquipItem(campaign.GetItemCopy("synth_helm"), actordata);
        //actordata.equipment.EquipItem(campaign.GetItemCopy("unarmed"), actordata);
        actorDatabase.AddEntry(actordata);


        actordata = actordata.Copy() as ActorData;
        actordata.ChangeKey("Ambrose");
        actordata.Name = "Ambrose";
        actordata.animationController = "green0";
        actordata.portraitFilePath = "green0";
        actorDatabase.AddEntry(actordata);


        actordata = actordata.Copy() as ActorData;
        actordata.ChangeKey("Ryan C");
        actordata.Name = "Ryan C";
        actordata.animationController = "yellow1";
        actordata.portraitFilePath = "yellow1";
        actorDatabase.AddEntry(actordata);


        //Familar
        //////////
        actordata = new ActorData("winston");
        actordata.Name = "Winston";
        actordata.Level = 1;
        actordata.movement = "flying";
        actordata.primaryJob = "spectralsupport";
        actordata.race = "familiar";
        actordata.actorPropertyTags = campaign.GetJobsData().raceDB.GetCopy(actordata.race).raceTags;
        actordata.portraitFilePath = "owl_0";
        actordata.animationController = "owl";
        actordata.controller = new PlayerController();
        actordata.JobDataState.LearnSkill("generic_skills", ("attack_skill"));
        actordata.JobDataState.AddJobKeys(actordata.race);
        actordata.JobDataState.AddJobKeys(campaign.GetJobsData().raceDB.GetCopy(actordata.race).avaliablejobs);

        actordata.maxStatCollection = StatsContainer.AddSC(campaign.GetJobsData().raceDB.GetData(actordata.race).baseStats,
            campaign.GetJobsData().JobDB.GetData(actordata.primaryJob).baseStats);
        actordata.currentStatCollection = StatsContainer.AddSC(campaign.GetJobsData().raceDB.GetData(actordata.race).baseStats,
            campaign.GetJobsData().JobDB.GetData(actordata.primaryJob).baseStats);
        //actordata.equipment.EquipItem(campaign.GetItemCopy("unarmed"), actordata);

        actorDatabase.AddEntry(actordata);

        //Oscar
        //////////
        ///

        actordata = new ActorData("oscar");



        /////
        // Vampire
        //////
        ///
        actordata = new ActorData("base_enemy");
        actordata.Name = "Vampire Crypto-Shaman";
        actordata.Level = 1;
        actordata.movement = "walking";
        actordata.primaryJob = "mage";
        actordata.race = "vampire";
        actordata.controller = new AIController()
        {
            aigoals = new List<AIGoal>() {
                        new AIGoalActionAndMove(),
                        new AIGoalMoveTowardsPlayer() }
        };
        actordata.portraitFilePath = "enemy_0";
        actordata.animationController = "enemy";
        //actordata.equipment.EquipItem(campaign.GetItemCopy("unarmed"), actordata);
        actordata.JobDataState.LearnSkill("mage", "fire_ball");
        actordata.JobDataState.LearnSkill("mage", "archon_buff_skill");
        actordata.JobDataState.LearnSkill("mage", "summon_skeleton");
        actordata.JobDataState.LearnSkill("generic_skills", ("attack_skill"));

        actordata.actorPropertyTags = campaign.GetJobsData().raceDB.GetCopy("undead").raceTags;
        actordata.JobDataState.AddJobKeys("undead");
        actordata.JobDataState.LearnSkill("undead", "vampiric_bite");
        actordata.JobDataState.LearnSkill("undead", "temp_health_buff");

        actordata.maxStatCollection = StatsContainer.AddSC(campaign.GetJobsData().raceDB.GetData(actordata.race).baseStats,
    campaign.GetJobsData().JobDB.GetData(actordata.primaryJob).baseStats);
        actordata.currentStatCollection = StatsContainer.AddSC(campaign.GetJobsData().raceDB.GetData(actordata.race).baseStats,
            campaign.GetJobsData().JobDB.GetData(actordata.primaryJob).baseStats);

        actorDatabase.AddEntry(actordata);

        /// Animate Wall
        ///// 
        ///
        actordata = (ActorData)actordata.Copy();
        actordata.Name = "Stone Guardian";
        actordata.animationController = "golem";
        actordata.ChangeKey("golem");
        actordata.currentStatCollection.FillBase();
        actordata.maxStatCollection.FillBase();
        actorDatabase.AddEntry(actordata);
        actordata.maxStatCollection = StatsContainer.AddSC(campaign.GetJobsData().raceDB.GetData(actordata.race).baseStats,
            campaign.GetJobsData().JobDB.GetData(actordata.primaryJob).baseStats);
        actordata.currentStatCollection = StatsContainer.AddSC(campaign.GetJobsData().raceDB.GetData(actordata.race).baseStats,
            campaign.GetJobsData().JobDB.GetData(actordata.primaryJob).baseStats);

        ///Training Dummy
        //////
        ///
        actordata = actordata.Copy() as ActorData;
        actordata.Name = "Training Dummy";
        actordata.primaryJob = "dummy";
        actordata.ChangeKey("traingdummy");
        actordata.animationController = "dummy";
        actordata.portraitFilePath = "dummy_0";

        actordata.maxStatCollection.SetValue(StatTypes.MovementRange, 0);
        //actordata.currentStatCollection.SetValue(StatTypes.MovementRange, 0);

        actordata.maxStatCollection.SetValue(StatTypes.NumberOfMovements, 0);
        //actordata.currentStatCollection.SetValue(StatTypes.NumberOfMovements, 0);
        actorDatabase.AddEntry(actordata);

        ///Skeleton
        //////
        ///
        actordata = (ActorData)actordata.Copy();
        actordata.Name = "Spooky Skeleton";
        actordata.portraitFilePath = "skeleton_0";
        actordata.animationController = "skeleton";
        actordata.primaryJob = campaign.GetJobsData().raceDB.GetData("human").avaliablejobs[1];
        actordata.secondaryJob = "";
        actordata.ChangeKey("skeleton");
        actorDatabase.AddEntry(actordata);
        actordata.maxStatCollection = StatsContainer.AddSC(campaign.GetJobsData().raceDB.GetData(actordata.race).baseStats,
            campaign.GetJobsData().JobDB.GetData(actordata.primaryJob).baseStats);
        actordata.currentStatCollection = StatsContainer.AddSC(campaign.GetJobsData().raceDB.GetData(actordata.race).baseStats,
            campaign.GetJobsData().JobDB.GetData(actordata.primaryJob).baseStats);


        ActorData temp = actordata.Copy() as ActorData;
        temp.ChangeKey("asdfa");
        actorDatabase.AddEntry(temp);
                                      
        contentContainer.actorDB = actorDatabase;
    }


    void Items()
    {
        //item database
        SavedDatabase<Item> itemDatabase = new SavedDatabase<Item>();

        Item helm = new Item("synth_helm");
        helm.itemType = ItemType.Helm;
        EquippableItem equippableItem = new EquippableItem();
        helm.equippEffect = equippableItem;
        equippableItem.AddSlot(EquipmentSlot.Head);
        equippableItem.GetBonusStats().SetValue(StatTypes.Health, 12);
        helm.validJobs.Add("warden");
        helm.validJobs.Add("necromancer");
        helm.Name = "Synthetic Hood";
        helm.descript = "A hood wooven from a durable fabric.";
        itemDatabase.AddEntry(helm);

        helm = helm.Copy() as Item;
        helm.ChangeKey("body");
        helm.Name = "Light Robe";
        helm.descript = "A light-weight robe that offers minimal protection and maximum style";
        helm.equippEffect.validSlots = new List<EquipmentSlot>() { EquipmentSlot.BodyArmor };
        itemDatabase.AddEntry(helm);

        Item pistol = new Item("ion_rifle");
        pistol.Name = "Ballistic Rifle";
        pistol.itemType = ItemType.Gun;
        equippableItem = new WeaponItem(new SingleTarget(new List<string>()));
        pistol.equippEffect = equippableItem;
        equippableItem.AddSlot(EquipmentSlot.MainHand);
        equippableItem.GetBonusStats().SetValue(StatTypes.Strength, 10);
        equippableItem.GetBonusStats().SetValue(StatTypes.MaxRange, 8);
        equippableItem.GetBonusStats().SetValue(StatTypes.MinRange, 0);
        pistol.descript = "An economic rifle design for the modern rebel on the go.";
        itemDatabase.AddEntry(pistol);


        pistol = pistol.Copy() as Item;
        pistol.ChangeKey("carbon_key");
        pistol.equippEffect.GetBonusStats().SetValue(StatTypes.MaxRange, 1);
        itemDatabase.AddEntry(pistol);

        Item godWep = new Item("dev_rifle");
        godWep.Name = "Dev Rifle";
        godWep.itemType = ItemType.ShortSword;
        equippableItem = new EquippableItem();
        godWep.equippEffect = equippableItem;
        equippableItem.AddSlot(EquipmentSlot.MainHand);
        equippableItem.GetBonusStats().SetValue(StatTypes.MinRange, 1);
        equippableItem.GetBonusStats().SetValue(StatTypes.MaxRange, 8);
        equippableItem.GetBonusStats().SetValue(StatTypes.Strength, 99999);
        godWep.descript = "For testing purposes only";
        itemDatabase.AddEntry(godWep);

        Item shield = new Item("shield");
        shield.Name = "Cardboard Cutout";
        shield.itemType = ItemType.Shield;
        equippableItem = new EquippableItem();
        shield.equippEffect = equippableItem;
        equippableItem.AddSlot(EquipmentSlot.OffHand);
        equippableItem.GetBonusStats().SetValue(StatTypes.Defenese, 5);
        shield.descript = "The best defense a trash can provide";
        itemDatabase.AddEntry(shield);

        //I don't think we want to do it this way
        /*
        Item fist = new Item("unarmed");
        equippableItem = new EquippableItem();
        equippableItem.GetBonusStats().SetValue(StatTypes.MinRange, 0);
        equippableItem.GetBonusStats().SetValue(StatTypes.MaxRange, 2);
        equippableItem.GetBonusStats().SetValue(StatTypes.Strength, 10);
        equippableItem.AddSlot(EquipmentSlot.MainHand);
        fist.itemType = ItemType.Hand;
        fist.Name = "Unarmed";
        fist.descript = "A bare fist or sharp tooth does more damage to the enemy's ego than their body.";
        fist.equippEffect = equippableItem;
        fist.DisappearsInventory = true;
        itemDatabase.AddEntry(fist);
        */
        campaign.GetItemDataContainer().itemDB = itemDatabase;

        Item skelejar = new Item("skele_jar");
        Activateableitem consumeable = new Activateableitem(
            (Skill)contentContainer.skillDatabase.GetCopy("summon_skeleton"), "skele_jar");
        skelejar.activationEffect = consumeable;
        skelejar.maxStack = 3;
        skelejar.itemType = ItemType.Consumeable;
        skelejar.Name = "Capsule of Bones";
        skelejar.descript = "A small capsule with a skeleton inside.";
        campaign.GetAllItems().AddEntry(skelejar);


        Item healthpot = new Item("health_potion");
        Activateableitem ci = new Activateableitem(
            contentContainer.skillDatabase.GetCopy("heal"), "health_potion");
        healthpot.maxStack = 3;
        healthpot.ChargeItem = true;
        healthpot.Name = "Health Potion";
        healthpot.descript = "This shit is magic";
        healthpot.consumeableEffect = ci;
        campaign.GetAllItems().AddEntry(healthpot);


        Item poison = new Item("bottled_poison");
        Activateableitem ipoison = new Activateableitem( contentContainer.skillDatabase.GetCopy("toss_poison"), "bottled_poison");
        poison.maxStack = 1;
        poison.Name = "Poison flask";
        poison.descript = "Take once before every meal";
        poison.consumeableEffect = ipoison;
        poison.cost = 0;
        campaign.GetAllItems().AddEntry(poison);

    }


    void Party()
    {
        Party p = new Party();


        p.partyCharacter.Add(contentContainer.actorDB.GetCopy("abagail"));
        p.partyCharacter.Add(contentContainer.actorDB.GetCopy("winston"));
        p.partyCharacter.Add(contentContainer.actorDB.GetCopy("Ambrose"));
        p.partyCharacter.Add(contentContainer.actorDB.GetCopy("Ryan C"));

        //p.partyCharacter[0].maxStatCollection.SetValue(StatTypes.Health, 125);

        bool test = true;

        if (test)
        {
            foreach (ActorData temp in p.partyCharacter)
            {
                foreach (string j in Globals.campaign.GetJobsData().raceDB.GetCopy(temp.race).avaliablejobs)
                {
                    temp.JobDataState.AddJobPoints(j, 999);


                    foreach (Discipline disc in campaign.GetJobsData().JobDB.GetCopy(j).avalibleDisciples)
                    {
                        foreach (string skill in disc.TalenPool)
                        {
                            temp.JobDataState.LearnSkill(j, skill);
                        }
                    }
                }
            }
        }
        else
        {



            p.partyCharacter[0].ChangeJobs("mage", SwitchJobsEffect.JobCategory.Primary);
            p.partyCharacter[0].ChangeJobs("", SwitchJobsEffect.JobCategory.Secondary);

            p.partyCharacter[0].JobDataState.LearnSkill("plague", "bottle_posion");
            p.partyCharacter[0].JobDataState.LearnSkill("plague", "prepare_posion");


            p.partyCharacter[0].JobDataState.LearnSkill("mage", "fire_ball");
            p.partyCharacter[0].JobDataState.LearnSkill("mage", "archon_buff_skill");
            p.partyCharacter[1].JobDataState.LearnSkill("spectralsupport", "aura_talent");

            p.partyCharacter[2].ChangeJobs("herb", SwitchJobsEffect.JobCategory.Primary);
            p.partyCharacter[2].ChangeJobs("plague", SwitchJobsEffect.JobCategory.Secondary);
            p.partyCharacter[2].JobDataState.LearnSkill("plague", "bottle_posion");
            p.partyCharacter[2].JobDataState.LearnSkill("plague", "prepare_posion");
            p.partyCharacter[2].JobDataState.LearnSkill("herb", "heal");





            p.partyCharacter[3].ChangeJobs("warden", SwitchJobsEffect.JobCategory.Primary);
            p.partyCharacter[3].ChangeJobs("", SwitchJobsEffect.JobCategory.Secondary);
            p.partyCharacter[3].JobDataState.LearnSkill("warden", "shield_bash");
            p.partyCharacter[3].JobDataState.LearnSkill("warden", "guardian_stance");




        }




        p.partyCharacter[0].equipment.EquipItem(campaign.GetItemCopy("dev_rifle"), p.partyCharacter[0]);
        p.partyCharacter[0].equipment.EquipItem(campaign.GetItemCopy("body"), p.partyCharacter[0]);
        p.partyCharacter[0].equipment.EquipItem(campaign.GetItemCopy("synth_helm"), p.partyCharacter[0]);
        p.partyCharacter[0].equipment.EquipItem(campaign.GetItemCopy("carbon_key"), p.partyCharacter[0]);


        p.partyCharacter[3].equipment.EquipItem(campaign.GetItemCopy("carbon_key"), p.partyCharacter[3]);
        p.partyCharacter[3].equipment.EquipItem(campaign.GetItemCopy("shield"), p.partyCharacter[3]);
        p.partyCharacter[2].equipment.EquipItem(campaign.GetItemCopy("ion_rifle"), p.partyCharacter[2]);
        p.partyCharacter[0].equipment.EquipItem(campaign.GetItemData("dev_rifle"), p.partyCharacter[0]);




        campaign.currentparty = p;


        p.partyInvenotry.AddItem(("dev_rifle"));
        p.partyInvenotry.AddItem(("ion_rifle"));
        p.partyInvenotry.AddItem(("synth_helm"));


    }


    void Map()
    {
        Tiles();

        int sizeX = 20;
        int sizeY = 20;

        MapDataModel testmap = new MapDataModel("testlevel", sizeX, sizeY);
        campaign.GetMapDataContainer().mapDB.AddEntry(testmap);
        testmap.mapName = "Test Map";

        MapDataModel testmap2 = new MapDataModel("trainingmap", sizeX, sizeY);
        campaign.GetMapDataContainer().mapDB.AddEntry(testmap2);
        testmap2.mapName = "Training Map";

        MapDataModel basemap = new MapDataModel("baselevel", sizeX, sizeY);
        campaign.GetMapDataContainer().mapDB.AddEntry(basemap);
        basemap.mapName = "Base Map";

        MapDataModel map = new MapDataModel("inn", 6, 7);
        campaign.GetMapDataContainer().mapDB.AddEntry(map);
        map.mapName = "Inn";

        string[] tiledata = campaign.GetTileData().Tiles.DbKeys();

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                testmap.tileBoard[x, y] = "grass";
                testmap2.tileBoard[x, y] = "grass";
                basemap.tileBoard[x, y] = "grass";
            }
        }


        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                map.tileBoard[x, y] = "floor";

                if ((x == 1 || x == 5) && (y != 5 || y != 0)
                    
                    )
                {
                    map.tileBoard[x, y] = "wall";
                }

            }

        }

        testmap.tileBoard[4, 5] = "wall";
        testmap.tileBoard[5, 5] = "wall";
        testmap.tileBoard[5, 4] = "wall";

        
        testmap.tileBoard[11, 15] = "wall";
        testmap.tileBoard[12, 13] = "wall";
        testmap.tileBoard[13, 15] = "wall";
        testmap.tileBoard[13, 13] = "wall";
        testmap.tileBoard[11, 13] = "wall";
        testmap.tileBoard[12, 13] = "wall";
        
        testmap.tileBoard[13, 14] = "wall";
        testmap.tileBoard[11, 14] = "wall";
        testmap.tileBoard[11, 14] = "wall";
        
        testmap.tileBoard[5, 2] = "wall";
        testmap.tileBoard[5, 1] = "wall";
        testmap.tileBoard[5, 0] = "wall";

        testmap.tileBoard[6, 2] = "wall";
        testmap.tileBoard[6, 1] = "wall";
        testmap.tileBoard[6, 0] = "wall";

        testmap.tileBoard[7, 2] = "wall";
        testmap.tileBoard[7, 1] = "wall";
        testmap.tileBoard[7, 0] = "wall";


        testmap2.tileBoard[5, 5] = tiledata[2];
        basemap.tileBoard[5, 5] = tiledata[2];

        testmap.playerSpawnLocations.Add(new MapCoords { X = 3, Y = 1 });
        testmap.playerSpawnLocations.Add(new MapCoords { X = 1, Y = 1 });
        testmap.playerSpawnLocations.Add(new MapCoords { X = 2, Y = 2 });
        testmap.playerSpawnLocations.Add(new MapCoords { X = 1, Y = 3 });

        testmap2.playerSpawnLocations.Add(new MapCoords { X = 3, Y = 1 });
        testmap2.playerSpawnLocations.Add(new MapCoords { X = 1, Y = 1 });
        testmap2.playerSpawnLocations.Add(new MapCoords { X = 2, Y = 2 });
        testmap2.playerSpawnLocations.Add(new MapCoords { X = 3, Y = 3 });

        basemap.playerSpawnLocations.Add(new MapCoords { X = 3, Y = 1 });
        basemap.playerSpawnLocations.Add(new MapCoords { X = 1, Y = 1 });
        basemap.playerSpawnLocations.Add(new MapCoords { X = 2, Y = 2 });
        basemap.playerSpawnLocations.Add(new MapCoords { X = 3, Y = 3 });

    }


    void Tiles()
    {
        SavedDatabase<TileTypes> db = new SavedDatabase<TileTypes>();

        TileTypes grass = new TileTypes("grass")
        {
            TileName = "grass",
            spriteFilePath = "grass",

        };
        grass.MovementTypeCostMap.Add("walking", 1);
        grass.MovementTypeCostMap.Add("flying", 1);
        grass.attributes.Add("flammable");
        db.AddEntry(grass);
        StatsContainer s = new StatsContainer();
        s.SetValue(StatTypes.Health, 5);
        grass.tileBonuses = s;



        TileTypes tree = new TileTypes("tree")
        {
            TileName = "tree",
            spriteFilePath = "tree",
        };
        StatsContainer sc = new StatsContainer();
        tree.tileBonuses = sc;
        tree.MovementTypeCostMap.Add("walking", 2);
        tree.MovementTypeCostMap.Add("flying", 1);
        tree.tileBonuses.SetValue(StatTypes.Defenese, 5);
        tree.attributes.Add("flammable");
        db.AddEntry(tree);



        TileTypes road = new TileTypes("road")
        {
            TileName = "road",
            spriteFilePath = "road",

        };
        road.attributes.Add("flammable");
        road.MovementTypeCostMap.Add("walking", 1);
        road.MovementTypeCostMap.Add("flying", 1);
        StatsContainer tsc = new StatsContainer();
        road.tileBonuses.SetValue(StatTypes.SpeedRating, 2);
        road.tileBonuses = tsc;
        db.AddEntry(road);




        TileTypes wall = new TileTypes("wall")
        {
            TileName = "wall",
            spriteFilePath = "wall",
        };
        wall.MovementTypeCostMap.Add("flying", 1);
        wall.attributes.Add("stone");
        db.AddEntry(wall);


        TileTypes dirtType = new TileTypes("dirt");
        dirtType.TileName = "Dirt";
        dirtType.spriteFilePath = "dirt";
        dirtType.MovementTypeCostMap.Add("walking", 1);
        dirtType.MovementTypeCostMap.Add("flying", 1);
        db.AddEntry(dirtType);

        TileTypes water = new TileTypes("water");
        water.TileName = "Water";
        water.spriteFilePath = "water";
        water.MovementTypeCostMap.Add("walking", 3);
        water.MovementTypeCostMap.Add("flying", 1);
        db.AddEntry(water);


        TileTypes ice = new TileTypes("ice");
        ice.TileName = "Ice";
        ice.spriteFilePath = "ice";
        ice.MovementTypeCostMap.Add("walking", 3);
        ice.MovementTypeCostMap.Add("flying", 1);
        ice.attributes.Add("frozen");
        db.AddEntry(ice);

        campaign.GetTileData().Tiles = db;
    }


    void Skills()
    {
        SavedDatabase<Skill> skillDB = new SavedDatabase<Skill>();

        List<DamageObject> dmgobj = new List<DamageObject>();
        dmgobj.Add(new DamageObject(StatTypes.Strength, StatTypes.Defenese, 1f, 1f, 20));

        Skill attack = new Skill("attack_skill", 0, 0, "unkown", true);
        attack.targetType = new SingleTarget(new List<string>() { "wall" }, false);
        //attack.skillCost.Add(new SkillCostStat(StatTypes.Health, 50, true));
        attack.effects.Add(new ChangeHealthEffect(dmgobj, "attack_skill"));
        attack.skillName = "Attack";
        attack.animControllerID.Add("laser");
        attack.tags.Add("weapon");
        attack.descript = "A simple move that uses whatever weapon is equipped or the user's bare hands";

        //attack.skillCost.Add(new SkillCostItemInventory("skele_jar", true));
        //attack.skillCost.Add(new SkillCostItemEquipped("dev_rifle", true));
        //attack.skillCost.Add(new SkillCostBuff("levitate", true));

        //attack.targetType = new ConeTarget(new List<string>() { "wall" }, false,  2);
        //attack.tags.Add("fire");
        //attack.effects.Add(new ChangeTargetPositionSkillEffect(9, false));
        //attack.effects.Add(new ChangeTileTypeEffect("wall"));
        //attack.skillFilter = new FactionTargeting(FilterType.Occupied);
        attack.cooldown = 0;
        skillDB.AddEntry(attack);

        Skill charge = new Skill("charge", 0, 4);
        charge.targetType = new LineTarget(new List<string>(), true);
        charge.effects.Add(new TeleportUnitSkillEffect());
        charge.skillName = "Charge";
        charge.nextSkill = new NextSkillNode("attack_skill", false);
        charge.descript = "Charge across sometiles and attack a nearby enemy";
        skillDB.AddEntry(charge);

        Skill levitate = new Skill("levitate_skill", 0, 1);
        levitate.targetType = new SingleTarget(new List<string>(), false);
        levitate.skillName = "Levitate";
        levitate.effects.Add(new ApplyBuffEffect("levitate"));
        skillDB.AddEntry(levitate);

        Skill conjurWep = new Skill("conjure", 0, 1);
        conjurWep.targetType = new SingleTarget(new List<string>(), false);
        conjurWep.skillName = "Conjure Weapon";
        conjurWep.effects.Add(new EquipItemSkillEffect("ion_rifle", true));

        skillDB.AddEntry(conjurWep);

        Skill heal = new Skill("heal", 0, 5);
        heal.targetType = new SingleTarget(new List<string>() { "wall" }, true);
        heal.tags.Add("holy");

        heal.skillName = "Heal";
        heal.descript = "Basic healing spell";
        heal.effects.Add(new ChangeHealthEffect(dmgobj, "heal", true));
        //heal.effects.Add(new RemoveBuffSkillEffect(true, "prepare_posion", "", true, RemoveBuffCombatNode.RemoveType.RemoveBoth));
        //heal.effects.Add(new StealCreditsSkillEffect());
        skillDB.AddEntry(heal);

        Skill SummonSkele = new Skill("summon_skeleton", 0, 2);
        SummonSkele.targetType = new SingleTarget(new List<string>() { "wall" }, true);
        //SummonSkele.targetType = new AoeTarget(new List<string>() { "wall" }, false, 5);

        SummonSkele.skillName = "Summon Skeleton";
        SummonSkele.descript = "Summon a super spooky skeleton to do your bidding";
        SummonSkele.effects.Add(new SummonEffect("skeleton"));


        skillDB.AddEntry(SummonSkele);

        SummonSkele = SummonSkele.Copy() as Skill;
        RandomTargeting rt = new RandomTargeting(new List<string>(), false);
        rt.min = 1;
        rt.maxTargets = 1;
        rt.baseTargeting = new AoeTarget(new List<string>(), false, 2);
        SummonSkele.targetType = rt;
        SummonSkele.ChangeKey("random_skeleton");
        skillDB.AddEntry(SummonSkele);


        Skill fireBall = new Skill("fire_ball", 0, 7);
        fireBall.animControllerID.Add("fire");
        //fireBall.targetType = new ConeTarget(new List<string>() { "wall" }, true, 2);
        /*RandomTargeting rt = new RandomTargeting(new List<string>() { "wall" }, false);
        rt.min = 2;
        rt.maxTargets = 5;
        rt.baseTargeting = new ConeTarget(new List<string>() { "wall" }, false, 5);
        fireBall.targetType = rt;*/

        //fireBall.targetType = new BlockTarget(new List<string>(), false, 5, 3);
        fireBall.targetType = new AoeTarget(new List<string>(), false, 3);
        fireBall.skillName = "Fireball";
        fireBall.skillCost.Add(new SkillCostStat(StatTypes.Mana, 10, true));

        fireBall.descript = "Throw a fireball that deals damage in an AoE and has a chance to set flammable tiles on fire";
        fireBall.tags.Add("fire");
        fireBall.effects.Add(new ChangeHealthEffect(dmgobj, "fire_ball"));
        //fireBall.effects.Add(new TeleportUnitSkillEffect());
        //fireBall.nextSkill = new NextSkillNode("teleport", false);
        //fireBall.effects.Add(new SummonEffect("skeleton"));
        //fireBall.skillFilter = new FactionTargeting(true);
        skillDB.AddEntry(fireBall);

        Skill teleport = new Skill("teleport", 0, 2);
        teleport.targetType = new SingleTarget(new List<string>() { "wall" }, true);
        teleport.skillFilter = new FactionTargeting(FilterType.Enemy, true);
        teleport.skillName = "Teleport";
        teleport.effects.Add(new TeleportUnitSkillEffect());
        teleport.descript = "A short range teleport.";
        skillDB.AddEntry(teleport);

        Skill bufftest = new Skill("temp_health_buff", 0, 1);
        bufftest.targetType = new SingleTarget(new List<string>() { "wall" }, true);
        bufftest.skillName = "Buff Health";
        bufftest.descript = "Buff health by 50";
        ApplyBuffEffect abe = new ApplyBuffEffect("temp_health_buff");

        bufftest.effects.Add(abe);
        skillDB.AddEntry(bufftest);

        Skill auraSkill = new Skill("aura_skill", 0, 0);
        auraSkill.targetType = new SingleTarget(new List<string>(), false);
        auraSkill.effects.Add(new ApplyBuffEffect("aura_buff"));
        auraSkill.skillName = "Burning Weapon Aura";
        auraSkill.descript = "Apply the burning weapon buff to all units in range.";
        skillDB.AddEntry(auraSkill);



        Skill turnLich = new Skill("skeltonize", 0, 1);
        turnLich.targetType = new ConeTarget(new List<string>() { "wall" }, true, 6);
        turnLich.skillName = "Skeletonize";
        turnLich.descript = "Turn target into a spooky bone boy";
        abe = new ApplyBuffEffect("lich_buff");
        turnLich.effects.Add(abe);


        skillDB.AddEntry(turnLich);

        Skill fireattack = new Skill("fire_attack", 0, 1, "flame_sword");
        fireattack.targetType = new SingleTarget(new List<string>() { "wall" }, true);

        fireattack.skillName = "Flaming Sword";
        fireattack.descript = "Set weapons ablaze, granting a chance to set attacked tiles on fire";
        ApplyBuffEffect se = new ApplyBuffEffect("fire_attack_buff");
        fireattack.nextSkill = new NextSkillNode("attack_skill", true);
        fireattack.effects.Add(se);

        skillDB.AddEntry(fireattack);

        Skill revive = new Skill("revive_unit", 0, 8);
        revive.targetType = new SingleTarget(new List<string>() { "wall" }, true);
        revive.effects.Add(new ReviveActorEffect());
        revive.descript = "Bring a lost unit back to life";
        revive.skillName = "Rise from the Grave";

        skillDB.AddEntry(revive);

        Skill doubleTap = new Skill("double_tap", 0, 0, "unkown", true);
        doubleTap.animControllerID.Add("laser");
        doubleTap.animControllerID.Add("laser");
        doubleTap.UseWepon = true;
        doubleTap.targetType = new SingleTarget(new List<string>() { "wall" }, true);
        UseSkillEffect effect = new UseSkillEffect();
        effect.skillKey = "attack_skill";
        doubleTap.effects.Add(effect);
        doubleTap.effects.Add((UseSkillEffect)effect.Copy());
        doubleTap.skillName = "Double Tap";
        doubleTap.descript = "Shoot someone twice";

        skillDB.AddEntry(doubleTap);

        Skill curse = new Skill("curse", 0, 4);
        curse.targetType = new ConeTarget(new List<string>() { "wall" }, true, 5);
        ApplyBuffEffect be = new ApplyBuffEffect("curse");
        curse.effects.Add(be);
        curse.skillName = "Curse";
        curse.descript = "Curse ppl";

        skillDB.AddEntry(curse);

        Skill guardianMech = new Skill("guardian_mech", 0, 0);
        guardianMech.targetType = new SingleTarget(new List<string>() { "wall" }, true);
        ApplyBuffEffect e = new ApplyBuffEffect("guardian");
        guardianMech.effects.Add(e);
        guardianMech.skillName = "Mech: Guardian";
        guardianMech.descript = "Pilot a guardian mech";

        skillDB.AddEntry(guardianMech);


        Skill knifemech = new Skill("knife_mech", 0, 0);
        knifemech.targetType = new SingleTarget(new List<string>() { "wall" }, true);
        e = new ApplyBuffEffect("knife");
        knifemech.effects.Add(e);
        knifemech.skillName = "Mech: Knife";
        knifemech.descript = "Pilot a guardian mech";

        skillDB.AddEntry(knifemech);


        Skill paleeclipse = new Skill("pale_eclipse", 0, 0);
        paleeclipse.targetType = new SingleTarget(new List<string>() { "wall" }, true);
        MapEnchantmentSkillEffect m = new MapEnchantmentSkillEffect("pale_eclipse");
        paleeclipse.effects.Add(m);
        paleeclipse.skillName = "Pale Eclipse";
        paleeclipse.descript = "Call Upon the Pale Eclipse";

        skillDB.AddEntry(paleeclipse);


        Skill vampireTest = new Skill("vampiric_bite", 0, 1);
        vampireTest.targetType = new SingleTarget(new List<string>() { "wall" }, true);
        DrainHealth drain = new DrainHealth(dmgobj, "vampiric_bite");
        vampireTest.effects.Add(drain);
        vampireTest.skillName = "Vampiric Bite";
        vampireTest.descript = "Bite and suck the life out of ppl";

        skillDB.AddEntry(vampireTest);

        Skill summonTurrent = new Skill("turret", 0, 4);
        summonTurrent.targetType = new SingleTarget(new List<string>() { "wall" }, true);
        LinkedSummonSkillEffect link = new LinkedSummonSkillEffect("skeleton", "test_link");
        summonTurrent.effects.Add(link);
        summonTurrent.skillName = "Deploy Turret";
        summonTurrent.descript = "Deploy a turrent that can fire at medium range.";
        skillDB.AddEntry(summonTurrent);

        /// Mage and Archon Skills
        ///
        ///
        Skill lightningbol = new Skill("lightningbolt", 0, 5);
        lightningbol.targetType = new LineTarget(new List<string>() { "wall" }, false);
        ChangeHealthEffect eff = new ChangeHealthEffect(dmgobj, "lightningbolt");
        lightningbol.effects.Add(eff);
        lightningbol.tags.Add("electric");
        lightningbol.skillName = "Lightning Bolt";
        skillDB.AddEntry(lightningbol);

        Skill ice = new Skill("ice_cicle", 0, 5);
        ice.targetType = new LineTarget(new List<string>() {}, false);
        ChangeHealthEffect ice_eff = new ChangeHealthEffect(dmgobj, "ice_cicle");
        ice.effects.Add(ice_eff);
        ice.skillName = "Icicle";
        ice.descript = "Fire an ice spear along a path";
        ice.tags.Add("ice");
        skillDB.AddEntry(ice);

        Skill archon = new Skill("archon_buff_skill", 0, 0);
        archon.targetType = new SingleTarget(new List<string>() { "wall" }, true);
        ApplyBuffEffect ae = new ApplyBuffEffect("archon_buff");
        archon.effects.Add(ae);
        archon.skillName = "Transform: Archon";
        archon.descript = "Become mana incarnate";
        skillDB.AddEntry(archon);

        Skill blizzard = new Skill("blizzard", 1, 2);
        blizzard.targetType = new SingleTarget(new List<string>(), true);
        MapEnchantmentSkillEffect b = new MapEnchantmentSkillEffect("blizzard");
        blizzard.effects.Add(b);
        blizzard.skillName = "Summon Blizzard";
        blizzard.descript = "Call upon the colds winds to chill everybody out";
        skillDB.AddEntry(blizzard);

        Skill tempst = new Skill("tempest_aura", 0, 0);
        tempst.targetType = new SingleTarget(new List<string>(), true);
        ApplyBuffEffect tbe = new ApplyBuffEffect("tempest_buff");
        tempst.skillCost.Add(new SkillCostStat(StatTypes.Mana, 10, true));
        tempst.effects.Add(tbe);
        tempst.skillName = "Roaring Tempest";
        tempst.descript = "Surround yourself in a storm";
        skillDB.AddEntry(tempst);

        Skill inferno = new Skill("inferno", 0, 6);
        inferno.targetType = new AoeTarget(new List<string>(), false, 5);
        inferno.tags.Add("fire");
        inferno.skillCost.Add(new SkillCostStat(StatTypes.Mana, 10, true));

        ChangeHealthEffect iche = new ChangeHealthEffect(dmgobj, "inferno");
        ApplyBuffEffect bef = new ApplyBuffEffect("burned");
        inferno.effects.Add(iche);
        inferno.effects.Add(bef);
        inferno.skillName = "Blazing Inferno";
        inferno.descript = "Burn everything";
        skillDB.AddEntry(inferno);


        Skill turn_off = new Skill("archon_buff_off", 0, 0);
        turn_off.targetType = new SingleTarget(new List<string>());
        SkillCostBuff skillCostBuff = new SkillCostBuff("archon_buff", true);
        turn_off.skillCost.Add(skillCostBuff);
        turn_off.skillName = "Resume Form";
        skillDB.AddEntry(turn_off);

        Skill gskill = new Skill("guardian_stance", 0, 5);
        gskill.targetType = new SingleTarget(new List<string>());
        gskill.effects.Add(new ApplyLinkedBuffEffect("guardian_stance", "guarded"));
        gskill.skillName = "Guard Ally";
        gskill.descript = "Move to block any attack targeting a nerby ally";
        skillDB.AddEntry(gskill);

        gskill = new Skill("shield_bash", 0, 1);
        gskill.targetType = new SingleTarget(new List<string>());
        gskill.effects.Add(new ApplyBuffEffect("stun"));
        gskill.skillName = "Shield Bash";
        gskill.descript = "Smash a unit with your shield, stunning them briefly.";
        //gskill.skillCost.Add(new SkillCostItemEquipped(ItemType.Shield, false));
        skillDB.AddEntry(gskill);


        gskill = new Skill("smash", 0, 1);
        gskill.targetType = new ConeTarget(new List<string>(), false, 2);
        gskill.effects.Add(new ChangeTileTypeEffect("dirt"));
        gskill.skillName = "Smash Wall";
        gskill.descript = "Destroy a wall, reducing it to dirt";
        skillDB.AddEntry(gskill);


        gskill = new Skill("brutal_kill", 0, 5);
        gskill.targetType = new SingleTarget(new List<string>() { "wall"}, true);
        gskill.effects.Add(new ChangeTargetPositionSkillEffect(2, true));
        gskill.skillName = "Brutal kick";
        gskill.descript = "Knock back target 2 spaces";
        skillDB.AddEntry(gskill);


        gskill = new Skill("bear_trap", 1, 5);
        gskill.targetType = new SingleTarget(new List<string>() { "wall" }, true);
        gskill.effects.Add(new ApplyTileEffectSkillEffect("bear_trap"));
        gskill.skillName = "Deploy Bear Trap";
        gskill.descript = "Set a bear trap on a tile, dealing damage to whoever enters the tile";
        skillDB.AddEntry(gskill);

        gskill = new Skill("flower", 1, 5);
        gskill.targetType = new AoeTarget(new List<string>() { "wall" }, true, 2);
        gskill.effects.Add(new ApplyTileEffectSkillEffect("flower"));
        gskill.skillName = "Tactical healing Foliage";
        gskill.descript = "Lay a healing effect over a given area";
        skillDB.AddEntry(gskill);


        gskill = new Skill("power_shot", 0, 5);
        gskill.skillCost = new List<SkillCost>() { new SkillCostStat(StatTypes.NumberOfMovements, 1, true) };
        gskill.skillName = "Power Shot";
        gskill.targetType = new AoeTarget(new List<string>(), false, 2);
        gskill.conditionalsRequired.Add(new MatchingTagConditional("road", MatchingTagConditional.MatchingType.Tile));
        gskill.effects.Add(new ChangeHealthEffect(dmgobj, "power_shot"));
        gskill.animControllerID.Add("laser");
        gskill.UseWepon = true;
        gskill.descript = "A powerful the shot that prevents movement for the rest of the turn.";
        /* use attack skilll effect here */
        skillDB.AddEntry(gskill);


        gskill = new Skill("prepare_posion", 0, 1);
        gskill.skillName = "Prepare Posion";
        gskill.targetType = new SingleTarget(new List<string>());
        gskill.effects.Add(new ApplyBuffEffect("prepared_poision"));
        skillDB.AddEntry(gskill);



        gskill = new Skill("bottle_posion", 0, 0);
        gskill.skillName = "Bottle Posion";
        gskill.targetType = new SingleTarget(new List<string>());
        gskill.skillCost.Add(new SkillCostBuff("prepared_poision", true));
        gskill.effects.Add(new ConjureItemInventorySkillEffect("bottled_poison"));
        gskill.descript = "Bottle your prepared poison.";
        skillDB.AddEntry(gskill);


        gskill = new Skill("toss_poison", 0, 4);
        gskill.skillName = "Toss Poison";
        gskill.targetType = new SingleTarget(new List<string>());
        gskill.effects.Add(new ApplyBuffEffect("posioned"));
        gskill.descript = "Throw a bottle of poison at the target. Poisoning them. ";
        skillDB.AddEntry(gskill);


        //Event testing
        SkillUsedEvent skillEvent = new SkillUsedEvent("idk", "fire_ball", EffectToAddType.TargetActor);
        skillEvent.publicEvent = true;
        skillEvent.flags.Add(new FlagInt("asdf", 1));
        skillEvent.publicEvent = true;

        gskill = new Skill("future_fireball", 1, 7);
        gskill.skillName = "Future Fireball";
        gskill.targetType = new SingleTarget(new List<string>());
        gskill.effects.Add(new AddEffectToMapSkillEffect(skillEvent));
        skillDB.AddEntry(gskill);


        ////Animate Wall
        //
        /*      

        condition: must be used on a wall tile (or tile with the wall tag??)

        effects:
            Change targeted tile
            spawn unit                    


        */

        gskill = new Skill("animate_wall", 1, 4);
        gskill.conditionalsRequired.Add(new MatchingTagConditional("stone", MatchingTagConditional.MatchingType.Tile));
        gskill.skillName = "Animate Wall";
        gskill.animControllerID.Add("laser");
        gskill.animControllerID.Add("smoke");
        gskill.descript = "Animate a stone wall to create a stone guardian";
        gskill.targetType = new SingleTarget(new List<string>());
        gskill.effects.Add(new ChangeTileTypeEffect("dirt"));
        gskill.effects.Add(new SummonEffect("golem")); //we need to look up a unit to summon
        skillDB.AddEntry(gskill);

        contentContainer.skillDatabase = skillDB;


    }


    
    void Disciplines()
    {
        SavedDatabase<Discipline> disciplineDB = new SavedDatabase<Discipline>();

        Discipline discipline = new Discipline("mending");
        discipline.Name = "Plant Stuff";
        discipline.TalenPool.Add(("aura_talent"));
        discipline.TalenPool.Add(("heal"));
        discipline.TalenPool.Add(("pale_eclipse"));
        discipline.TalenPool.Add(("blizzard"));
        discipline.TalenPool.Add(("manabattery"));

        
        // discipline.TalenPool.Add(("temp_health_buff"));
        //  discipline.TalenPool.Add("turret");
        //  
        //   discipline.TalenPool.Add("conjure_talent");

        //discipline.TalenPool.Add(("knife_mech"));
        //discipline.TalenPool.Add(("guardian_mech"));      
        disciplineDB.AddEntry(discipline);

        discipline = new Discipline("familiar");
        discipline.Name = "Etherial Being";
        discipline.TalenPool.Add(("teleport"));
        disciplineDB.AddEntry(discipline);

        discipline = new Discipline("spectralhelper");
        discipline.Name = "Spectral Helper";
        discipline.TalenPool.Add("flower");
        discipline.TalenPool.Add("levitate_talent");
        discipline.TalenPool.Add(("aura_talent"));
        disciplineDB.AddEntry(discipline);

        discipline = new Discipline("Poisoncraft");
        discipline.Name = "Poison Craft";
        discipline.TalenPool.Add(("prepare_posion"));
        discipline.TalenPool.Add(("bottle_posion"));
        disciplineDB.AddEntry(discipline);

        discipline = new Discipline("Fightcraft");
        discipline.Name = "Fighting Stuff";
        discipline.TalenPool.Add("charge");
        discipline.TalenPool.Add("temp_health_buff");
        discipline.TalenPool.Add("buff_skill");
        discipline.TalenPool.Add("smash");
        discipline.TalenPool.Add("brutal_kill");
        discipline.TalenPool.Add("shield_bash");
        discipline.TalenPool.Add("guardian_stance");

        disciplineDB.AddEntry(discipline);

        discipline = new Discipline("pathfinding");
        discipline.Name = "Pathfinding";
        discipline.TalenPool.Add("bear_trap");
        discipline.TalenPool.Add("dtrait");
        discipline.TalenPool.Add("power_shot");
        disciplineDB.AddEntry(discipline);




        discipline = new Discipline("mage");
        discipline.Name = "Mage Stuff";
        discipline.TalenPool.Add(("fire_ball"));
        discipline.TalenPool.Add("lightning_bolt");
        discipline.TalenPool.Add("tempest_learn_aura");
        discipline.TalenPool.Add("archon_buff_skill");
        discipline.TalenPool.Add("animate_wall");
        discipline.TalenPool.Add("blood_maigc");
        discipline.TalenPool.Add("summon_skeleton");

        disciplineDB.AddEntry(discipline);

        
        discipline = new Discipline("arcane");
        discipline.Name = "Arcane Stuff";
        discipline.TalenPool.Add("blizzard");
        discipline.TalenPool.Add("inferno");
        discipline.TalenPool.Add("tempest_aura");
        discipline.TalenPool.Add("archon_buff_off");
        disciplineDB.AddEntry(discipline);

        


        discipline = new Discipline("corpse_crafting");
        discipline.Name = "Grave Stuff";
        discipline.TalenPool.Add("summon_skeleton");
        discipline.TalenPool.Add(("revive_unit"));
        discipline.TalenPool.Add("pale_eclipse");
        discipline.TalenPool.Add("lich");
        disciplineDB.AddEntry(discipline);


        discipline = new Discipline("fire_craft");
        discipline.Name = "Fire Stuff";
        discipline.TalenPool.Add(("fire_ball"));
        discipline.TalenPool.Add(("fire_attack"));
        disciplineDB.AddEntry(discipline);


        discipline = new Discipline("piloting");
        discipline.Name = "Piloting";
        discipline.TalenPool.Add(("knife_mech"));
        discipline.TalenPool.Add(("guardian_mech"));
        disciplineDB.AddEntry(discipline);


        discipline = new Discipline("human_skills");
        discipline.Name = "Human Shit";
        discipline.TalenPool.Add(("fire_ball"));
        disciplineDB.AddEntry(discipline);



        discipline = new Discipline("undead_skills");
        discipline.Name = "Undead Shit";
        discipline.TalenPool.Add(("vampiric_bite"));
        discipline.TalenPool.Add(("temp_health_buff"));
        disciplineDB.AddEntry(discipline);

        
        discipline = new Discipline("lichdom");
        discipline.Name = "Lich Skills";
        discipline.TalenPool.Add(("summon_skeleton"));
        discipline.TalenPool.Add(("skeltonize"));
        disciplineDB.AddEntry(discipline);


        discipline = new Discipline("vampirism");
        discipline.Name = "Vampirism";
        discipline.TalenPool.Add(("vampiric_bite"));
        discipline.TalenPool.Add(("curse"));
        disciplineDB.AddEntry(discipline);

        discipline = new Discipline("wage");
        discipline.Name = "Overtime";
        disciplineDB.AddEntry(discipline);

        contentContainer.disciplineDB = disciplineDB;
    }


    void Jobs()
    {
        SavedDatabase<Job> jobDB = new SavedDatabase<Job>();
        StatsContainer sc = new StatsContainer();
        sc.SetValue(StatTypes.Health, 25);



        Job temp = new Job("warden");
        temp.Name = "Warden";
        temp.AbilitNames = "Fighting Skills";
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("Fightcraft"));
        jobDB.AddEntry(temp);
        temp.descript = "Warden's keep the peace.";


        temp = new Job("dev");
        temp.Name = "Dev";
        temp.AbilitNames = "Everything";
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("mending"));
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("Fightcraft"));
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("pathfinding"));
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("mage"));
        temp.descript = "Job with all abilities";
        jobDB.AddEntry(temp);


        temp = new Job("spectralsupport");
        temp.Name = "Spectral Friend";
        temp.AbilitNames = "Bonds";
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("spectralhelper"));

        temp.descript = "";
        jobDB.AddEntry(temp);

        temp = temp.Copy() as Job;
        temp.ChangeKey("familiar");
        temp.Name = "Familar";
        temp.avalibleDisciples = new List<Discipline>() { contentContainer.disciplineDB.GetCopy("familiar") };
        jobDB.AddEntry(temp);


        temp = new Job("flamespeaker");
        temp.Name = "Fire Wizard";
        temp.AbilitNames = "Fire Moves";
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("fire_craft"));
        //temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("piloting"));
        jobDB.AddEntry(temp);
        temp.descript = "Pyro whispers";


        List<JobReq> req = new List<JobReq>();
        //TalentJobReq t = new TalentJobReq("flamespeaker", 2);
        MissionCompleteJobReq t = new MissionCompleteJobReq(new List<string>() { "test_mission_00" });
        req.Add(t);

        temp = new AdvancedJob("necromancer", req);
        temp.Name = "Necromancer";
        temp.AbilitNames = "Dark Arts";
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("corpse_crafting"));
        jobDB.AddEntry(temp);
        temp.descript = "Command spooky skeletons.";
        temp.statGrowth = (StatsContainer)sc.Copy();


        temp = new Job("human");
        temp.Name = "Human";
        temp.AbilitNames = "Humanitiy";
        temp.statGrowth = sc.Copy();
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("human_skills"));
        jobDB.AddEntry(temp);

        temp = new Job("undead");
        temp.Name = "Undead";
        temp.AbilitNames = "Undead Skils";
        temp.statGrowth = sc.Copy();
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("undead_skills"));
        jobDB.AddEntry(temp);

        temp = new Job("lich");
        temp.Name = "Lich";
        temp.AbilitNames = "Lich Skils";
        temp.statGrowth = sc.Copy();
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("lichdom"));
        jobDB.AddEntry(temp);

        temp = new Job("archon");
        temp.Name = "Archon";
        temp.AbilitNames = "Arcane Mastery";
        temp.statGrowth = sc.Copy();
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("arcane"));
        jobDB.AddEntry(temp);


        temp = new Job("mage");
        temp.Name = "Magic";
        temp.AbilitNames = "Magical Shit";
        temp.statGrowth = sc.Copy();
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("mage"));
        jobDB.AddEntry(temp);

        temp = new Job("plague");
        temp.Name = "Plague Slinger";
        temp.AbilitNames = "Plague Craft";
        temp.statGrowth = sc.Copy();
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("Poisoncraft"));
        jobDB.AddEntry(temp);

        temp = new Job("herb");
        temp.Name = "Herbologist";
        temp.AbilitNames = "Herbology";
        temp.statGrowth = sc.Copy();
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("mending"));
        jobDB.AddEntry(temp);


        temp = new Job("vampire");
        temp.Name = "Vampire";
        temp.avalibleDisciples.Add( contentContainer.disciplineDB.GetCopy("vampirism"));
        jobDB.AddEntry(temp);


        temp = new Job("wageslave");
        temp.Name = "Wage Slave";
        temp.AbilitNames = "Coperate Craft";
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("wage"));
        jobDB.AddEntry(temp);


        temp = new Job("path");
        temp.Name = "Tracker";
        temp.AbilitNames = "Tracker";
        temp.avalibleDisciples.Add(contentContainer.disciplineDB.GetCopy("pathfinding"));
        temp.statGrowth = sc.Copy();
        jobDB.AddEntry(temp);

        temp = new Job("dummy");
        temp.Name = "Training Dummy";
        temp.statGrowth = sc.Copy();

        campaign.GetJobsData().JobDB = jobDB;

    }


    void Races()
    {
        
        SavedDatabase<Race> raceDB = new SavedDatabase<Race>();
        Race temp = new Race("human");
        temp.Name = "Human";
        temp.statGrowth.FillGrowth();
        temp.baseStats.FillBase();
        temp.avaliablejobs.Add(("warden"));
        temp.avaliablejobs.Add(("herb"));
        temp.avaliablejobs.Add(("plague"));
        temp.avaliablejobs.Add(("path"));
        temp.avaliablejobs.Add(("mage"));
        temp.avaliablejobs.Add(("dev"));
        temp.descript = "Normies idk";
        temp.raceTags.Add("biological");
        raceDB.AddEntry(temp);

        temp = temp.Copy() as Race;
        temp.ChangeKey("undead");
        temp.Name = "Undead";
        temp.descript = "Zombies idk";
        temp.raceTags = new List<string>();
        temp.raceTags.Add("dead");
        temp.avaliablejobs = new List<string>() { "lich", "mage" };

        raceDB.AddEntry(temp);


        temp = new Race("familiar");
        temp.Name = "Familiar";
        temp.statGrowth.FillGrowth();
        temp.baseStats.FillBase();
        temp.descript = "A spirit from another world";
        temp.avaliablejobs.Add("spectralsupport");
        temp.raceTags.Add("Spectral");
        raceDB.AddEntry(temp);


        temp = new Race("vampire");
        temp.Name = "Vampire";
        temp.baseStats.FillBase();
        temp.statGrowth.FillGrowth();
        temp.descript = "Generic blook sucker";
        temp.avaliablejobs.Add("wageslave");
        raceDB.AddEntry(temp);


        campaign.GetJobsData().raceDB = raceDB;
    }



    void CutsceneDB()
    {
        SavedDatabase<CutScene> csDB = new SavedDatabase<CutScene>();

        CutScene cs = new CutScene("test_cutscene") { mapID = 0, mapname = "testlevel" };

        string winston_id = Globals.GenerateRandomHex();
        string abagail_id = Globals.GenerateRandomHex();

        cs.AddActor(new CutsceneActorPositionData("winston", winston_id, 2, 1));
        cs.AddActor(new CutsceneActorPositionData("abagail", abagail_id, 1, 2));
        cs.AddActor(new CutsceneActorPositionData("base_enemy", Globals.GenerateRandomHex(), 12, 10));
        cs.AddActor(new CutsceneActorPositionData("base_enemy", Globals.GenerateRandomHex(), 10, 12));


        DialogueAction action = new DialogueAction();
        action.actorID = "winston";
        action.dialog = "We're almost there. Just a little further.";
        cs.AddAction(action);

        List<MapCoords> path = new List<MapCoords>()
        {
            new MapCoords(2,2),
            new MapCoords(2,3),
            new MapCoords(3,3),
            new MapCoords(4,3),
            new MapCoords(5,3),
            new MapCoords(6,3),
        };

        CutsceneActionMoveSprite moveAction = new CutsceneActionMoveSprite(abagail_id, path);

        List<MapCoords> win_path = new List<MapCoords>()
        {
            new MapCoords(2,2),
            new MapCoords(3,2),
            new MapCoords(4,2),
            new MapCoords(5,2),
            new MapCoords(6,2),
            new MapCoords(7,2),
        };

        CutsceneActionMoveSprite  win_moveAction = new CutsceneActionMoveSprite(winston_id, win_path);
        CutsceneActionList csal = new CutsceneActionList();
        csal.actions.Add(win_moveAction);
        csal.actions.Add(moveAction);


        CutsceneActionChangeCameraPosition poss = (new CutsceneActionChangeCameraPosition(1, 1));
        //CutsceneActionChangeCameraPosition poss = (new CutsceneActionChangeCameraPosition(6, 12));
        csal.actions.Add(poss);


        cs.AddAction(win_moveAction);
        cs.AddAction(moveAction);


        /*
        CutsceneActionSkillAnimation anim = new CutsceneActionSkillAnimation("", "double_tap", new MapCoords(7, 3), new MapCoords(10, 10));
        //cs.AddAction(anim);

        CutsceneActionTileEffect fire = new CutsceneActionTileEffect(10, 10, "fire");
        cs.AddAction(fire);*/

        action = new DialogueAction();
        action.actorID = "abagail";
        action.dialog = "There!";
        cs.AddAction(action);

        action = new DialogueAction();
        action.actorID = "winston";
        action.dialog = "Let's kill them.";
        cs.AddAction(action);

        csDB.AddEntry(cs);

        cs = cs.Copy() as CutScene;
        cs.mapname = "inn";
       // csDB.AddEntry(cs);


        //////
        CutScene choiceCutscene = new CutScene("choice_test") { mapID = 0, mapname = "testlevel" };

        DialogueAction dia = new DialogueAction();
        dia.actorID = "winston";
        dia.dialog = "This is a test of of the choice selection please stay calm";
        choiceCutscene.AddAction(dia);

        ChoiceAction choice = new ChoiceAction();
        choice.dialog = "Please select a choice";
        choice.choices[0] = "Fire the test event";
        choice.eventFlagMission[0] = "summon_event";
        choice.choices[1] = "Play test cutscene";
        choice.nextCutscene[1] = "test_cutscene";
        choice.missionFlagToSet[1] = "flag_test_mission";
        choice.actorID = "winston";

        //choiceCutscene.AddAction(new CutsceneActionTransitionToBattle("test_mission_00"));
        
        choiceCutscene.AddAction(choice);
        csDB.AddEntry(choiceCutscene);

        //campaign.initalCutscene = "choice_test";

        campaign.SetCutsceneDatabase(csDB);
    }


    void Buff()
    {
        SavedDatabase<Buff> buffDatabase = new SavedDatabase<Buff>();

        Buff healthbuff = new Buff("500_health_buff", "unkown");
        ScalingStatBuffEffect e = new ScalingStatBuffEffect(StatContainerType.Both, StatContainerType.Max, StatTypes.Health, StatTypes.Health, .5f);
        healthbuff.effects.Add(e);
        healthbuff.maxStacks = 2;
        healthbuff.turnDuration = 7;
        healthbuff.buffName = "Health Boost";
        healthbuff.tempBuff = false;
        healthbuff.IsTrait = true;
        e.restore_stats = true;
        buffDatabase.AddEntry(healthbuff);



        Buff levitate = new Buff("levitate");
        ChangeMovementType cmove = new ChangeMovementType("flying");
        //levitate.effects.Add(cmove);
        levitate.effects.Add(new SkillOnDeathBuffEffect(true, "fire_ball"));
        ChangeTargetTypeBuffEffect tbuff = new ChangeTargetTypeBuffEffect(new BlockTarget(new List<string>(), false, 4, 4));
        tbuff.conditionalsRequired.Add(new MatchingTagConditional("fire", MatchingTagConditional.MatchingType.Skill));
        levitate.effects.Add(tbuff);
        levitate.buffName = "Levitate";
        buffDatabase.AddEntry(levitate);


        Buff flying_mount = new Buff("flying_mount");
        ChangeMovementType cm = new ChangeMovementType("flying");
        flying_mount.effects.Add(cm);
        flying_mount.buffName = "Flying Mount";
        flying_mount.IsTrait = true;
        buffDatabase.AddEntry(flying_mount);


        Buff tempHealthbuff = (Buff)healthbuff.Copy();
        tempHealthbuff.ChangeKey("temp_health_buff");
        StatBuff ef = new StatBuff(StatContainerType.Current, true);
        ef.sc.SetValue(StatTypes.Health, 50);
        tempHealthbuff.effects.Add(e);
        tempHealthbuff.IsTrait = false;
        tempHealthbuff.tempBuff = true;
        buffDatabase.AddEntry(tempHealthbuff);


        Buff fireattackbuuff = new Buff("fire_attack_buff", "flame_sword");
        BonusTagToSkillBuffEffect bde = new BonusTagToSkillBuffEffect("fire");
        fireattackbuuff.tempBuff = true;
        fireattackbuuff.turnDuration = 3;
        fireattackbuuff.buffName = "Flamming Weapon";
        fireattackbuuff.effects.Add(bde);
        buffDatabase.AddEntry(fireattackbuuff);



        Buff cursed = new Buff("curse", "unkown");
        ChangePerTurnBuffEffect effect = new ChangePerTurnBuffEffect(new ScalingStatBuffEffect(
                StatContainerType.Current, StatContainerType.Max, StatTypes.Health, StatTypes.Health, -.5f
                ));

        cursed.effects.Add(effect);
        cursed.turnDuration = 4;
        cursed.buffName = "Curse";
        cursed.tempBuff = true;
        buffDatabase.AddEntry(cursed);



        Buff guardian = new Buff("guardian", "unkown");
        StanceBuffEffect tb = new StanceBuffEffect();
        tb.transformCatagory = "pilot";
        tb.transformBuff = (Buff)healthbuff.Copy();
        ChangeSpriteBuffEffect cse = new ChangeSpriteBuffEffect();
        cse.newAnimationController = "robot";
        guardian.tempBuff = false;
        guardian.buffName = "Guardian Mech";
        guardian.effects.Add(tb);
        guardian.effects.Add(cse);
        buffDatabase.AddEntry(guardian);



        Buff knife = new Buff("knife", "unkown");
        tb = new StanceBuffEffect();
        tb.transformCatagory = "pilot";
        tb.transformBuff = (Buff)fireattackbuuff.Copy();
        knife.tempBuff = false;
        knife.buffName = "Knife Mech";
        knife.effects.Add(tb);
        buffDatabase.AddEntry(knife);


        //This is used by other skills i think
        //we should figure out a cleaner way to do stuff like this
        //but that's a problem for later
        Buff vanishing = new Buff("vanishing", "unkown");
        BanishEffect be = new BanishEffect(2);
        vanishing.effects.Add(be);
        buffDatabase.AddEntry(vanishing);



        Buff lichBuff = new Buff("lich_buff", "unkown");
        lichBuff.IsTrait = false;
        TransformBuffEffect transformEffect = new TransformBuffEffect();
        transformEffect.primary = new SwitchJobsEffect("lich", SwitchJobsEffect.JobCategory.Primary, true);
        transformEffect.race = new SwitchJobsEffect("undead", SwitchJobsEffect.JobCategory.Race, true);
        transformEffect.newSprite = new ChangeSpriteBuffEffect();
        transformEffect.newSprite.newAnimationController = "skeleton";
        transformEffect.newSprite.newPortraitpath = "skeleton_0";
        lichBuff.effects.Add(transformEffect);
        buffDatabase.AddEntry(lichBuff);

        //This buff doesn't actually summon the unit, it links the unit to it's master
        Buff turrentBuff = new Buff("test_link");
        turrentBuff.effects.Add(new LinkBuffEffect());
        buffDatabase.AddEntry(turrentBuff);

        Buff auraBuffTest = new Buff("aura_buff");
        auraBuffTest.buffName = "Flaming sword aura";
        auraBuffTest.effects.Add(new AuraBuffEffect("fire_attack_buff", 3));
        buffDatabase.AddEntry(auraBuffTest);


        // so what other buffs do we want?
        //
        Buff archon = new Buff("archon_buff");
        archon.buffName = "Archon Form";
        TransformBuffEffect arbe = new TransformBuffEffect();
        arbe.primary = new SwitchJobsEffect("Archon", SwitchJobsEffect.JobCategory.Primary, true);
        arbe.newSprite = new ChangeSpriteBuffEffect();
        arbe.newSprite.newAnimationController = "archon";
        arbe.newSprite.newPortraitpath = "skeleton_0";
        archon.tempBuff = true;
        archon.turnDuration = 4;
        //we should also do some stat regens
        ScalingStatBuffEffect mana_scaling_effect = new ScalingStatBuffEffect(StatContainerType.Both, StatContainerType.Current, StatTypes.Mana, StatTypes.Mana, .2f);
        ScalingStatBuffEffect int_scaling_effect = new ScalingStatBuffEffect(StatContainerType.Both, StatContainerType.Current, StatTypes.Potency, StatTypes.Potency, .2f);
        ScalingStatBuffEffect manaregen_scaling_effect = new ScalingStatBuffEffect(StatContainerType.Both, StatContainerType.Current, StatTypes.ManaRegen, StatTypes.ManaRegen, .2f);

        archon.effects.Add(mana_scaling_effect);
        archon.effects.Add(manaregen_scaling_effect);
        archon.effects.Add(int_scaling_effect);
        archon.effects.Add(arbe);
        buffDatabase.AddEntry(archon);

 

        Buff tempest_auraBuff = new Buff("tempest_buff");
        BonusDamageOnAttack bdoa = new BonusDamageOnAttack(15);
        MatchingTagConditional mtgc = new MatchingTagConditional("electric", MatchingTagConditional.MatchingType.Skill);
        tempest_auraBuff.buffName = "Call Tempest";
        tempest_auraBuff.tempBuff = false;
        bdoa.conditionalsRequired.Add(mtgc);
        tempest_auraBuff.effects.Add(bdoa);
        buffDatabase.AddEntry(tempest_auraBuff);




        Buff burned = new Buff("burned", "unkown");
        StatBuff sb = new StatBuff(StatContainerType.Current, false);
        FlatStatBuffEffectPerTurn bed = new FlatStatBuffEffectPerTurn(sb);
        burned.IsBuff = false;
        sb.sc.ChangeStat(StatTypes.Health, -15);
        burned.effects.Add(bed);
        burned.tempBuff = true;
        burned.turnDuration = 3;
        burned.buffName = "Burned Flesh";
        buffDatabase.AddEntry(burned);

        Buff chilled = new Buff("chilled");
        StatBuff csb = new StatBuff(StatContainerType.Current, true);
        csb.sc.SetValue(StatTypes.SpeedRating, -15);
        chilled.effects.Add(csb);
        chilled.buffName = "Chilled";
        buffDatabase.AddEntry(chilled);

        contentContainer.buffDatabase = buffDatabase;

        //This is what th user has applied ot him
        // THE TRICK IS THAT WE WILL HAVE TO ASSIGN A SECOND SKILL TO THE SKILL FOR THIS ONE NODE AHHHHHHH 
        //
        Buff guardianStance = new Buff("guardian_stance");
        //slow, apply guarded
        StatBuff gssb = new StatBuff(StatContainerType.Max, true);
        gssb.sc.SetValue(StatTypes.MovementRange, -1);
        guardianStance.effects.Add(gssb);
        guardianStance.buffName = "Guardian Stance";
        buffDatabase.AddEntry(guardianStance);

        //This is what the target has applied to them
        Buff guarded = new Buff("guarded");
        RedirectCombatBuffEffect rcbe = new RedirectCombatBuffEffect();
        guarded.effects.Add(rcbe);
        guarded.buffName = "Guarded";
        guarded.tempBuff = true;
        guarded.turnDuration = 2;
        buffDatabase.AddEntry(guarded);


        Buff stun = new Buff("stun");
        stun.effects.Add(new BlockAttackEffect());
        stun.effects.Add(new BlockMoveBuffEffect());
        stun.buffName = "Stunned";
        stun.tempBuff = true;
        stun.turnDuration = 1;
        buffDatabase.AddEntry(stun);

        // trait and bonus //
        //
        Buff desperateTrait = new Buff("dtrait");
        ApplyBuffOnTurnStartEffect t = new ApplyBuffOnTurnStartEffect("desperate_buff");
        t.conditionalsRequired.Add(new StatThresholdConditional(StatTypes.Health, StatContainerType.Current, 60, true));
        desperateTrait.effects.Add(t);
        desperateTrait.buffName = "Desperate Measures Trait";
        desperateTrait.IsTrait = true;
        buffDatabase.AddEntry(desperateTrait);


        Buff desperateTimes = new Buff("desperate_buff");
        StatBuff dtsb = new StatBuff(StatContainerType.Both, true);
        dtsb.sc.SetValue(StatTypes.SpeedRating, 50);
        desperateTimes.buffName = "Desperate Times";
        desperateTimes.effects.Add(dtsb);
        buffDatabase.AddEntry(desperateTimes);


        Buff preparedPosion = new Buff("prepared_poision");
        preparedPosion.buffName = "Prepared Poison";
        preparedPosion.description = "Apply a deadly poison to the unit's weapon.";
        preparedPosion.effects.Add(new AddSkillEffectBuffEffect(new ApplyBuffEffect("posioned")) {conditionalsRequired = new List<Conditional>() { new MatchingTagConditional("weapon", MatchingTagConditional.MatchingType.Skill)} } );
        buffDatabase.AddEntry(preparedPosion);

        Buff poisoned = new Buff("posioned");
        poisoned.buffName = "Poisoned";
        poisoned.tempBuff = true;
        poisoned.turnDuration = 3;
        StatBuff pstb = new StatBuff(StatContainerType.Current, false);
        pstb.sc.SetValue(StatTypes.Health, 20);
        preparedPosion.effects.Add(new FlatStatBuffEffectPerTurn(pstb));
        buffDatabase.AddEntry(poisoned);

        Buff bloodmagic = new Buff("blood_maigc");
        bloodmagic.buffName = "Blood Magic";
        bloodmagic.IsTrait = true;
        ModifyStatCostBuffEffect modifyStatCostBuffEffect = new ModifyStatCostBuffEffect();
        modifyStatCostBuffEffect.typeToChange = StatTypes.Mana;
        modifyStatCostBuffEffect.modifier = new ModifyIncrease(10, StatTypes.Health);
        bloodmagic.effects.Add(modifyStatCostBuffEffect);
        buffDatabase.AddEntry(bloodmagic);


        Buff manabattery = new Buff("mana_battery");
        manabattery.buffName = "Mana battery";
        manabattery.IsTrait = true;
        SkillUsedOnCastBuffEffect ss = new SkillUsedOnCastBuffEffect("random_skeleton", true);
        ss.conditionalsRequired.Add(new MatchingTagConditional("weapon", MatchingTagConditional.MatchingType.Skill));
        manabattery.effects.Add(ss);
        buffDatabase.AddEntry(manabattery);
        
    }



    void MapEnchantments()
    {
        MapEnchantment enchantment = new MapEnchantment("pale_eclipse");

        enchantment.actorEffect = new ActorBuffMapEnchantmentEffect("curse");
        enchantment.tileEffect = new TileMapBuffEnchantmentEffect("flame");
        enchantment.bg_color_gradient_itd = "pale";
        List<string> tags = new List<string>();
        tags.Add("fire");

        //enchantment.tileEffect = new TileMapPropertyEnchantmentEffect(tags);

        campaign.GetMapDataContainer().MapEnchantmentsDB.AddEntry(enchantment);


        MapEnchantment blizzard = new MapEnchantment("blizzard");
        blizzard.actorEffect = new ActorBuffMapEnchantmentEffect("chilled");
        blizzard.tileEffect = new TileMapBuffEnchantmentEffect("chilled_tile");
        blizzard.bg_color_gradient_itd = "ice";
        campaign.GetMapDataContainer().MapEnchantmentsDB.AddEntry(blizzard);

    }

    void TileEffects()
    {
        SavedDatabase<TileEffect> db = new SavedDatabase<TileEffect>();

        PropertyTagMap<int, TileEffect> effectMap = new PropertyTagMap<int, TileEffect>();


        CounterLimit l = new CounterLimit(2);
        RandomNeighborSpread s = new RandomNeighborSpread(0);
        ChangeHPComponent hp = new ChangeHPComponent(-5);
        ChangeTileTypeTileEffectCompontent newtile = new ChangeTileTypeTileEffectCompontent("dirt");


        TileEffect flame = new TileEffect("flame", "default_fire", l, s);
        flame.attributes.Add("fire");
        flame.descript = "On fire";
        flame.init.Add(new RemoveAttribute("flammable"));
        flame.turn.Add(hp);
        flame.init.Add(newtile);
        flame.animationID = "fire";
        db.AddEntry(flame);


        string k1 = "fire";
        string k2 = "flammable";
        string k3 = flame.GetKey();


        /// for mana battery
        /// k1 damage
        /// k2 explodeable
        /// explode
        /// explode > change tile type, explosion damange in radius

        effectMap.AddKey(k1, k2, 100, flame);

        campaign.GetPropertyMaps().tileEffectMap = (effectMap);
        campaign.GetTileData().Effects = db;


        TileEffect thaw = new TileEffect("thaw", "", new CounterLimit(0), new NoSpread());
        thaw.init.Add(new ChangeTileTypeTileEffectCompontent("water"));
        db.AddEntry(thaw);
        k2 = "frozen";
        effectMap.AddKey(k1, k2, 100, thaw);

        NoLengthLimit nll = new NoLengthLimit();
        NoSpread nsp = new NoSpread();
        AdjustMovementCostTileEffectComponent add = new AdjustMovementCostTileEffectComponent("walking", 1);
        AdjustMovementCostTileEffectComponent off = new AdjustMovementCostTileEffectComponent("walking", -1);
        ChangeHPComponent hp_dmg = new ChangeHPComponent(-10);

        TileEffect frosted = new TileEffect("chilled_tile", "blizzard_id", nll, nsp);
        frosted.descript = "Frozed";
        frosted.init.Add(add);
        frosted.end.Add(off);
        db.AddEntry(frosted);


        TileEffect bear_trap = new TileEffect("bear_trap", "bear_trap", new NoLengthLimit(), new NoSpread());
        bear_trap.removeOnTrigger = true;
        bear_trap.actorEnter.Add(new ChangeHPComponent(-40));
        bear_trap.descript = "Watch your step";

        TileEffect healingflowers = new TileEffect("flower", "flower", new NoLengthLimit(), new NoSpread());
        healingflowers.actorEnter.Add(new ChangeHPComponent(25));
        healingflowers.descript = "Flowers soothe all wounds.";
        healingflowers.animationID = "sparkle";
        //end behavior restore tileeffect
        db.AddEntry(healingflowers);

        db.AddEntry(bear_trap);

    }

    void Shops()
    {
        string item1 = "synth_helm";
        List<string> items = new List<string>();
        items.Add("ion_rifle");
        items.Add("health_potion");
        items.Add("synth_helm");
        items.Add("bottled_poison");

        Shop testshop = new ItemShop("test_shop", "Test Shop", items, true);
        testshop.Unlocked = true;
        campaign.GetItemDataContainer().ShopDB.AddEntry(testshop);
    }

    void SkillPropertyMap()
    {
        PropertyTagMap<float, ResistanceLevel> skillEffectMap = new PropertyTagMap<float, ResistanceLevel>();

        string skillProperty = "holy";
        string targetProperty = "undead";
        float multipler = 1;
        ResistanceLevel res = ResistanceLevel.Vulnerable;

        skillEffectMap.AddKey(skillProperty, targetProperty, multipler, res);

        campaign.GetPropertyMaps().skillEffectMap = skillEffectMap;
    }

    void WorldMaps()
    {
        WorldMap map = new WorldMap(20, 20);
        map.playerAvatarFilePath = "player_0";
        map.defaultStartPos = new MapCoords(1, 1);

        map.worldMapName = "Amber Falls";

        WorldMapTile tile = new WorldMapTile("field", 0, 9, "road");

        for (int x = 0; x < map.sizeX; x++)
        {
            for (int y = 0; y < map.sizeY; y++)
            {
                WorldMapTile temp = (WorldMapTile)tile.Copy();
                temp.position.X = x;
                temp.position.Y = y;

                map.worldMapDisplayData[x, y] = temp;


            }
        }

        LocationNode baselocationnode = new LocationNode("home base1", "baselevel", "city", 1, 1);
        map.AddLocation(baselocationnode);

        //baselocationnode.locationcomponents.Add(new BaseComponent("base"));
        baselocationnode.locationcomponents.Add(new ShopComponent("test_shop"));
        baselocationnode.locationcomponents.Add(new BarLocationComponent("test_bar"));

        LocationNode baselocationnode2 = new LocationNode("home base2", "testlevel", "city", 5, 3);
        map.AddLocation(baselocationnode2);
        // baselocationnode2.locationcomponents.Add(new BaseComponent("base"));

        LocationNode baselocationnode3 = new LocationNode("home base3", "trainingmap", "city", 2, 3);
        map.AddLocation(baselocationnode3);

        LocationNode lnode = new LocationNode("neighbor_1", "trainingmap1", "city", 6, 5);
        map.AddLocation(lnode);


        LocationNode lnode2 = new LocationNode("neighbor_2", "trainingmap2", "city", 8, 7);
        map.AddLocation(lnode2);

        LocationNode.AddNeighbors(lnode, lnode2);

        LocationNode lnode3 = new LocationNode("neighbor_3", "trainingmap3", "city", 6, 8);
        map.AddLocation(lnode3);

        LocationNode lnode4 = new LocationNode("neighbor_4", "trainingmap4", "city", 4, 7);
        map.AddLocation(lnode4);

        LocationNode.AddNeighbors(lnode, lnode2);
        LocationNode.AddNeighbors(lnode, lnode3);
        LocationNode.AddNeighbors(lnode, lnode4);

        LocationNode.AddNeighbors(lnode, baselocationnode2);
        LocationNode.AddNeighbors(baselocationnode2, baselocationnode);
        LocationNode.AddNeighbors(baselocationnode2, baselocationnode3);

        lnode.locationcomponents.Add(new TravelLocationComponent() { mapKey = map.key, startPos = new MapCoords(5,5)});

        campaign.currentWorldMap = map.key;
        campaign.worldMapDictionary.Add(map.key, map);
    }
    #endregion 


}
