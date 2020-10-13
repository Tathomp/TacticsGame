using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MapEditingPanel : MonoBehaviour
{

    ///Editor
    ///
    public CreationSuiteManager manager;

    public TMP_InputField sizeXField, sizeYField, mapNameField;
    public Transform mapContainer; //for printin the map i think
    public MapCreationTile tilePrefab;
    public TilePanel tileSelectionPanel;

    public ScrollListScaleableContent mapList;
    public TextButton missionSelectPrefab;

    [HideInInspector]
    public MapDataModel mapDataModel;
    Campaign currCampaign;

    public MapCreationTile[,] tileBoard;

    public MapEditingState drawState;

    //private
    int sizeX, sizeY;


    public void InitEditingPanel(Campaign campaign)
    {
        currCampaign = campaign;

        manager.SetCurrentActiveObject(this.gameObject);

        gameObject.SetActive(true);
        tileSelectionPanel.InitTilePanel(currCampaign);

        PrintMapList();
    }

    public void ResizeMap()
    {

        int oldX = sizeX;
        int oldY = sizeY;

        int newX = int.Parse(sizeXField.text);
        int newY = int.Parse(sizeYField.text);


        string[,] newArray = new string[newX, newY];

        for (int x = 0; x < newX; x++)
        {
            for (int y = 0; y < newY; y++)
            {
                if(x >= oldX || y >= oldY)
                {
                    newArray[x, y] = GetDefaultTileType();
                }
                else
                {
                    newArray[x, y] = mapDataModel.tileBoard[x, y];
                }
            }
        }

        ClearBoard();

        sizeX = newX;
        sizeY = newY;

        mapDataModel.sizeX = newX;
        mapDataModel.sizeY = newY;

        mapDataModel.tileBoard = newArray;

        PrintMap();
    }

    public void SaveMap()
    {
        string mapName = mapNameField.text;
        SavedDatabase<MapDataModel> mdb = currCampaign.GetMapDataContainer().mapDB;

        if (mdb.database.ContainsKey(mapName))
        {
            mdb.database.Remove(mapName);
        }

        mdb.AddEntry(mapDataModel);

        SaveLoadManager.Savecampaign(currCampaign);

    }

    public void LoadMap(string key)
    {
        string mapName = key;

        ClearBoard();

        mapDataModel = currCampaign.GetMapDataContainer().mapDB.GetData(mapName);

        sizeX = mapDataModel.sizeX;
        sizeY = mapDataModel.sizeY;

        mapNameField.text = key;
        sizeXField.text = mapDataModel.sizeX + "";
        sizeYField.text = mapDataModel.sizeY + "";


        PrintMap();

    }

    public void NewMap()
    {
        ClearBoard();

        sizeX = int.Parse(sizeXField.text);
        sizeY = int.Parse(sizeYField.text);

        string mapName = mapNameField.text;

        mapDataModel = new MapDataModel(mapName, sizeX, sizeY);
        mapDataModel.mapName = mapName;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                mapDataModel.tileBoard[x,y] = GetDefaultTileType();
            }
        }

        PrintMap();
    }

    public void ToggleOff()
    {
        mapList.CleanUp();

        gameObject.SetActive(false);
    }

    public void PrintMap()
    {

        tileBoard = new MapCreationTile[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                MapCreationTile tile = Instantiate<MapCreationTile>(tilePrefab, mapContainer.transform);
                tile.transform.position = Globals.GridToWorld(x + (int)mapContainer.position.x, y + (int)mapContainer.position.y);
                tile.name = x + " " + y;
                tile.InitMapTile(mapDataModel.tileBoard[x,y], x, y);
                tileBoard[x, y] = tile;
            }
        }
    }

    public void ClearBoard()
    {
        if(tileBoard == null)
        {
            return;
        }

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                GameObject.Destroy(tileBoard[x, y].gameObject);
                GameObject.Destroy(tileBoard[x, y]);
            }
        }
    }

    public bool InBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && x < sizeX && y < sizeY;
    }



    public void FillMap(int startPosX, int startPosY, string newType)
    {
        Stack<MapCreationTile> tileStack = new Stack<MapCreationTile>();
        HashSet<MapCreationTile> visitedSet = new HashSet<MapCreationTile>();
        List<MapCreationTile> tilesToFill = new List<MapCreationTile>();

        MapCreationTile currTile = tileBoard[startPosX, startPosY];

        string tileTypeToFill = currTile.currType;
        tileStack.Push(currTile);

        while(tileStack.Count > 0)
        {
            if(currTile.currType == tileTypeToFill)
            {
                tilesToFill.Add(currTile);

                MapCreationTile testTile;

                int x = currTile.posX;
                int y = currTile.posY;

                if (InBounds(x, y + 1))
                {
                    testTile = tileBoard[x, y + 1];

                    if (testTile.currType == tileTypeToFill
                            && !visitedSet.Contains(testTile))
                    {
                        tileStack.Push(testTile);
                        visitedSet.Add(testTile);
                    }
                }

                if (InBounds(x + 1, y))
                {
                    testTile = tileBoard[x + 1, y];

                    if (testTile.currType == tileTypeToFill
                             && !visitedSet.Contains(testTile))
                    {
                        tileStack.Push(testTile);
                        visitedSet.Add(testTile);
                    }
                }

                if (InBounds(x, y - 1))
                {
                    testTile = tileBoard[x, y - 1];

                    if (InBounds(x, y - 1)
                             && testTile.currType == tileTypeToFill
                             && !visitedSet.Contains(testTile))
                    {
                        tileStack.Push(testTile);
                        visitedSet.Add(testTile);
                    }
                }


                if (InBounds(x - 1, y))
                {
                    testTile = tileBoard[x - 1, y];
                    if (testTile.currType == tileTypeToFill
                        && !visitedSet.Contains(testTile))
                    {
                        tileStack.Push(testTile);
                        visitedSet.Add(testTile);
                    }
                }



                currTile = tileStack.Pop();
            }
        }

        foreach (MapCreationTile tile in tilesToFill)
        {
            tile.ChangeTileType(newType);
            mapDataModel.tileBoard[tile.posX, tile.posY] = newType;
        }
    }

    public string GetDefaultTileType()
    {
        return "grass";
    }

    public string GetMapName()
    {
        return mapNameField.text;
    }


    public void PrintMapList()
    {
        mapList.CleanUp();

        foreach (string item in currCampaign.GetMapDataContainer().mapDB.DbKeys())
        {
            TextButton temp = Instantiate<TextButton>(missionSelectPrefab, mapList.contentTransform);
            temp.ChangeText( item);
            temp.button.onClick.AddListener(delegate { LoadMap(item); });
            mapList.AddToList(temp);
        } 
    }

}
