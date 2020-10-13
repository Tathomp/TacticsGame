using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat
{
    public Actor source;

    public List <CombatNode> actorDamageMap;
    //public Dictionary<Actor, List<CombatNode>> reactionMap;

    public List<AnimationData> animationDatas;
    public Queue<AnimationData> animationQueue;

    //public List<TileNode> targets;

    public TileNode center;

	public Combat(Actor source)
    {
        this.source = source;
        
        actorDamageMap = new List<CombatNode>();
        animationDatas = new List<AnimationData>();
        animationQueue = new Queue<AnimationData>();
    }

    public Combat(Actor source, Skill skill, TileNode center, TileNode startNode)
    {
        this.source = source;
        this.center = center;

        //targets = skill.GetFinalTargetedTiles(source, center);
        animationDatas = new List<AnimationData>();

        actorDamageMap = new List<CombatNode>();

        foreach (string item in skill.animControllerID)
        {
            animationDatas.Add(AnimationData.NewAntionData(skill, startNode, center));

        }
    }


    public void UseSkill()
    {


        animationQueue = new Queue<AnimationData>();
        foreach (AnimationData useable in animationDatas)
        {

            animationQueue.Enqueue(useable);
        }


        while(animationQueue.Count > 0)
        {
            AnimationData useable = animationQueue.Dequeue();

            PoopulateCombat(useable, true);

            if (useable.skillUsed is ConsumableItem)
            {
                actorDamageMap.Add(new ConsumeableCombatNode(source, useable.DestNode, (ConsumableItem)useable.skillUsed));
            }

            foreach (CombatNode cn in actorDamageMap)
            {
                cn.ApplyEffect();
            }


            useable.skillUsed.ProcessTags(source, useable.targets);
        }

        // trigger any reaction effects

        // somethings should cal the PayCostsCDAttack thing after this is called

    }

    public void PayCostsCDAttack()
    {
        animationDatas[0].skillUsed.PayCosts(source);
        source.actorData.cooldownMap.AddSKillToCooldown(animationDatas[0].skillUsed);
        source.Attack();
    }

    public List<CombatNode> DisplayCombatPreview(AnimationData useable)
    {
        // We're making a new combat obj 
        Combat c = new Combat(source);
        c.PoopulateCombat(useable);

        return c.actorDamageMap;
    }


    public void PoopulateCombat(AnimationData skillUsed, bool final = false)
    {
       // Skill te = Globals.campaign.contentLibrary.skillDatabase.GetCopy(skillUsed.skillUsed.GetKey()); //lolwut
       // skillUsed.skillUsed = te;
        source.OnAttackBuffs(this, skillUsed);

        if(skillUsed.DestNode.HasActor())
        {
            skillUsed.DestNode.actorOnTile.actorData.buffContainer.OnTargeted(this, skillUsed);
        }

        if(final == false)
        {
            skillUsed.targets = skillUsed.skillUsed.GetTargetedTiles(source, skillUsed.DestNode);

        }
        else
        {
            skillUsed.targets = skillUsed.skillUsed.GetFinalTargetedTiles(source, skillUsed.DestNode);

        }



        foreach (TileNode tile in skillUsed.targets)
        {

            skillUsed.skillUsed.ProcessEffects(this, source, tile);

        }


    }
    

}

public class CombatData
{
    public Skill skillUsed;
    public Actor source;
    public List<CombatNode> nodesGenerated;

    public CombatData()
    {
        nodesGenerated = new List<CombatNode>();
    }
}


public class AnimationData
{
    public IUseable skillUsed;
    public TileNode sourceNode, DestNode;
    public List<TileNode> targets;

    public static AnimationData NewAntionData(IUseable skill, TileNode source, TileNode dest)
    {
        AnimationData data = new AnimationData();
        data.skillUsed = skill;
        data.sourceNode = source;
        data.DestNode = dest;
        if(source.HasActor())
        data.targets = data.skillUsed.GetTargetedTiles(source.actorOnTile, data.DestNode); //this might not always have an actor i guess we'll see
        return data;
    }
}



