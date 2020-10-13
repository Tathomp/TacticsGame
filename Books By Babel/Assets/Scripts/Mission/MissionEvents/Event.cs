using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Event : DatabaseEntry
{
    public List<Flags> flags;
    public bool publicEvent;

    public Event(string id) : base(id)
    {
        flags = new List<Flags>();
        publicEvent = false;
    }

    public bool EventShouldFire()
    {
        foreach (Flags flag in flags)
        {
            if (flag.CheckFlagStatus() == false)
            {
                return false;
            }
        }

        return true;
    }

    public Flags GetFlag(string id)
    {
        foreach (Flags f in flags)
        {
            if (f.GetFlagID() == id)
            {
                return f;
            }
        }


        return null;
    }

    public abstract string DisplayText();

    public abstract void FireEvent();

    public void CopyFlags(Event e)
    {
        foreach (Flags flag in flags)
        {
            e.flags.Add(flag);
        }
    }
}
