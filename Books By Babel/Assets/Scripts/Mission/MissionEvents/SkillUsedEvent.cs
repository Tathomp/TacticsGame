using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillUsedEvent : Event
{
    public ActorData source;
    private string skillKey;
    private MapCoords target;
    private ActorData targetActor;

    public EffectToAddType addType;

    public SkillUsedEvent(string id, string skillKey, EffectToAddType addType) : base(id)
    {
        this.skillKey = skillKey;
        this.addType = addType;



    }

    public override DatabaseEntry Copy()
    {
        SkillUsedEvent e = new SkillUsedEvent(key, skillKey, addType);
        CopyFlags(e);
        e.publicEvent = publicEvent;
        return e;
    }

    public override void FireEvent()
    {
        Skill s = Globals.campaign.contentLibrary.skillDatabase.GetCopy(skillKey);


        if (addType == EffectToAddType.TargetActor)
        {
            //Currently, there's nothing stopping us for using the skill on a tile that is empty and then trying to use this 
            //bit of code to cast the skill that was on an empty tile
            //maybe skills that are supposed to follow an actor should just be casted on the target tile instead if 
            //no actor is found
            TileNode n = Globals.GetBoardManager().pathfinding.GetTileNode(targetActor.gridPosX, targetActor.gridPosY);
            BuiltCombat(s, n);

        }
        else
        {
            //fire it at the selected Tile
            BuiltCombat(s, Globals.GetBoardManager().pathfinding.GetTileNode(target));
        }
    }

    //THis whole things is a fucking mess sry future me
    public Combat BuiltCombat(Skill s, TileNode target)
    {
       Combat c = new Combat(Globals.GetBoardManager().spawner.GetActor(source));
       c.source = Globals.GetBoardManager().spawner.GetActor(source);
        TileNode sourceNode = Globals.GetBoardManager().pathfinding.GetTileNode(c.source);
        s.ProcessEffects(c, c.source, target);
        c.animationDatas.Add(new AnimationData() { skillUsed = s, DestNode = target, sourceNode = sourceNode });

        c.UseSkill();
        return c;
    }


    public void FillOutEVent(Actor source, TileNode target)
    {

        this.target = new MapCoords(target.data.posX, target.data.posY);

        if (addType == EffectToAddType.TargetActor)
        {
            if(target.HasActor())
            {
                targetActor = target.actorOnTile.actorData;
            }
        }

        this.source = source.actorData;
    }

    public override string DisplayText()
    {
        return "Skill used event: " + skillKey;
    }
}

public enum EffectToAddType
{    
    TargetTile,
    TargetActor
}

