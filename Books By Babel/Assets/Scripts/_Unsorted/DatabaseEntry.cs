using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class DatabaseEntry 
{
    protected string key;

    public DatabaseEntry(string key)
    {
        this.key = key.ToLower().Trim();
    }

    public abstract DatabaseEntry Copy();

    public string GetKey()
    {
        return key.ToLower().Trim();
    }

    public void ChangeKey(string key)
    {
        this.key = key.ToLower().Trim();
    }
}
