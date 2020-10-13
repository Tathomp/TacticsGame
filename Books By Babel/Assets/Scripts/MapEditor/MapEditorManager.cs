using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MapEditorManager : MonoBehaviour {

    public GameObject saveDialog;
    public GameObject loadDialog;
    public GameObject deleteDialog;


    public MapDataModel currBoard;

    SpriteAtlas atlas;
    TileDatabaseContainer tileDatabase;

    GameObject tilePrefab;

    public GameObject TileButtonPrefab;
    public GameObject TilePanel;

    public string currSelection = null;

	// Use this for initialization
	void Start ()
    {
        Globals.currentLibrary = ((SavedFile)SaveLoadManager.LoadFile(FilePath.DefaultSaveFile)).campaign.contentLibrary;

        atlas = Resources.Load<SpriteAtlas>(FilePath.TileSetAtlas);
        tileDatabase = Globals.campaign.GetTileData();
        tilePrefab = Resources.Load<GameObject>(FilePath.TilePrefab);

        foreach (TileTypes tt in tileDatabase.Tiles.database.Values)
        {
            GameObject go = Instantiate<GameObject>(TileButtonPrefab, TilePanel.transform);
           // go.name = "" + tt.ID;
           // Debug.Log(tt.ID);
            go.GetComponent<Image>().sprite = atlas.GetSprite(tt.spriteFilePath);
           // go.GetComponent<TileButton>().editor = this;
            go.GetComponent<TileButton>().selectedTileType = tt.GetKey();
        }

    }

    void SelectContentLibrary()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            // Debug.Log(Globals.WorldToGrid(mousePos));
            mousePos.z = 10;

            Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);

            if(hit)
            {
                Vector2 grid = Globals.WorldToGrid(hit.collider.name);
                currBoard.tileBoard[(int)grid[0], (int)grid[1]] = currSelection;

                //here is where we should do tile map stuff
                /* TODO: 
                 height map in mapdata model
                    2d int array with hieght values (negative is below base level, position above)
                Tile y-offets based on height map
                    When the map is being drawn
                    deactivate colliders on top tile when we 
                 */

                Debug.Log(currSelection);
                PrintBoard();
            }
        }
    }

    public void InitBOard()
    {
        currBoard = new MapDataModel("new_map",15, 15);

        for (int x = 0; x < currBoard.sizeX; x++)
        {
            for (int y = 0; y < currBoard.sizeY; y++)
            {
                currBoard.tileBoard[x, y] = tileDatabase.Tiles.DbKeys()[0];
            }
        }

        PrintBoard();

    }

    public void PrintBoard()
    {
        ClearBoard();

        for (int x = 0; x < currBoard.sizeX; x++)
        {
            for (int y = 0; y < currBoard.sizeY; y++)
            {
               
                GameObject tile = Instantiate(tilePrefab, transform);
                tile.transform.position = Globals.GridToWorld(x, y);
                tile.GetComponent<SpriteRenderer>().sortingOrder = -(x + y);
                tile.name = x + " " + y;
                tile.GetComponent<SpriteRenderer>().sprite = atlas.GetSprite(
                    tileDatabase.Tiles.GetData(currBoard.tileBoard[x, y]).spriteFilePath);

                
            }
        }

    }

    public void ClearBoard()
    {

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SaveDialog()
    {
        //Just opens save dialog
        saveDialog.SetActive(true);
    }

    public void LoadDialog()
    {
        //Just opens save dialog
        loadDialog.SetActive(true);
    }

    public void DeleteDialog()
    {
        deleteDialog.SetActive(true);
    }

}
