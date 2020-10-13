using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bar : DatabaseEntry
{
    public string BarName;
    public List<RandomSpawnData> randomspawnsPool, activeSpawns;
    public int maxrandommisison, minrandommissions;

    public Bar(string key, string name) : base(key)
    {
        this.BarName = name;
        randomspawnsPool = new List<RandomSpawnData>();
        activeSpawns = new List<RandomSpawnData>();
    }

    public override DatabaseEntry Copy()
    {
        Bar temp = new Bar(key, BarName);
        temp.randomspawnsPool = new List<RandomSpawnData>();
        temp.activeSpawns = new List<RandomSpawnData>();
        return temp;
    }

    public void AddRandomSpawnData(List<RandomSpawnData> data)
    {
        foreach (RandomSpawnData d in data)
        {
            activeSpawns.Add(d.Copy());
        }
    }
}
