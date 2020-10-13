using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutsceneEvent : Event
{
    private string cutsceneID;

    public CutsceneEvent(string id, string cutsceneID) : base(id)
    {
        this.cutsceneID = cutsceneID;
    }

    public override DatabaseEntry Copy()
    {
        return new CutsceneEvent(key, cutsceneID);
    }

    public override string DisplayText()
    {
        return "";
    }

    public override void FireEvent()
    {
        Globals.GetBoardManager().InitCutsceneData(cutsceneID, CinematicStatus.DuringBattle);
    }
}
