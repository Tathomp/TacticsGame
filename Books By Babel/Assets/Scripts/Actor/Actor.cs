using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class Actor : MonoBehaviour
{
    public ActorData actorData;

    public TurnManager turnManger;
    public Pathfinding pathfinding;


    public void InitActor(ActorData data, BoardManager boardManager)
    {
        actorData = data;
        pathfinding = boardManager.pathfinding;

        transform.position = Globals.GridToWorld(data.gridPosX, data.gridPosY);
        // GetComponent<SpriteRenderer>().sprite = Resources.Load<SpriteAtlas>("Sprites").GetSprite(data.spriteName);
        turnManger = boardManager.turnManager;
        pathfinding.MoveWithOutOnMoveBuffs(this, data.gridPosX, data.gridPosY);
    }


    #region Movement Code
    public void MoveAlongPath(List<TileNode> path)
    {
        if(path.Count == 0)
        {
            return;
        }

        int x = path[path.Count - 1].data.posX;
        int y = path[path.Count - 1].data.posY;



        pathfinding.MoveWithOnMoveBuffs(this, x, y);
        Moved();

        StartCoroutine(ProcessPath(path));


    }
    

    IEnumerator ProcessPath(List<TileNode> path)
    {

        int x = path[path.Count - 1].data.posX;
        int y = path[path.Count - 1].data.posY;        

        for (int i = 0; i < path.Count; i++)
        {
            gameObject.transform.position = Globals.GridToWorld(path[i].data.posX, path[i].data.posY);
            //StartCoroutine(SmoothMovement(Globals.GridToWorld(path[i])));
            yield return new WaitForSeconds(.35f);

        }
    }

    void ActorShake()
    {
        StartCoroutine(ActorHit());
    }

    IEnumerator ActorHit()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return StartCoroutine(DefenderShake());

        }
    }

    IEnumerator DefenderShake()
    {
        Vector3 startV3 = new Vector3(1f, 1f);
        Vector3 shrinkV3 = new Vector3(1.3f, .6f);
        Vector3 stretchv3 = new Vector3(.6f, 1.3f);
        float speedFactor = 9f;
        
        float remainingDist = (this.gameObject.transform.localScale - shrinkV3).sqrMagnitude;


        while (remainingDist > float.Epsilon)
        {
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, shrinkV3, speedFactor * Time.deltaTime);
            remainingDist = (this.gameObject.transform.localScale - shrinkV3).sqrMagnitude;

            yield return null;

        }

        remainingDist = (this.gameObject.transform.localScale - stretchv3).sqrMagnitude;

        while (remainingDist > float.Epsilon)
        {
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, stretchv3, speedFactor * Time.deltaTime);
            remainingDist = (this.gameObject.transform.localScale - stretchv3).sqrMagnitude;

            yield return null;

        }

        remainingDist = (this.gameObject.transform.localScale - startV3).sqrMagnitude;

        while (remainingDist > float.Epsilon)
        {
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, startV3, speedFactor * Time.deltaTime);
            remainingDist = (this.gameObject.transform.localScale - startV3).sqrMagnitude;

            yield return null;

        }



        //gameObject.transform.localScale = new Vector3(1, 1, 1);

    }

    IEnumerator AttackShake()
    {
        yield return null;

    }


    public IEnumerator SmoothMovement(Vector3 target)
    {
        float remainingDist = (transform.position - target).sqrMagnitude;

        while(remainingDist > float.Epsilon)
        {

            transform.position = Vector3.MoveTowards(transform.position, target, 5 * Time.deltaTime);
            remainingDist = (transform.position - target).sqrMagnitude;
            yield return null;
        }
    }
    #endregion


    #region Buff Controls
    public void ApplyBuff(ActorData source, Buff buff)
    {
        actorData.buffContainer.ApplyBuff(actorData, source,buff);
    }

    //currently not called
    public void OnAttackBuffs(Combat combat, AnimationData currentData)
    {
        actorData.buffContainer.OnAttacked(combat, currentData);
    }

    public void RemoveBuff(Buff buff)
    {
        actorData.buffContainer.RemoveBuff(actorData, buff);
    }

    /// <summary>
    /// Removes all non-trait buffs from each actor
    /// </summary>
    public void RemoveAllBuffs()
    {
        List<Buff> buffs = actorData.buffContainer.buffList;

        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            if (buffs[i].IsTrait == false)
            {
                RemoveBuff(buffs[i]);
            }
        }
    }

    #endregion



    public void EquipItem(Item item)
    {
        //maybe this is where we put the safety check

        actorData.equipment.EquipItem(item, actorData);

    }


    public void Attack()
    {
        Attacked();
    }

    
    public void FinishAnim()
    {
        StartCoroutine(FinishUpANimation());
    }


    IEnumerator FinishUpANimation()
    {
        yield return new WaitForSeconds(.5f);
    }


    public void ChangeHealth(int dmg, ActorData source = null)
    {
        if(!actorData.isAlive)
        {
            return;
        }

        if(dmg < 0)
        {
            ActorShake();
        }

        SetCurrentStat(StatTypes.Health, GetCurrentStats(StatTypes.Health) + dmg);

        //StartCoroutine(DefenderShake());


        if(GetCurrentStats(StatTypes.Health) <= 0)
        {
            //run buffs here
            if(actorData.isDying == false)
            {
                actorData.isDying = true;
                KillActor();
                actorData.buffContainer.OnDeath(actorData, source);

            }

            actorData.isDying = false;

        }
        else if(GetCurrentStats(StatTypes.Health) > GetMaxStats(StatTypes.Health))
        {
            SetCurrentStat(StatTypes.Health, GetMaxStats(StatTypes.Health));
        }
    }


    public void KillActor()
    {
        //turnManger.actorList.Remove(this);
        
        actorData.isAlive = false;

        gameObject.transform.Rotate(0, 0, -90);
        SetCurrentStat(StatTypes.Health, 0);

        // Destroy(gameObject);
        // Destroy(this);

        GetComponent<Animator>().enabled = false;
    }


    public void DestoryActor()
    {
        pathfinding.GetTileNode(this).actorOnTile = null;

        Destroy(gameObject);
        Destroy(this);
    }

    public void ReviveActor(int h)
    {
        turnManger.actorList.Add(this);
        actorData.isAlive = true;
        gameObject.transform.Rotate(0, 0, 90);

        SetCurrentStat(StatTypes.Health, h);
    }


    public void StartTurn()
    {
        actorData.currentStatCollection.statDict[StatTypes.NumberOfActions] =
            actorData.maxStatCollection.statDict[StatTypes.NumberOfActions];

        actorData.currentStatCollection.statDict[StatTypes.NumberOfMovements] =
            actorData.maxStatCollection.statDict[StatTypes.NumberOfMovements];


        actorData.buffContainer.OnStartTurn(actorData);
        actorData.cooldownMap.DecrementAllCoolDowns();

    }


    public bool CanMove()
    {
        return actorData.currentStatCollection.GetValue(StatTypes.NumberOfMovements) > 0 & actorData.blockAttack == false;
    }


    public bool CanAttack()
    {
        return actorData.currentStatCollection.GetValue(StatTypes.NumberOfActions) > 0 & actorData.blockMove == false;
    }


    void Moved()
    {
        actorData.currentStatCollection.ChangeStat(StatTypes.NumberOfMovements, -1);
    }


    void Attacked()
    {
       // actorData.currentStatCollection.statDict[StatTypes.NumberOfActions]--;
            //actorData.currentStatCollection.GetValue(StatTypes.NumberOfActions)
    }


    public void Wait()
    {
        if(CanMove() && CanAttack())
        {
            ReduceSpeed(50);
        }
        else if(CanMove() != CanAttack())
        {
            ReduceSpeed(75);
        }
        else
        {
            ReduceSpeed(100);
        }


    }


    public Equipment GetEquipment()
    {
        return actorData.equipment;
    }


    public int GetMaxAttackRange()
    {
        //return actorData.equipment.GetPrimaryWeapon().maxRange;
        return actorData.currentStatCollection.GetValue(StatTypes.MaxRange);
    }

    public int GetMinAttackRange()
    {
        return actorData.currentStatCollection.GetValue(StatTypes.MinRange);
    }


    //give tiles stat containers
    //rework this shit

    public void AddStats(StatsContainer sc)
    {
        actorData.currentStatCollection.AddStats(sc);
        actorData.maxStatCollection.AddStats(sc);

    }

    public void ReduceStats(StatsContainer sc)
    {
        actorData.currentStatCollection.RemoveStats(sc);
        actorData.maxStatCollection.RemoveStats(sc);
    }


    #region Accessors/Mutators
    public void ReduceSpeed(int x)
    {
        UpdateCurrentStat(StatTypes.Speed, -x);
    }


    public ActorController ActorsController()
    {
        return actorData.controller;
    }

    public int GetCurrentStats(StatTypes key)
    {
        return actorData.currentStatCollection.statDict[key];
    }


    public int GetMaxStats(StatTypes key)
    {
        return actorData.maxStatCollection.statDict[key];
    }

    public void SetMaxStat(StatTypes key, int value)
    {
        actorData.maxStatCollection.statDict[key] = value;
    }

    public void SetCurrentStat(StatTypes key, int value)
    {
        actorData.currentStatCollection.statDict[key] = value;
    }

    public void UpdateCurrentStat(StatTypes key, int value)
    {
        SetCurrentStat(key, GetCurrentStats(key) + value);
    }

    public void UpdateMaxStats(StatTypes key, int value)
    {
        SetMaxStat(key, GetMaxStats(key) + value);
    }

    public void UpdateSpeed()
    {
        UpdateCurrentStat(StatTypes.Speed, GetCurrentStats(StatTypes.SpeedRating));
    }

    public int GetPosX()
    {
        return actorData.gridPosX;
    }

    public int GetPosY()
    {
        return actorData.gridPosY;
    }

    public void SetPosX(int x)
    {
        actorData.gridPosX = x;
    }

    public void SetPosY(int y)
    {
        actorData.gridPosY = y;
    }
    #endregion
}
