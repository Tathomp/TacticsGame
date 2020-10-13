using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffContainer
{
    public List<Buff> buffList;

    public BuffContainer()
    {
        buffList = new List<Buff>();
    }


    public bool CanBuffBeApplied(Buff key)
    {
        int count = 0;

        foreach (Buff buff in buffList)
        {
            if(buff.GetKey() == key.GetKey())
            {
                count++;
            }
        }

        return count < key.maxStacks;
    }

    public void ApplySkillCostAdjustments(Actor source, Skill skill)
    {
        foreach (Buff buff in buffList)
        {
            buff.SkillCostAdjustments(source, skill);
        }
    }


    public void ApplyBuff(ActorData actor, ActorData source, Buff buff)
    {

        int indexOfFirstInstance = -1;
        int currStack = 0;

        Debug.Log(buff.buffName + " applied");

        for (int i = 0; i < buffList.Count; i++)
        {
            if(buffList[i].GetKey() == buff.GetKey())
            {
                currStack++;

                if(indexOfFirstInstance < 0)
                {
                    indexOfFirstInstance = i;
                }
            }
        }

        if(currStack >= buff.maxStacks)
        {
            RemoveBuff(actor, buffList[indexOfFirstInstance]);
        }

        buff.ApplyEffects(actor, source);
        buffList.Add(buff);

    }

    //Do we mean the passive talents?
    //Investigate further
    //
    public void LearnPassiveBuff(ActorData data, Buff buff)
    {
        //buff.IsTrait = true;
        ApplyBuff(data, null, buff);
    }

    public void RemoveBuff(ActorData actor, Buff buffToRemove)
    {
        if(buffList.Remove(buffToRemove))
        {

            buffToRemove.RemoveBuff(actor);

            Debug.Log(buffToRemove.buffName + " removed.");
        }



    }

    public void OnActorSpawn(ActorData actor)
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            buffList[i].OnSpawn(actor);
        }
    }

    public void OnTargeted(Combat combat, AnimationData currentData)
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            buffList[i].OnTargetedBuffs(combat, currentData);
        }
    }


public void RemoveBuff(ActorData actor, string key)
    {
        Buff b = null;

        for (int i = 0; i < buffList.Count; i++)
        {
            if(buffList[i].GetKey() == key)
            {
                //we found the buff we're removing
                b = buffList[i];
                break;
            }
        }

        if(b == null)
        {
            Debug.LogWarning("Buff that should've been removed was not founded");
            return;
        }

        RemoveBuff(actor, b);
    }


    public void OnStartTurn(ActorData actor)
    {
        int count = buffList.Count - 1;

        for (int i = count; i >= 0; i--)
        {
            buffList[i].OnStartTurn(actor);
        }        
    }


    public void OnAttacked(Combat combat, AnimationData currentData)
    {
        foreach (Buff b in buffList)
        {
            b.OnAttacked(combat, currentData);
        }
    }


    public void OnMove(ActorData data, TileNode curr, TileNode dest)
    {
        for (int i = 0; i < data.buffContainer.buffList.Count; i++)
        {
            data.buffContainer.buffList[i].OnMove(data, curr, dest);
        }
    }

    public void OnDeath(ActorData dying, ActorData killer)
    {
        foreach (Buff buff in buffList)
        {
            buff.OnDeath(dying, killer);
        }
    }


    public BuffContainer Copy()
    {
        BuffContainer bc = new BuffContainer();

        foreach (Buff b in buffList)
        {
            bc.buffList.Add(b.Copy() as Buff);
        }

        return bc;
    }
}
