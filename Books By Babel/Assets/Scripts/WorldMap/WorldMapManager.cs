using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldMapManager : MonoBehaviour {

    public WorldMap currWorldMap;
    public WorldMapTileGameObject[,] tiledisplay;
    public WorldMapLocationGameObject[,] locationDisplay;
    public Dictionary<string, WorldMapLocationGameObject> locationDictionary, locationNameDict;

    public WorldAvatar playerAvatarGO;
    public InputFSM worldMapInput;

    // Editor
    public WorldMapUIManager worldMapUIManager;
    public Transform tileContainer;
    public GameObject questIconPrefab, shopPrefab;


    public WorldMapTileGameObject tileGOPrefab;
    public WorldMapLocationGameObject locationPrefab;
    public WorldMapSelector worldMapelectorInstance;
    public LineTest linePrefab;
    public WorldAvatar playerAvatar;


    private List<GameObject> icons;


    private void Start()
    {

        Globals.currState = GameState.Base;

        if(Globals.campaign == null)
        {
            GenerateDemoCampaign dcm = new GenerateDemoCampaign();
            Globals.campaign = dcm.campaign;
        }



        currWorldMap = Globals.campaign.worldMapDictionary[Globals.campaign.currentWorldMap];
        icons = new List<GameObject>();

        PrintBoard();
        PrintPlayer();

        InitInputState();
        InitIcons();

        Debug.Log("Loaded: " + Globals.campaign);


        worldMapUIManager.InitWorldMap();


    }

    private void InitInputState()
    {

        worldMapelectorInstance.InitSelector(this);
        worldMapelectorInstance.MoveSelectorTo(playerAvatarGO.position.X, playerAvatarGO.position.Y);


        NavigateWorldInputState currstate = new NavigateWorldInputState(this, worldMapUIManager);

        worldMapInput = new InputFSM(currstate);

    }

    private void Update()
    {
        //Debug.Log( Globals.MouseToWorld());

        worldMapInput.ProcessInput();
    }

    private void PrintPlayer()
    {
        playerAvatarGO = Instantiate<WorldAvatar>(playerAvatar);
        playerAvatarGO.AssignSprite(currWorldMap.playerAvatarFilePath);
        playerAvatarGO.SetPosition(currWorldMap.GetPositionForAvatar().X, currWorldMap.GetPositionForAvatar().Y);
    }

    public void PrintBoard()
    {
        tiledisplay = new WorldMapTileGameObject[currWorldMap.sizeX, currWorldMap.sizeY];
        locationDisplay = new WorldMapLocationGameObject[currWorldMap.sizeX, currWorldMap.sizeY];

        locationDictionary = new Dictionary<string, WorldMapLocationGameObject>();
        locationNameDict = new Dictionary<string, WorldMapLocationGameObject>();
        for (int x = 0; x < currWorldMap.sizeX; x++)
        {
            for (int y = 0; y < currWorldMap.sizeY; y++)
            {
                if(currWorldMap.worldMapDisplayData[x,y] != null)
                {
                    WorldMapTileGameObject tilego = Instantiate<WorldMapTileGameObject>(tileGOPrefab, tileContainer);
                    tilego.InitWorldMapObject(currWorldMap.worldMapDisplayData[x, y]);
                    tiledisplay[x, y] = tilego;
                    tilego.name = x + " " + y;

                    if (currWorldMap.locations[x,y] != null)
                    {
                        //spawn the location
                        LocationNode currNode = currWorldMap.locations[x, y];          

                        WorldMapLocationGameObject go = Instantiate<WorldMapLocationGameObject>(locationPrefab, tileContainer);
                        go.InitGameObject(currNode);
                        locationDisplay[x, y] = go;
                        go.name = x + " " + y;
                        locationDictionary.Add(currNode.MapKey, go);
                        locationNameDict.Add(currNode.AreaName, go);
                        DrawNeighbors(currNode);


                    }
                }

            }
        }


    }

    // Draws the lines between all the neighboring nodes
    //
    private void DrawNeighbors(LocationNode currNode)
    {
        List<MapCoords> neighbors = currNode.neighbors;

        foreach (MapCoords loc in neighbors)
        {
            if(locationDisplay[loc.X, loc.Y] != null)
            {
                LineTest line = Instantiate<LineTest>(linePrefab);

                line.SetEndPoints(locationDisplay[currNode.coords.X, currNode.coords.Y].gameObject, locationDisplay[loc.X, loc.Y].gameObject);
                line.InitLine();
            }
        }
    }

    // Adds the missions to the nodes
    //

    public void InitIcons()
    {
        CleanMissions();
        PopulateMissions();
        DisplayShopIcons();
    }

    private void DisplayShopIcons()
    {
        for (int x = 0; x < currWorldMap.sizeX; x++)
        {
            for (int y = 0; y < currWorldMap.sizeY; y++)
            {
                if (currWorldMap.locations[x, y] != null)
                {
                    LocationNode currNode = currWorldMap.locations[x, y];

                    foreach (LocationComponent component in currNode.locationcomponents)
                    {
                        if (component is ShopComponent)
                        {
                            //icons.Add(Instantiate<GameObject>(shopPrefab, locationDisplay[x, y].transform));
                        }
                    }
                }

            }
        }
    }

    private void PopulateMissions()
    {
        List<string> missions = Globals.campaign.GetMissionHandler().MissionsAccepted;

        foreach (string key in locationDictionary.Keys.ToArray())
        {
            locationDictionary[key].missions = new List<Mission>();
        }

        foreach (string key in missions)
        {
            Mission mission = Globals.campaign.GetMissionData(key);

            if(locationDictionary.ContainsKey(mission.mapName))
            {
                locationDictionary[mission.mapName].missions.Add(mission);
                icons.Add( Instantiate<GameObject>(questIconPrefab, locationDictionary[mission.mapName].transform));

                Debug.Log("Mission " + mission.MissionName + " added to location " + locationDictionary[mission.mapName].location.AreaName);
            }
            else
            {
                Debug.Log("Mission not on this map");
            }
        }
    }

    private void CleanMissions()
    {
        for (int i = icons.Count - 1; i >= 0; i--)
        {
            Destroy(icons[i].gameObject);
            Destroy(icons[i]);
        }

        icons = new List<GameObject>();
    }


    #region World Map Pathfinding
    public List<LocationNode> GeneratePath(int sourceX, int sourceY, int targetX, int targetY)
    {
        int sizeX = currWorldMap.sizeX;
        int sizeY = currWorldMap.sizeY;

        Dictionary<LocationNode, float> dist = new Dictionary<LocationNode, float>();
        Dictionary<LocationNode, LocationNode> prev = new Dictionary<LocationNode, LocationNode>();

        List<LocationNode> path = new List<LocationNode>();
        List<LocationNode> unvistied = new List<LocationNode>();

        LocationNode source = currWorldMap.locations[sourceX, sourceY];
        LocationNode target = currWorldMap.locations[targetX, targetY];

        dist[source] = 0;
        prev[source] = null;

        foreach (LocationNode v in currWorldMap.locations)
        {
            if (v != null)
            {
                if (v != source)
                {
                    dist[v] = Mathf.Infinity;
                    prev[v] = null;
                }

                unvistied.Add(v);
            }
        }

        while (unvistied.Count > 0)
        {
            LocationNode u = null;

            foreach (LocationNode possibleU in unvistied)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvistied.Remove(u);

            foreach (MapCoords v in u.neighbors)
            {
                float alt = dist[u] + CoastToEnterTile(u.coords.X, u.coords.Y, v.X, v.Y);

                LocationNode n = currWorldMap.locations[v.X, v.Y];

                if (alt < dist[n])
                {
                    dist[n] = alt;
                    prev[n] = u;
                }
            }
        }

        if (prev[target] == null)
        {
          //  return null;
        }

        LocationNode curr = target;

        while (curr != null)
        {
            path.Add(curr);
            curr = prev[curr];
        }


        //change this to do somekind of BFS instead to find the nearest tile that's in range? idk


        path.Reverse();


        return path;

    }
    private float CoastToEnterTile(int sourceX, int sourceY, int destX, int destY)
    {
        float cost = 1;

        if(sourceX != destX && sourceY != destY)
        {
            cost += 0.001f;
        }

        return cost;
    }
    #endregion
}
