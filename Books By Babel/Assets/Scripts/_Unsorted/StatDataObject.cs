using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatDataObject : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMP_InputField input;

    private string currentTile;
    private EditTileTypesPanel panel;


    private string stat;
    private int value;

    public void Init(EditTileTypesPanel panel, string stat, int value)
    {
        this.panel = panel;
        this.stat = stat;
        this.value = value;

        for (int i = 0; i < dropdown.options.Count; i++)
        {
            if(dropdown.options[i].text == stat)
            {
                dropdown.value = i;
            }
        }


        input.text = value + "";
    }

    private void Temp()
    {
        stat = dropdown.options[dropdown.value].text;
        value = int.Parse(input.text);

    }

    public void DeleteStatBonus()
    {

        Temp();
        TileTypes t = panel.GetCurrentTileType();

        if (t.tileBonuses.statDict.ContainsKey((StatTypes)System.Enum.Parse(typeof(StatTypes), stat)))
        {
            t.tileBonuses.statDict.Remove((StatTypes)System.Enum.Parse(typeof(StatTypes), stat));
        }

        panel.statdisplay.RemoveSDO(this);
    }


    public void Save()
    {
        Temp();

        StatTypes t = (StatTypes) System.Enum.Parse( typeof( StatTypes), stat );

        panel.GetCurrentTileType().tileBonuses.SetValue(t, value);
    }
}
