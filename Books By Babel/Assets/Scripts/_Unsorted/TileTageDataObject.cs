using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileTageDataObject : MonoBehaviour
{
    public string t;
    private EditTileTypesPanel panel;

    public TMP_Dropdown dropdown;

    public void InitTagObj(string tag, EditTileTypesPanel panel)
    {
        this.t = tag;
        this.panel = panel;
    }

    public void TagChanged()
    {
       t = dropdown.options[dropdown.value].text;
    }

    public void Save()
    {
       panel.GetCurrentTileType().attributes.Add(t);
    }

    public void RemoveTag()
    {
        panel.GetCurrentTileType().attributes.Remove(t);


        panel.tiletypetags.DestoryBUtton(this);
    }
}
