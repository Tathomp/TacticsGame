using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlagBool : Flags
{
    private bool status;
    
    public FlagBool(string id, bool status=false) : base(id)
    {
        this.status = status;
    }

    public override bool CheckFlagStatus()
    {
        return status;
    }

    public void ChangeFlag(bool value)
    {
        status = value;
    }
}
