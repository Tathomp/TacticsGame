using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill : DatabaseEntry, IUseable
{
    public string skillName, descript, sfxkey;
    public string iconKey;
    public int maxRange, minRange, cooldown;
    public bool UseWepon;

    public ITargetable targetType;
    public NextSkillNode nextSkill;     //next skill to use
    public TargetFiltering skillFilter; //filters out targets meeting the parameters

    public List<string> tags;
    public List<string> animControllerID;

    public List<SkillEffect> effects;
    public List<SkillCost> skillCost;

    //public RandomRollConditional hit_chance;
    public List<Conditional> conditionalsRequired;
    // we probably want to wrap firing the tags and the effects up in the same effect so that we can make
    // sure we only fire the effects and tags when the condition is met and we're not checking the conditions multiple
    // times which could be an issue with the hit chance conditional
    //

    public Skill(string key, int min, int max, string iconKey = "unkown", bool useWep = false, string sfxkey = "explosion") : base(key)
    {
        maxRange = max;
        minRange = min;
        cooldown = 2;
        UseWepon = useWep;
        this.iconKey = iconKey;
        this.sfxkey = sfxkey;
        this.animControllerID = new List<string>();

        tags = new List<string>();
        effects = new List<SkillEffect>();
        skillCost = new List<SkillCost>();
        //hit_chance = new RandomRollConditional(100);
        conditionalsRequired = new List<Conditional>();

        //default will be to have skills cost one action
        skillCost.Add(new SkillCostStat(StatTypes.NumberOfActions, 1, true));

        descript = "No description added right now";
    }


    public List<TileNode> GetTargetedTiles(Actor source, TileNode center)
    {
        List<TileNode> nodes = targetType.TargetTiles(source, center);

        if (skillFilter != null)
        { 
            skillFilter.FilterTargetNodes(nodes, source, center);
        }

        return nodes;        
    }

    public List<TileNode> GetFinalTargetedTiles(Actor source, TileNode center)
    {
        List<TileNode> nodes = targetType.FinalTargetTiles(source, center);

        if (skillFilter != null)
        {
            skillFilter.FilterTargetNodes(nodes, source, center);
        }

        return nodes;
    }

    public void ProcessEffects(Combat combat, Actor source, TileNode center)
    {
        foreach (Conditional conditional in conditionalsRequired)
        {
            if(conditional.ConditionMet(source, center, this) == false)
            {
                return;
            }
        }

        //all the conditions were met

        foreach (SkillEffect e in effects)
        {
            e.ActorEffect(combat, source, center);
        }
    }

    public void PayCosts(Actor actor)
    {
        actor.actorData.buffContainer.ApplySkillCostAdjustments(actor, this);


        foreach (SkillCost cost in skillCost)
        {
            cost.PayCost(actor);
        }
    }

    public bool CanPayCost(Actor actor)
    {
        //cycle through cost effects
        //check if the cost would leave a particular stat below zero
        //if so, return false

        actor.actorData.buffContainer.ApplySkillCostAdjustments(actor, this);

        foreach (SkillCost cost in skillCost)
        {
            if(cost.CanPayCost(actor) == false)
            {
                return false;
            }
        }


        return true;
    }

    public int GetMaxRange(Actor data)
    {
        if(UseWepon)
        {
            return data.GetMaxAttackRange();
        }
        
        return maxRange;
    }

    public int GetMinRange(Actor data)
    {
        if (UseWepon)
        {
            return data.GetMinAttackRange();
        }

        return minRange;
    }

    public override DatabaseEntry Copy()
    {
         Skill temp = new Skill(key, minRange, maxRange, iconKey);


        temp.skillName = skillName;
        temp.maxRange = maxRange;
        temp.minRange = minRange;
        temp.UseWepon = UseWepon;
        temp.targetType = targetType;
        temp.tags = tags;
        temp.descript = descript;
        temp.animControllerID = animControllerID;
        temp.sfxkey = sfxkey;
        if(skillFilter != null)
        temp.skillFilter = skillFilter.Copy();

        temp.cooldown = cooldown;

        if(nextSkill != null)
        temp.nextSkill = nextSkill.Copy();

        foreach (SkillEffect e in effects)
        {
            temp.effects.Add((SkillEffect) e.Copy());
        }

        temp.skillCost = new List<SkillCost>();

        foreach (SkillCost cost in skillCost)
        {
            temp.skillCost.Add(cost.Copy());
        }


        foreach (Conditional conditional in conditionalsRequired)
        {
            temp.conditionalsRequired.Add(conditional.Copy());
        }


        return temp;
    }


    public void ProcessTags(Actor source, List<TileNode> center)
    {
        foreach (TileNode tile in center)
        {
            tile.ProccessTags(tags);
            tile.ProcessEffectQueue();
        }
    }



    public string GetHotbarDescription()
    {
        string s = skillName + "\n";

        s += "Cooldown: " + cooldown + "\n";

        foreach (SkillCost item in skillCost)
        {
            s += item.PrintCost() + "\n";
        }

        s += "\n";

        s += descript;

        return s;
    }

    public string GetName()
    {
        return skillName;
    }

    public List<string> GetTags()
    {
        return tags;
    }

    public List<string> GetAnimControllerID()
    {
        return animControllerID;
    }

    public string GetIconFilePath()
    {
        return iconKey;
    }

    public TargetFiltering GetTargetFiltering()
    {
        return skillFilter;
    }

    public string GetSFXKey()
    {
        return sfxkey;
    }
    
    public bool FilterTileNode(Actor source, TileNode center)
{
        if (skillFilter == null)
        {
            return skillFilter.ShouldNodeBeFiltered(center,
                source.ActorsController().PlayerControlled(),
                source);
        }

        return false;
    }

    public ITargetable GetTargetType()
    {
        return targetType;
    }

    public int GetCoolDown()
    {
        return cooldown;
    }
}
