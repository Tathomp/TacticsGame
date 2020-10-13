using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutScene : DatabaseEntry
{
    Queue<CutSceneAction> actions;

    public int mapID;
    public string mapname;
    public string bgm_Music;

    //maybe we load the actor database and use ids to populate the needed actors
    // I dont think we actually do anything with this rn
    public Dictionary<string, CutsceneActorPositionData> actorIDMap;

    public CutScene(string key) : base(key)
    {
        actions = new Queue<CutSceneAction>();

        actorIDMap = new Dictionary<string, CutsceneActorPositionData>();
    }

    public void AddAction(CutSceneAction action)
    {
        actions.Enqueue(action);
        
    }

    public void AddActor(CutsceneActorPositionData data)
    {
        actorIDMap.Add(data.uid, data);
    }

    public CutSceneAction NextAction()
    {
        return actions.Dequeue();

    }

    public bool IsDialogue()
    {
        if (IsEmpty())
            return false;

        return actions.Peek().IsDialogueAction();
    }

    public bool IsEmpty()
    {
        return actions.Count == 0;
    }

    public override DatabaseEntry Copy()
    {
        CutScene cs = new CutScene(key);
        cs.mapID = mapID;
        cs.mapname = mapname;
        cs.bgm_Music = bgm_Music;

        CutSceneAction[] temp = actions.ToArray();

        foreach (CutSceneAction csa in temp)
        {
            cs.AddAction( (CutSceneAction)csa.Copy());
        }


        foreach (string data in actorIDMap.Keys)
        {
            cs.actorIDMap.Add(data, actorIDMap[data]);
        }

        return cs;
    }
}

[System.Serializable]
public class CutsceneActorPositionData
{
    public string actorID;
    public string uid;
    public MapCoords position;

    public CutsceneActorPositionData(string actorID, string uid, int posX, int posY)
    {
        this.actorID = actorID;
        this.uid = uid;
        position = new MapCoords(posX, posY);
    }

    public CutsceneActorPositionData(string actorID, string uid, MapCoords coords)
    {
        this.actorID = actorID;
        this.uid = uid;
        this.position = coords;
    }

    public CutsceneActorPositionData Copy()
    {
        return new CutsceneActorPositionData(actorID, uid, position);
    }



}

