using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JobsAllowedPanel : MonoBehaviour
{
    public TMP_Text list;

    public void InitPanel(Item item)
    {
        string job = "";


        foreach (string s in item.validJobs)
        {
            job += s + "\t";
        }


        list.text = job;

    }
}
