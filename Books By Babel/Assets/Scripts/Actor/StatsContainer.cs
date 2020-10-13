using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class StatsContainer
{
    public Dictionary<StatTypes, int> statDict = new Dictionary<StatTypes, int>();


    public StatsContainer()
    {
        InitDict();
    }

    public StatTypes[] GetKeys()
    {
        return statDict.Keys.ToArray();
    }

    public void SetValue(StatTypes key, int value)
    {
        statDict[key] = value;
    }

    public void AddStats(StatsContainer sc)
    {
        StatTypes[] k = sc.GetKeys();

        foreach (StatTypes st in k)
        {
            statDict[st] += sc.statDict[st];
        }
    }

    public void RemoveStats(StatsContainer sc)
    {
        StatTypes[] k = sc.GetKeys();

        foreach (StatTypes st in k)
        {
            statDict[st] -= sc.statDict[st];
        }
    }


    public void ChangeStat(StatTypes type, int delta)
    {
        statDict[type] += delta;
    }

    public void InitDict()
    {
        statDict.Add(StatTypes.Health, 0);
        statDict.Add(StatTypes.Mana, 0);
        statDict.Add(StatTypes.Strength, 0);
        statDict.Add(StatTypes.Defenese, 0);
        statDict.Add(StatTypes.Potency, 0);
        statDict.Add(StatTypes.Resistence, 0);
        statDict.Add(StatTypes.Evade, 0);
        statDict.Add(StatTypes.HealthRegen, 0);
        statDict.Add(StatTypes.ManaRegen, 0);
        statDict.Add(StatTypes.MovementRange, 0);
        statDict.Add(StatTypes.SpeedRating, 0);
        statDict.Add(StatTypes.Speed, 0);
        statDict.Add(StatTypes.MinRange, 0);
        statDict.Add(StatTypes.MaxRange, 0);
        statDict.Add(StatTypes.NumberOfMovements, 0);
        statDict.Add(StatTypes.NumberOfActions, 0);
    }

    public void FillBase()
    {
        SetValue(StatTypes.Health, 100);
        SetValue(StatTypes.Mana, 50);
        SetValue(StatTypes.Strength, 8);
        SetValue(StatTypes.Defenese, 4);
        SetValue(StatTypes.Potency, 7);
        SetValue(StatTypes.Resistence, 3);
        SetValue(StatTypes.Evade, 1);
        SetValue(StatTypes.HealthRegen, 5);
        SetValue(StatTypes.ManaRegen, 2);
        SetValue(StatTypes.MovementRange, 5);
        SetValue(StatTypes.SpeedRating, 10);
        SetValue(StatTypes.Speed, 0);
        SetValue(StatTypes.MinRange, 0);
        SetValue(StatTypes.MaxRange, 1);
        SetValue(StatTypes.NumberOfMovements, 1);
        SetValue(StatTypes.NumberOfActions, 1);
    }


    public void FillGrowth()
    {
        SetValue(StatTypes.Health, 10);
        SetValue(StatTypes.Mana, 5);
        SetValue(StatTypes.Strength, 1);
        SetValue(StatTypes.Defenese, 1);
        SetValue(StatTypes.Potency, 1);
        SetValue(StatTypes.Resistence, 1);
        SetValue(StatTypes.Evade, 1);
        SetValue(StatTypes.HealthRegen, 1);
        SetValue(StatTypes.ManaRegen, 1);
        SetValue(StatTypes.MovementRange, 1);
        SetValue(StatTypes.SpeedRating, 1);
        SetValue(StatTypes.Speed, 0);
    }


    public string PrintStats()
    {
        string s = "";

        foreach (StatTypes k in statDict.Keys)
        {
            if(statDict[k] != 0)
            s+= k.ToString() + " " + statDict[k].ToString() + "\n";
        }

        return s;
    }


    public int GetValue(StatTypes key)
    {
        if (statDict.ContainsKey(key))
        {
            return statDict[key];
        }
        else
        {
            throw new System.EntryPointNotFoundException();
        }
    }


    public StatsContainer Copy()
    {
        StatsContainer sc = new StatsContainer();

        StatTypes[] statypes = statDict.Keys.ToArray<StatTypes>();

        foreach (StatTypes k in statypes)
        {
            sc.SetValue(k, statDict[k]);
        }

        return sc;

    }


    public static StatsContainer AddSC(StatsContainer sc1, StatsContainer sc2)
    {
        StatsContainer temp = (StatsContainer)sc1.Copy();

        temp.AddStats(sc2);

        return temp;
    }
}
