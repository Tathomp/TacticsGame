using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Glossary
{
    public string glossaryName;

    private List<string> Entries;
    private Dictionary<string, bool> UnlockedEntriesDict;
    private Dictionary<string, string> EntriesDict;

    public Glossary(string glossaryName)
    {
        Entries = new List<string>();
        UnlockedEntriesDict = new Dictionary<string, bool>();
        EntriesDict = new Dictionary<string, string>();

        this.glossaryName = glossaryName;
    }

    public string GetEntry(string key)
    {
        if(UnlockedEntriesDict[key] == true)
        {
            return EntriesDict[key];
        }

        return "Locked Entry";
    }

    public void UnlockEntry(string key)
    {
        UnlockedEntriesDict[key] = true;
    }

    public void SetLock(string key, bool value)
    {
        if(UnlockedEntriesDict.ContainsKey(key))
        {
            UnlockedEntriesDict[key] = value;
        }
        else
        {
            UnlockedEntriesDict.Add(key, value);
        }
    }

    public void AddEntry(string key, string definition)
    {
        Entries.Add(key);
        SetLock(key, false);
        SetDefinition(key, definition);
    }

    public void SetDefinition(string key, string definition)
    {
        if (EntriesDict.ContainsKey(key))
        {
            EntriesDict[key] = definition;
        }
        else
        {
            EntriesDict.Add(key, definition);
        }
    }

    public List<string> GetAllUnlockedEntries()
    {
        List<string> entries = new List<string>();
        string[] keys = UnlockedEntriesDict.Keys.ToArray();

        foreach (string key in keys)
        {
            if(UnlockedEntriesDict[key] == true)
            {
                entries.Add(EntriesDict[key]);
            }
        }

        return entries;
    }
}
