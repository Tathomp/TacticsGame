using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Campaign
{
    public string CampaignName { get; set; }
    public string CampaignDescrtiption { get; set; }
    public string thumbnailName;

    public string fileName;
    public Party currentparty;
    public DifficultyModifier campaignModifier;

    //flag name, flag value
    public Dictionary<string, Flags> GlobalFlags;
    /// New Stuff
    ///
    private ItemDataContainer ItemDataContainer;
    private CutsceneDataContianer CutsceneDataContainer;
    private EffectMapsDataContainer EffectMapDataContainer;
    private TileDatabaseContainer TileDataContainer;
    private MapDataContainer MapData;
    private PropertyMapsContainer PropertyMaps;
    private JobsDataContainer JobsData;
    private Glossary glossary;



    public List<string> properties = new List<string>();
    public List<string> movementTypes;

    private List<string> cutscenes_watched;

    public string initalCutscene, initalCombat;
    /// Old stuff
    ///
    public ContentLibrary contentLibrary;
    public RespecCost RespecModel;
    public RelationshipMap relationshipMap; //put in the propertymaps container
    //public ShopContainer CampaignShops;

    public string currentWorldMap;

    public Dictionary<string, WorldMap> worldMapDictionary = new Dictionary<string, WorldMap>();

    public Campaign(string cname, string fileName)
    {

        CampaignName = cname;
        this.fileName = fileName;
        CampaignDescrtiption = "no campaign description";
        thumbnailName = "default";

        cutscenes_watched = new List<string>();

        /// New Data stuff
        ///
        ItemDataContainer = new ItemDataContainer();
        CutsceneDataContainer = new CutsceneDataContianer();
        EffectMapDataContainer = new EffectMapsDataContainer();
        TileDataContainer = new TileDatabaseContainer();
        MapData = new MapDataContainer();
        PropertyMaps = new PropertyMapsContainer();
        JobsData = new JobsDataContainer();

        GlobalFlags = new Dictionary<string, Flags>();

        movementTypes = new List<string>() { "walking", "flying" };
        properties.Add("flammable");
        properties.Add("on fire");

        initalCutscene = "";
        initalCombat = "";

        campaignModifier = new DifficultyModifier();
    }


    public string GetFileName()
    {
        return fileName;
    }


    public string GetFilePath()
    {
        return fileName + FilePath.CampExt;
    }

    #region Jobs Data Container
    public JobsDataContainer GetJobsData()
    {
        return JobsData;
    }
    #endregion

    #region Property Maps
    public PropertyMapsContainer GetPropertyMaps()
    {
        return PropertyMaps;
    }

    public void SetPropertyMaps(PropertyMapsContainer maps)
    {
        PropertyMaps = maps;
    }
    #endregion


    #region Map Data Container
    public MapDataContainer GetMapDataContainer()
    {
        return MapData;
    }

    public void SetMapDataContainer(MapDataContainer db)
    {
        MapData = db;
    }

    #endregion


    #region Tile Data
    public TileDatabaseContainer GetTileData()
    {
        return TileDataContainer;
    }
    #endregion


    #region Item Database Container
    public ItemDataContainer GetItemDataContainer()
    {
        return ItemDataContainer;
    }

    public SavedDatabase<Item> GetAllItems()
    {
        return ItemDataContainer.itemDB;
    }

    public Item GetItemCopy(string key)
    {
        return ItemDataContainer.GetitemCopy(key);
    }

    public Item GetItemData(string key)
    {
        return ItemDataContainer.GetItemData(key);
    }
    #endregion


    #region Cutscene Database Container
    public CutScene GetCutsceneCopy(string key)
    {
        return CutsceneDataContainer.cutsceneDatabase.GetCopy(key);
    }

    public CutScene GetCutsceneData(string key)
    {
        return CutsceneDataContainer.cutsceneDatabase.GetData(key);
    }

    public void SetCutsceneDatabase(SavedDatabase<CutScene> db)
    {
        CutsceneDataContainer.cutsceneDatabase = db;
    }
    #endregion


    #region Mission Handler
    public List<string> GetAllAvaliableMissions()
    {
        return CutsceneDataContainer.missionHandler.GetAvaliableMissions();
    }

    public void MissionCompleted(string key)
    {
        //we can do the random generattion here
        //
        CutsceneDataContainer.missionHandler.GenerateRandomMissions(worldMapDictionary[currentWorldMap]);

        CutsceneDataContainer.missionHandler.MissionHasBeenCompleted(key);
    }

    public bool HasMissionBeenCompleted(string key)
    {
        return CutsceneDataContainer.missionHandler.CheckCompletion(key);
    }

    public Mission GetMissionData(string key)
    {
        return CutsceneDataContainer.missionHandler.GetEntryData(key);
    }

   
    public Mission GetMissionDataCopy(string key)
    {
        return GetMissionData(key).Copy() as Mission;
    }

    public MissionHandler GetMissionHandler()
    {
        return CutsceneDataContainer.missionHandler;
    }

    public void SetMissionDatabase(MissionHandler handler)
    {
        CutsceneDataContainer.missionHandler = handler;
    }
    #endregion

    public CutsceneDataContianer GetcutScenedataContainer()
    {
        return CutsceneDataContainer;
    }

    public Glossary GetGlossary()
    {
        return glossary;
    }

    public void SetGlossary(Glossary glossary)
    {
        this.glossary = glossary;
    }

    public void AddCutsceneToWatchList(CutScene scene)
    {
        if(cutscenes_watched.Contains(scene.GetKey()) == false)
        {
            cutscenes_watched.Add(scene.GetKey());
        }
    }

    public List<string> GetCutscenesWatched()
    {

        return cutscenes_watched.ToList();
    }

    public TileDatabaseContainer GetTileDatabaseContainer()
    {
        return TileDataContainer;
    }
}
