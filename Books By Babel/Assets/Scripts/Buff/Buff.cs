using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff : DatabaseEntry, IDisplayInfo
{
    public string ID;

    public List<BuffEffect> effects;
    public string buffName, description;
    public List<string> tags;

    public int turnDuration;
    public bool tempBuff;

    public bool IsTrait;
    public bool IsBuff; //everything is either a buff or a debuff
    public int maxStacks; //everything is either a buff or a debuff

    public string iconKey;

    //stackable buff
    // if it's not stackable then refresh the buff

    public Buff(string key, string iconName= "unkown", bool IsBuff = true, bool tempBuff = false, bool IsTrait = false) : base(key)
    {
        this.tempBuff = tempBuff;
        this.IsTrait = IsTrait;
        this.IsBuff = IsBuff;

        buffName = "buff_name";
        description = "pla";
        this.maxStacks = 1;

        effects = new List<BuffEffect>();
        iconKey = iconName;
        tags = new List<string>();
    }


    public void ApplyEffects(ActorData actor, ActorData source)
    {
        foreach (BuffEffect e in effects)
        {
            e.OnApply(actor, source);
        }
    }


    public void RemoveBuff(ActorData actor)
    {
        foreach (BuffEffect effect in effects)
        {
            effect.OnRemove(actor);
        }
    }

    public void OnSpawn(ActorData actor)
    {
        foreach (BuffEffect effect in effects)
        {
            effect.OnActorSpawn(actor);
        }
    }

    public void OnAttacked(Combat combat, AnimationData currentData)
    {
        foreach (BuffEffect e in effects)
        {
            e.OnDamageInflicted(combat, currentData);
        }
    }

    public void OnTargetedBuffs(Combat combat, AnimationData currentData)
    {
        foreach (BuffEffect effect in effects)
        {
            effect.OnTargeted(combat, currentData);
        }
    }

    public void OnDeath(ActorData actor, ActorData killer)
    {
        foreach (BuffEffect e in effects)
        {
            e.OnDeath(actor, killer);
        }
    }

    public void SkillCostAdjustments(Actor source, Skill skill)
    {
        foreach (BuffEffect e in effects)
        {
            e.ModSkillCost(source, skill);
        }
    }



    public void OnStartTurn(ActorData actor)
    {
        if(tempBuff)
        {
            turnDuration--;

            if(turnDuration < 0)
            {
                Globals.GetBoardManager().spawner.ActorDataGameObjectMap[actor].RemoveBuff(this);
            }
        }

        foreach (BuffEffect e in effects)
        {
            e.OnStartTurn(actor);
        }
    }

    public void OnMove(ActorData data, TileNode start, TileNode dest)
    {
        foreach (BuffEffect effect in effects)
        {
            effect.OnMove(data, start, dest);
        }
    }

    public override DatabaseEntry Copy()
    {
        Buff b = new Buff(key, iconKey);
        b.effects = new List<BuffEffect>();

        foreach (BuffEffect e in effects)
        {
            b.effects.Add((BuffEffect) e.Copy());
        }

        b.turnDuration = turnDuration;
        b.tempBuff = tempBuff;
        b.buffName = buffName;
        b.iconKey = iconKey;
        b.IsBuff = IsBuff;
        b.maxStacks = maxStacks;
        b.description = description;

        foreach (string s in tags)
        {
            b.tags.Add(s);
        }

        return b;
    }

    public string GetHotbarDescription()
    {
        string s = buffName + "\n";

        foreach (BuffEffect b in effects)
        {
          s += b.GetHotbarDescription();
        }


        return s;
    }

    public string GetName()
    {
        return buffName;
    }

    public string GetIconFilePath()
    {
        return iconKey;
    }
}
