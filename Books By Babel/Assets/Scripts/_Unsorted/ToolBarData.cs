using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToolBarData
{
    /// <summary>
    ///  I think this is where we store the skills we have on our hotbar?
    ///  Like which skill is in which slot?
    ///  I dont fucking know anymore man
    /// </summary>
    /// 
    public string[] skills;

    public ToolBarData()
    {
        skills = new string[10];

        for (int i = 0; i < skills.Length; i++)
        {
            skills[i] = "";
        }
    }

    public ToolBarData Copy()
    {
        ToolBarData d = new ToolBarData();

        for (int i = 0; i < skills.Length; i++)
        {
            d.skills[i] = skills[i];
        }

        return d;
    }
}
