using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class StatEditPrefab : MonoBehaviour
{
    public TMP_Text statLabel;
    public TMP_InputField input;

    StatsContainer sc;
    StatTypes type;
    public void InitConatinerPanel(StatsContainer sc, StatTypes stat)
    {
        this.sc = sc;
        this.type = stat;

        statLabel.text = stat.ToString();
        input.text = sc.GetValue(stat) + "";

    }

    public void UpdateData()
    {
        sc.SetValue(type, Int32.Parse(input.text));
    }
}
