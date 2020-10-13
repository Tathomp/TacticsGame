using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIController : ActorController
{
    public List<AIGoal> aigoals;
    Dictionary<string, float> costMap;

    public AIController()
    {
        aigoals = new List<AIGoal>();
        EmptyAction action = new EmptyAction();
    }

    public void TakeAITurn(BoardManager bm, Actor ai)
    {
       ai.StartCoroutine(CPUTUrn(bm, ai));
    }


    IEnumerator CPUTUrn(BoardManager bm, Actor ai)
    {

        List<AIAction> aiaction = new List<AIAction>();

        foreach (AIGoal goal in aigoals)
        {
           yield return  goal.CalculateActions(aiaction, bm, ai);
        }


        //choose and action to perform


        //sort the action list

        //ai.Attack();
       
        AIAction action = GetActionToUSe(aiaction);
        //AIAction action = new MoveToPlayerAction(ai, new MapCoords(0, 0));

        Debug.Log("Choosen score: " + action.GetScore());

        if (action.GetScore() != 0)
        {

           ai.StartCoroutine(
            action.ExecuteAction(ai, bm));
        }
        //execute action

    }
    





    public List<TileNode> MoveTowardsPlayer(Actor currActor, BoardManager bm)
    {
        int x = 0, y = 0;

        foreach (TileNode tile in bm.pathfinding.tiles)
        {
            if (tile.actorOnTile != null)
            {
                if (tile.actorOnTile.actorData.controller.PlayerControlled())
                {
                    x = tile.data.posX;
                    y = tile.data.posY;
                }
            }
        }


        return bm.pathfinding.GenerateMovementPath(currActor, x, y);
    }
    

    public override bool PlayerControlled()
    {
        return false;
    }


    public AIAction GetActionToUSe(List<AIAction> validActions)
    {
       // DebugDisplay dis = GameObject.Find("Debug").GetComponent<DebugDisplay>();

        AIAction currAction = validActions[0];

        costMap = new Dictionary<string, float>();

        //Debug.Log(validActions.Count);

        foreach (AIAction action in validActions)
        {
            //assuming we want the action with the highest score
            //
            if(action.GetScore() > currAction.GetScore())
            {
                currAction = action;

            }

            if (costMap.ContainsKey(action.ToString()))
            {
                if (costMap[action.ToString()] < action.GetScore())
                {
                    costMap[action.ToString()] = action.GetScore();
                }
            }
            else
            {
                costMap.Add(action.ToString(), action.GetScore());
            }
        }


       // dis.UpdateDisplay(costMap);




        return currAction;
    }

}
