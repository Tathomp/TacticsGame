using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditTileStatsDisplay : MonoBehaviour
{
    public EditTileTypesPanel panel;
    public ScrollListScaleableContent container;
    public TextButton button_prefab;
    public StatDataObject stat_data_obj_prefab;

    //
    private List<StatDataObject> sdo_list = new List<StatDataObject>();
    private TextButton new_button_instance;

    //

    public void InitStatDisplay(EditTileTypesPanel panel)
    {
        this.panel = panel;

        ClearList();

        PrintStatBonuses();
        PrintNewButton();

    }

    private void PrintStatBonuses()
    {
        TileTypes t = panel.GetCurrentTileType();

        foreach (StatTypes types in t.tileBonuses.GetKeys())
        {
           sdo_list.Add( PrintStatDataObjButton(types.ToString(), t.tileBonuses.GetValue(types)));
        }

    }

    private void PrintNewButton()
    {
        if(new_button_instance != null)
        {
            new_button_instance.button.onClick.RemoveAllListeners();
            Destroy(new_button_instance.gameObject);
        }

        new_button_instance = Instantiate<TextButton>(button_prefab, container.contentTransform);
        new_button_instance.ChangeText("New Stat Bonus");
        new_button_instance.button.onClick.AddListener(delegate { NewButtonPressed(); });
    }

    private void NewButtonPressed()
    {
        ClearList();



        PrintStatBonuses();

        sdo_list.Add(PrintStatDataObjButton());

        PrintNewButton();
    }

    private StatDataObject PrintStatDataObjButton(string s, int value)
    {
        StatDataObject obj = PrintStatDataObjButton();

        obj.Init(panel, s, value);

        return obj;
    }

    private StatDataObject PrintStatDataObjButton()
    {
        StatDataObject obj = Instantiate<StatDataObject>(stat_data_obj_prefab, container.contentTransform);

        obj.dropdown.ClearOptions();

        foreach(StatTypes t in System.Enum.GetValues(typeof(StatTypes)))
        {
            obj.dropdown.options.Add(new TMP_Dropdown.OptionData(t.ToString()));
        }

        //Defaults
        obj.Init(panel, "", 0);
        return obj;
    }

    private void ClearList()
    {
        int x = sdo_list.Count - 1;

        for (int i = x; i >= 0; i--)
        {
            Destroy(sdo_list[i].gameObject);
        }

        sdo_list = new List<StatDataObject>();
    }

    public void RemoveSDO(StatDataObject obj)
    {
        sdo_list.Remove(obj);

        Destroy(obj.gameObject);
    }

    public void SaveStatData()
    {
        foreach (StatDataObject item in sdo_list)
        {
            item.Save();
        }
    }
}
