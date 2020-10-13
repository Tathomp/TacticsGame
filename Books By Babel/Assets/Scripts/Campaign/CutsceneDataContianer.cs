using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutsceneDataContianer
{
    public SavedDatabase<CutScene> cutsceneDatabase;
    public MissionHandler missionHandler;
    public SavedDatabase<Bar> barDatabase;

    public CutsceneDataContianer()
    {
        cutsceneDatabase = new SavedDatabase<CutScene>();
        missionHandler = new MissionHandler();
        barDatabase = new SavedDatabase<Bar>();
    }

}
