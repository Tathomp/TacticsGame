using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditTileTypesPanel : MonoBehaviour
{
    public CreationSuiteManager creationManager;

    // TileType buttons
    public TextButton tiletpye_button_prefab;
    private TextButton new_tile_instance;

    public ScrollListScaleableContent textButtonList;

    //
    public MovementDataObject mdo_prefab;
    public List<MovementDataObject> mdo_lists = new List<MovementDataObject>();

    //
    public TextButton default_button;
    public TextButton instance;

    //Sprite data
    public EditTileTypeSprite spriteEditing;

    //Tile data fields
    public Image sprite_thumbnail;
    public TMP_InputField tile_name;
    public ScrollListScaleableContent movementContainer, tags, stats;

    // Tags
    public DisplayTileTypeTags tiletypetags;
    public EditTileStatsDisplay statdisplay;

    // Containers
    //public TileTypes curr_tiledata;
    private string current_tile;
    public TileDatabaseContainer container;

    public TextMeshProUGUI keyText;

    public void InitEditTileTypePanel()
    {
        creationManager.SetCurrentActiveObject(this.gameObject);
        gameObject.SetActive(true);
        container = creationManager.currentCampaign.GetTileDatabaseContainer();
        

        if (creationManager.currentCampaign != null)
        {
            ClearButtons();
            PrintTileTypeBUttons();
            PrintNewTileButton();

        }
    }

    //Prints buttons mapped to the current tile types in the 
    private void PrintTileTypeBUttons()
    {
        string[] key = container.Tiles.DbKeys();

        foreach (string k in key)
        {
            TextButton temp = Instantiate<TextButton>(tiletpye_button_prefab, textButtonList.contentTransform);
            temp.button.onClick.AddListener(delegate { DataButtonClicked(k); });
            temp.ChangeText(container.Tiles.GetData(k).TileName);

            textButtonList.AddToList(temp);
        }

    }

    public void DataButtonClicked(string k)
    {
        current_tile = k;

        UpdateData();
    }

    public void UpdateData()
    {
        spriteEditing.InitDisplay(this);

        tile_name.text = GetCurrentTileType().TileName;
        keyText.text = GetCurrentTileType().GetKey();
        //////////////
        /// Stat Types
        ///////////////
        statdisplay.InitStatDisplay(this);

        ///////////////////
        // Movement Cost
        ////////////////////
        PrintMovementBonuses();

        ////////
        // Tags
        ///////
        tiletypetags.InitDisplay(this);
    }

    public void NewTileButtonPressed()
    {
        

        ClearTileTypeButtons();

        NewTileType();

        PrintTileTypeBUttons();

        PrintNewTileButton();
    }

    public void PrintNewTileButton()
    {
        if (new_tile_instance != null)
        {
            new_tile_instance.button.onClick.RemoveAllListeners();
            Destroy(new_tile_instance.gameObject);
        }

        new_tile_instance = Instantiate<TextButton>(tiletpye_button_prefab, textButtonList.contentTransform);
        new_tile_instance.ChangeText( "New Tile Type");
        new_tile_instance.button.onClick.AddListener(delegate { NewTileButtonPressed(); });
    }

    public void NewTileType()
    {
        TileTypes t = new TileTypes(Globals.GenerateRandomHex());
        t.TileName = "Unnamed";

        current_tile = t.GetKey();

        container.Tiles.AddEntry(t);

        UpdateData();
    }

    void ClearTileTypeButtons()
    {
        textButtonList.CleanUp();
    }

    #region Movement
    // Movement
    // maybe move this to a different script at some point
    public void SaveMovementData()
    {
        foreach (MovementDataObject item in mdo_lists)
        {
            item.SaveValue();
        }
    }

    public void PrintMovementBonuses()
    {
        ClearButtons();

        TileTypes curr_tiledata = GetCurrentTileType();

        foreach (string key in curr_tiledata.MovementTypeCostMap.Keys.ToArray())
        {
            MovementDataObject obj = NewMovementDataObject();
            obj.movement_type = key;
            obj.cost = curr_tiledata.MovementTypeCostMap[key];
            obj.input.text = "" + obj.cost;

            mdo_lists.Add(obj);
        }

        PrintNewMovementButton();
    }

    void PrintNewMovementButton()
    {
        instance = Instantiate<TextButton>(default_button, movementContainer.contentTransform);
        instance.ChangeText("New Movement");
        instance.button.onClick.AddListener(delegate { NewButtonClicekd(); });

    }

    void NewButtonClicekd()
    {
        instance.button.onClick.RemoveAllListeners();
        Destroy(instance.gameObject);

        MovementDataObject data = NewMovementDataObject();
        mdo_lists.Add(data);

        PrintNewMovementButton();
    }
    
    public void ClearButtons()
    {
        if(instance != null)
        Destroy(instance.gameObject);

        int x = mdo_lists.Count - 1;

        for (int i = x; i >= 0; i--)
        {
            Destroy(mdo_lists[i].gameObject);
        }

        mdo_lists = new List<MovementDataObject>();
    }

    public MovementDataObject NewMovementDataObject()
    {


        MovementDataObject obj = Instantiate<MovementDataObject>(mdo_prefab, movementContainer.contentTransform);
        obj.dropdown.ClearOptions();
        obj.InitMovement(GetCurrentTileType().GetKey(), this);

        foreach (string movement in creationManager.currentCampaign.movementTypes)
        {
            obj.dropdown.options.Add(new TMP_Dropdown.OptionData(movement));
        }

        return obj;
    }
    #endregion
    ////////////
    // Tags
    ////////////

    //This weill be the general save method for all tiletypes?
    //mabye
    //no just all the components for the current tile types
    public void SaveTileData()
    {
        SaveMovementData();
        statdisplay.SaveStatData();
        tiletypetags.SaveTagData();

        GetCurrentTileType().TileName = tile_name.text;

        spriteEditing.SaveSprite();


        ///by now all the data should be written directly to the tile type data base
        ///we'd just have to save the campaign data to the disk;
    }

    public void DeleteCurrentTile()
    {
        //delete tile type
        container.Tiles.RemoveEntry(current_tile);

        ClearTileTypeButtons();
        PrintTileTypeBUttons();
    }

    public TileTypes GetCurrentTileType()
    {
        return container.Tiles.GetData(current_tile);
    }

}
