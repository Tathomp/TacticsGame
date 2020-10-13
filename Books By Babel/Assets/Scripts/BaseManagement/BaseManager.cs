using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class BaseManager : MonoBehaviour
{
    [HideInInspector]
    public Party party;
    [HideInInspector]
    public Campaign campaign;
    public BaseUI baseUI;

    public Button missionselectButotn;

    MapDataModel baseMap;
    Pathfinding pathfinding;
    SpriteAtlas atlas;

    ContentLibrary contentContainer;

    public InputFSM inputFSM;

	// Use this for initialization
	void Start ()
    {
        Globals.currState = GameState.Base;

        SaveStateBase stat = (SaveStateBase)(SaveLoadManager.LoadFile(FilePath.CurrentSaveFilePath));

        campaign = stat.campaign;

        contentContainer = campaign.contentLibrary;

        baseUI.campaign = campaign;

        baseMap = Globals.campaign.GetMapDataContainer().mapDB.GetCopy(stat.baseID);

        atlas = Resources.Load<SpriteAtlas>(FilePath.TileSetAtlas);

        PrintBoard();

        inputFSM = new InputFSM(new BlockUserInputState());
	}


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            baseUI.CollapseAll();
        }

        inputFSM.ProcessInput();
    }


    private void PrintBoard()
    {
        GameObject go = Resources.Load<GameObject>(FilePath.TilePrefab);
        SavedDatabase<TileTypes> tiles = Globals.campaign.GetTileData().Tiles;

        int sizeX = baseMap.sizeX;
        int sizeY = baseMap.sizeY;

        pathfinding = new Pathfinding(sizeX, sizeY);

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                // Heres where we'll probably adjust the height map stuff
                GameObject temp = Instantiate(go, transform);
                TileData data = new TileData(x, y);

                // Creating new tile types
                // We have to rework how maps are saved
                TileTypes types = tiles.GetCopy(baseMap.tileBoard[x, y]);

                Tile newTile = temp.GetComponent<Tile>();
                temp.GetComponent<Tile>().InitTile(data, types);

                pathfinding.AddTile(newTile, x, y);

                temp.transform.position = Globals.GridToWorld(x, y);

                temp.GetComponent<SpriteRenderer>().sprite = atlas.GetSprite(types.spriteFilePath);
                temp.name = x + " " + y;

            }
        }

        pathfinding.PopulateNieghbors();
    }

}
