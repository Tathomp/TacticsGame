using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Flags
{
    private string FlagID;

    public Flags(string id)
    {
        FlagID = id.ToLower().Trim();
    }

    public abstract bool CheckFlagStatus();

    public string GetFlagID()
    {
        return FlagID;
    }

    public bool IsFlag(string id)
    {
        return FlagID == id;
    }
}
