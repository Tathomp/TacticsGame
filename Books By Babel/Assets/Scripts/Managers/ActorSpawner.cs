using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ActorSpawner
{
    public List<Actor> actors;

    public Dictionary<ActorData, Actor> ActorDataGameObjectMap;

    Pathfinding pathfind;

    public ActorSpawner(BoardManager boardManager)
    {
        pathfind = boardManager.pathfinding;
        actors = new List<Actor>();

        ActorDataGameObjectMap = new Dictionary<ActorData, Actor>();
    }

    public void GenerateActor(Mission currMission, BoardManager bm, bool restoreStats = true)
    {
        GenerateActor(currMission.npcs, bm, restoreStats);
    }

    //maybe swap the guts of this generate actor and the next one down, that probs makes more sense
    public void GenerateActor(ActorData data, BoardManager bm, bool restore = true)
    {
        List<ActorData> temp = new List<ActorData>();

        temp.Add(data);

        GenerateActor(temp, bm, restore);
    }


    public void GenerateActor(List<ActorData> currMission, BoardManager bm, bool restoreStats = true)
    {
        foreach (ActorData actor in currMission)
        {
            Actor temp = GameObject.Instantiate(Resources.Load<Actor>("BaseObjects/Actor"));
            temp.InitActor(actor, bm);

            if (restoreStats)
            {
                actor.RestoreCurrentStats();
            }

            actors.Add(temp);

            ActorDataGameObjectMap.Add(actor, temp);

            //Globals.AssignSprite(temp.gameObject, actor.portraitFilePath);
            string fp = "AnimationControllers/" + actor.animationController;

            //temp.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(fp);
            temp.GetComponent<Animator>().runtimeAnimatorController = Globals.GEtAnatimationController(actor.animationController);

            temp.GetPosX();
            temp.GetPosY();


            if (!actor.isAlive)
            {
                temp.KillActor();
            }
            else
            {
                actor.buffContainer.OnActorSpawn(actor);
            }
        }

        foreach (Actor a in actors)
        {
            a.GetComponent<Animator>().Rebind();
        }
        
    }



    public void GenerateActor(ActorData data, BoardManager bm, int x, int y)
    {
        data.gridPosX = x;
        data.gridPosY = y;

        GenerateActor(data, bm);
    }

    public void RemoveActor(Actor actor)
    {
        actors.Remove(actor);
        ActorDataGameObjectMap.Remove(actor.actorData);

        GameObject.Destroy(actor.gameObject);
        GameObject.Destroy(actor);
    }

    public Actor GetActor(ActorData data)
    {
        foreach (Actor actor in actors)
        {
            if(actor.actorData == data)
            {
                return actor;
            }
        }

        return null;
    }

}
