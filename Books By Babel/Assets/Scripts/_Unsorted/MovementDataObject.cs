using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovementDataObject : MonoBehaviour
{

    public string movement_type;
    public int cost;

    private string currentType;
    private EditTileTypesPanel panel;
    // Editor
    public TMP_Dropdown dropdown;
    public TMP_InputField input;

   public void InitMovement(string currentType, EditTileTypesPanel panel)
    {
        this.currentType = currentType;
        this.panel = panel;
    }

    public void DropdownCanged()
    {
        movement_type = dropdown.options[dropdown.value].text;
    }

    public void ValueChanged()
    {
        cost = int.Parse(input.text);
    }

    public void SaveValue()
    {
        TileTypes t = panel.GetCurrentTileType();
      

        if(t.MovementTypeCostMap.ContainsKey(movement_type))
        {
            t.MovementTypeCostMap[movement_type] = cost;
        }
        else
        {
            //survey10 swagbucks
            t.MovementTypeCostMap.Add(movement_type, cost);
        }
    }

    public void DeleteMovementData()
    {

        panel.container.Tiles.GetData(currentType).MovementTypeCostMap.Remove(movement_type);
    }

}
