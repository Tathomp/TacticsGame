using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SavedDatabase <T> where T: DatabaseEntry
{
    public Dictionary<string, T> database;


    public SavedDatabase()
    {
        database = new Dictionary<string, T>();
    }


    public void AddEntry(T v)
    {
        database.Add(v.GetKey(), v);
    }

    public void RemoveEntry(string k)
    {
        database.Remove(k);
    }

    private T GetEntry(string entry)
    {

        string key = entry.ToLower().Trim();


        if (!database.ContainsKey(key))
        {
            Debug.Log("Failed to find key: " + key + " " + database.Values.GetType());

            foreach (string k in database.Keys)
            {
                Debug.Log(k);
            }
        }

        return (T)database[key];
    }

    public T GetCopy(string key)
    {
        return (T)GetEntry(key).Copy();
    }

    public T GetData(string key)
    {
        return GetEntry(key);
    //    return (T)GetEntry(key).Copy();
    }

    public void UpdateEntry(T v)
    {
        database[v.GetKey()] = null;
        database[v.GetKey()] = v;
    }

    internal string[] DbKeys()
    {
        return database.Keys.ToArray();
    }
}
