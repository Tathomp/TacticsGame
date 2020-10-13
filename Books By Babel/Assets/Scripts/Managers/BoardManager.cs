using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class BoardManager : MonoBehaviour
{

    public static BoardManager _instance;

    public GameObject victoryGO;
    public TileEffectSprite[,] EffectBoard;
   // public Board board;
    public Pathfinding pathfinding;

    public TurnManager turnManager;
    public Selector Selector;
    public BattleUIManager ui;
    public InputFSM inputFSM;

    public TileSelction tileSelection;

    [HideInInspector]
    public MapDataModel currMap;
    SpriteAtlas atlas;
    public ActorSpawner spawner;

    [HideInInspector]
    public Campaign campaign;
    public Mission currentMission;
    public Party party;


    void Start ()
    {
        _instance = this;

        //Just a testing thing
        if (Globals.campaign == null)
        {
            Debug.Log("DEPENDICIES INJECTED");

            GenerateDemoCampaign dcm = new GenerateDemoCampaign();
            Globals.campaign = dcm.campaign;

            campaign = dcm.campaign;
            party = campaign.currentparty;

            currentMission = campaign.GetMissionData("test_mission_00");
            currentMission.started = false;
            SavedFileMission state = new SavedFileMission(campaign, currentMission);
            FilePath.CurrentSaveFilePath = SaveLoadManager.GenerateSaveStateFilePath(state.campaign.GetFileName());
            SaveLoadManager.AutoSaveCampaignProgress(state);
        }
        else
        {
            campaign = Globals.campaign;
            party = campaign.currentparty;
        }


        //Actual initialization starts here
        Globals.currState = GameState.Combat;


        SavedFileMission file = SaveLoadManager.LoadFile(FilePath.CurrentSaveFilePath) as SavedFileMission;
        currentMission = file.currentMission;

        currMap = Globals.campaign.GetMapDataContainer().mapDB.GetCopy(currentMission.mapName);

        if(currentMission.started)
        {
            for (int x = 0; x < currMap.sizeX; x++)
            {
                for (int y = 0; y < currMap.sizeY; y++)
                {
                    currMap.tileBoard[x, y] = file.TileTypeStates[x, y].GetKey();
                }
            }
        }

        inputFSM = new InputFSM(new BlockUserInputState());
        atlas = Resources.Load<SpriteAtlas>(FilePath.TileSetAtlas);

        GenerateBoard();

        //we are loading a mission
        if (currentMission.started)
        {
            StartBattleFromLoad();

            for (int x = 0; x < currMap.sizeX; x++)
            {
                for (int y = 0; y < currMap.sizeY; y++)
                {
                    pathfinding.tiles[x, y].tileGO.ChangeTileType(file.TileTypeStates[x, y]);

                    foreach (TileEffect effect in file.TileEffectStates[x,y])
                    {

                        if (effect is AuraTileEffect)
                        {
                            //pathfinding.tiles[x, y].AddTileEffect(effect.Copy() as AuraTileEffect);

                        }
                        else
                        {
                            pathfinding.tiles[x, y].AddTileEffect(effect.Copy() as TileEffect);
                        }
                    }
                }
            }

            turnManager.globalTurnSpeed = file.currentTurnSpeed;

        }
        else // we starting a new mission
        {
            Globals.cutsceneData = null;
            InitBattledata();

            //ui.TurnOffInfoPanels();
            currentMission.started = true;
        }
    }

    // Update is called once per frame
    void Update ()
    {      
        inputFSM.ProcessInput();
    }


    private void OnDestroy()
    {
        _instance = null;
    }


    public SavedFileMission SaveMission()
    {
        //Remove the tile bonuses given to actors
        // These bonus should be given back when the game is loaded
        //
        pathfinding.RemoveAllActorStats(); //I think we're doing this because
        SavedFileMission file = new SavedFileMission(campaign, currentMission);
        file.SaveTileState(pathfinding.tiles);
        file.currentTurnSpeed = turnManager.globalTurnSpeed;

        pathfinding.AddAllStats();
        return file;
    }


    void Missionfailed()
    {
        Debug.Log("Misison failed");
        ui.gameoverPanel.SetActive(true);
    }


    void MissionComplete()
    {
        pathfinding.RemoveAllActorStats();

        foreach (Actor actor in spawner.actors)
        {
            actor.RemoveAllBuffs();
            actor.actorData.ResetCharges();
        }

        int i = 0;

        foreach (ActorData m in currentMission.npcs)
        {
            i++;
        }


        currentMission.mainReward.AddReward(new ExperienceReward(i * 1000));
        Debug.Log(i + " enemies killed");


        Globals.campaign.MissionCompleted(currentMission.GetKey());


        Debug.Log("MISSION COMPLETE");
        currentMission.MissionComplete(this);


        //
        inputFSM.SwitchState(new RewardState(this));


        foreach (ActorData ad in party.partyCharacter)
        {
            ad.selected = false;
            ad.RestoreCurrentStats();
        }

        if (campaign.campaignModifier.permaDeath == false)
        {
            foreach (ActorData actorData in party.partyCharacter)
            {
                if (actorData.selected && actorData.isAlive == false)
                {
                    actorData.isAlive = true;
                }
            }
        }


        SavedFile state = new SaveStateWorldMap(Globals.campaign);
        SaveLoadManager.AutoSaveCampaignProgress(state);

        ui.rewardPanel.InitRewardList(this);

    }


    void GenerateBoard()
    {
        GameObject go = Resources.Load<GameObject>(FilePath.TilePrefab);
        SavedDatabase<TileTypes> tileDB = Globals.campaign.GetTileData().Tiles;

        int sizeX = currMap.sizeX;
        int sizeY = currMap.sizeY;

        pathfinding = new Pathfinding(sizeX, sizeY);
        EffectBoard = new TileEffectSprite[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                //Heres where we'll probably adjust the height map stuff

                GameObject temp = Instantiate(go, transform);
                TileData data = new TileData(x, y);

                // maybe we should create a new board that will just replace the board in the current
                // map?
                TileTypes types = tileDB.GetCopy(currMap.tileBoard[x, y]);
                //currMap.tileBoard[x, y] = types;

                Tile newTile = temp.GetComponent<Tile>();
                temp.GetComponent<Tile>().InitTile(data, types);

                pathfinding.AddTile(newTile, x, y);

                // temp.transform.position = Globals.HeightCorrectedPosition(pathfinding.tiles[x, y]);
                temp.transform.position = Globals.GridToWorld(x,y);

                temp.GetComponent<SpriteRenderer>().sprite = atlas.GetSprite(types.spriteFilePath);
                temp.name = x + " " + y;

                // we create a concection between the tile effects on the map and the


            }
        }

        if(currentMission.started == false)
        {
            foreach (Tuple<MapCoords, List<string>> key in currentMission.initTileEffects)
            {
                int x = key.ele1.X;
                int y = key.ele1.Y;

                List<TileEffect> effects = new List<TileEffect>();

                foreach (string s in key.ele2)
                {
                    effects.Add(Globals.campaign.GetTileData().Effects.GetCopy(s));
                }

                pathfinding.tiles[x, y].tileEffects = effects;
                pathfinding.tiles[x, y].InitTileEffect();
                pathfinding.tiles[x, y].InitTileEffectsVisuals();
                pathfinding.tiles[x, y].ProcessEffectQueue();

            }
        }

        pathfinding.PopulateNieghbors();

    }
  

    public void ExitGame()
    {
        Application.Quit();
    }

    public void IterateTurn()
    {
        currentMission.currentTurn++;
        ui.turnOrderPanel.UpdateTurnOrderHeaderDisplay(currentMission.currentTurn);

        Debug.Log("Turn Number: " + currentMission.currentTurn);

        foreach (TileNode tile in pathfinding.tiles)
        {
            tile.ProccessTurn();

        }

        foreach (TileNode tile in pathfinding.tiles)
        {
            tile.ProcessEffectQueue();
        }


    }

    public bool CheckEventsAndCompletion()
    {
        ui.eventDisplayPanel.InitDisplay();

        bool evenFired = currentMission.CheckEvents();

        ui.objectivesPanel.InitMissionObjectivePanel(currentMission);


        if (currentMission.CheckIfComplete(this))
        {
            //mission complete
            inputFSM.SwitchState(new BlockUserInputState());
            MissionComplete();
            return true;


        }
        else if(currentMission.CheckIfFailed(this))
        {
            inputFSM.SwitchState(new BlockUserInputState());
            Missionfailed();
            return true;
        }

        return evenFired;
    }

    public void InitCutsceneData(string cutSceneData, CinematicStatus status)
    {
        
        ui.ToggleOffBattleUI();

        inputFSM.SwitchState(new BoardDialogInputState(Globals.campaign.GetCutsceneCopy(cutSceneData), this));
    }


    public void InitBattledata()
    {
        ui.tileselctionPanel.gameObject.SetActive(true);
        
        InitUI();

        //ui.characterSelectionInfoPanel.UpDatePlacementNumber(0, currentMission.maxUnitsAllowed);

        inputFSM.SwitchState(new ExamineBattlefieldInputState(this));


    }


    void StartBattleFromLoad()
    {

        InitUI();


        foreach (ActorData d in party.partyCharacter)
        {
            if (d.selected)
            {
                spawner.GenerateActor(d, this, false);
            }
        }

        turnManager.InitTurnManager(this);
        turnManager.CalculateFastest();

        inputFSM.SwitchState(new UsersTurnState(this));



    }


    void InitUI()
    {
        tileSelection = new TileSelction(currMap.sizeX, currMap.sizeY, pathfinding);

        Selector = new Selector(this);
        turnManager = new TurnManager();
        spawner = new ActorSpawner(this);
        spawner.GenerateActor(currentMission, this);

        // ui.actionMenu.InitMenu(this);
        // ui.boardManager = this;
        //ui.skillPanel.boardManager = this;

        // ui.turnOrderPanel.gameObject.SetActive(true);
        //ui.tileselctionPanel.gameObject.SetActive(true);

        ui.ToggleOnBattleUI();

        turnManager.InitTurnManager(this);

        //ui.objectivesPanel.InitMissionObjectivePanel(currentMission);

    }


    public void ResetEffectAnimations()
    {
        StartCoroutine(ProcessEffectRest());

    }

    IEnumerator ProcessEffectRest()
    {

        for (int x = 0; x < currMap.sizeX; x++)
        {
            for (int y = 0; y < currMap.sizeY; y++)
            {
                if (EffectBoard[x, y] != null)
                {
                    EffectBoard[x, y].ResetAnimation();
                }
            }
        }
        yield return null;
    }

}

