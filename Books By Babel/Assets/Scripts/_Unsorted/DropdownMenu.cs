using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownMenu : MonoBehaviour
{
    public TMP_Dropdown droptDown;

    public string GetValue(int i)
    {
        return droptDown.options[i].text;
    }

    public string GetValue()
    {
        return GetValue(droptDown.value);
    }
    public void AddList(List<string> toAdd, string value="")
    {
        ClearList();

        foreach (string item in toAdd)
        {
            AddList(item);
            if(item == value)
            {
                droptDown.value = droptDown.options.Count - 1;
            }
        }


        droptDown.RefreshShownValue();
    }

    public void AddList(string toAdd, string current = "")
    {
        droptDown.options.Add(new TMP_Dropdown.OptionData(toAdd));

        if(current == toAdd)
        {
            droptDown.value = droptDown.options.Count - 1;
        }
    }

    public void ClearList()
    {
        droptDown.ClearOptions();
    }

    public void ClearListeners()
    {
        droptDown.onValueChanged.RemoveAllListeners();
    }
}
