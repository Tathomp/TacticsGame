using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager
{
    public bool FastestFound;
    public Actor currFastest;
    public int globalTurnSpeed = 0;
    public int globalTurrnSpeedRating = 10;
    public List<Actor> actorList;

    BoardManager board;
    Selector selector;
    TurnOrderPanel orderPanel;
    TileNode[,] tiles;

    GameObject turnIndicator;
    GameObject currTurn;


    bool stupidLockToPayForOurSins;

    public TurnManager()
    {
        actorList = new List<Actor>();
        turnIndicator = Resources.Load<GameObject>("BaseObjects/CurrentTurn");
        currTurn = new GameObject();

        globalTurnSpeed = 0;
        stupidLockToPayForOurSins = true;
    }

    
    public void InitTurnManager(BoardManager boardManager)
    {
        actorList = boardManager.spawner.actors;
        currFastest = actorList[0];
        selector = boardManager.Selector;
        orderPanel = boardManager.ui.turnOrderPanel;
        tiles = boardManager.pathfinding.tiles;
        board = boardManager;
    }


    public void CalculateFastest()
    {
        if (true)
        {
            while (currFastest.GetCurrentStats(StatTypes.Speed) < 100)
            {
                foreach (Actor actor in actorList)
                {
                    if (actor.actorData.isAlive)
                        actor.UpdateSpeed();
                }

                Fastest();

                //here's a global turn cycle
                globalTurnSpeed += globalTurrnSpeedRating;
                if (globalTurnSpeed >= 100)
                {
                    globalTurnSpeed -= 100;
                    //do global turn stuff

                    board.IterateTurn();
                    Debug.Log("Global turn passed");
                }
            }

            // So here is where the magic happens for the whatever unit's
            //turn it is
            if (board.CheckEventsAndCompletion() == false)
            {
                ActorTakesTurn();

            }
            else
            {
                stupidLockToPayForOurSins = false;
                //CalculateFastest();
            }

            //  Debug.Log(currFastest.actorData.Name + "'s  turn");
        }
    }

    public void ActorTakesTurn()
    {
        Globals.GetBoardManager().ResetEffectAnimations();

        GameObject.Destroy(currTurn);
        currTurn = GameObject.Instantiate(turnIndicator, currFastest.transform.position, Quaternion.identity);

        currFastest.StartTurn();

        if (currFastest.actorData.isAlive)

        {
            selector.MoveTo(currFastest.GetPosX(), currFastest.GetPosY());

            if (!currFastest.ActorsController().PlayerControlled())
            {
                board.inputFSM.SwitchState(new CPUInputState(board));
                ((AIController)currFastest.ActorsController()).TakeAITurn(board, currFastest);
            }
            else
            {
                board.inputFSM.SwitchState(new UsersTurnState(board));
            }

            orderPanel.PopulateTurnOrder(actorList, selector);
        }
        else
        {
            currFastest.SetCurrentStat(StatTypes.Speed, 0);
            CalculateFastest();
        }
    }

    public void Fastest()
    {
        int min;

        for (int i = 0; i <= actorList.Count - 2; i++)
        {
            min = i;

            for (int j = i + 1; j <= actorList.Count - 1; j++)
            {
                if(actorList[j].GetCurrentStats(StatTypes.Speed) > actorList[min].GetCurrentStats(StatTypes.Speed))
                {
                    min = j;
                }
            }

            Actor temp = actorList[i];
            actorList[i] = actorList[min];
            actorList[min] = temp;
        }

        currFastest = actorList[0];
    }

    /*
    void FindFastest()
    {
        Actor temp = actorList[0];

        for (int i = 0; i < actorList.Count; i++)              
        {
          
                if (actorList[i].GetCurrentStats(StatTypes.Speed) >= temp.GetCurrentStats(StatTypes.Speed))
                {
                    //tie goes to the actor with the fastest speed rating
                    //if that's a tie it goes to the current fastest
                    if (actorList[i].GetCurrentStats(StatTypes.Speed) == temp.GetCurrentStats(StatTypes.Speed))
                    {
                        if (actorList[i].GetCurrentStats(StatTypes.SpeedRating) <= temp.GetCurrentStats(StatTypes.SpeedRating))
                        {
                            temp = actorList[i];
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        temp = actorList[i];
                    }
                }
            
        }

        currFastest = temp;
    }
    */
}
