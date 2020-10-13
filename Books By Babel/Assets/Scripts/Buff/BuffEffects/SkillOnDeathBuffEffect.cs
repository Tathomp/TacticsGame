using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillOnDeathBuffEffect : BuffEffect
{
    public bool useOnKiller;
    public string skillID;

    public SkillOnDeathBuffEffect(bool use, string key)
    {
        this.useOnKiller = use;
        this.skillID = key;
    }


    public override void OnDeath(ActorData actor, ActorData kiler)
    {
        Skill s = Globals.campaign.contentLibrary.skillDatabase.GetCopy(skillID);
        TileNode target, source;
        int x, y;

        if(useOnKiller)
        {
           if(kiler == null)
           {
                return;
           }

            x = kiler.gridPosX;
            y = kiler.gridPosY;

        }
        else
        {
            x = actor.gridPosX;
            y = actor.gridPosY;
        }

        target = Globals.GetBoardManager().pathfinding.GetTileNode(x, y);
        source = Globals.GetBoardManager().pathfinding.GetTileNode(actor.gridPosX, actor.gridPosY);

        Combat c = new Combat(Globals.GetBoardManager().spawner.GetActor(actor), s, target, source);
        c.UseSkill();

    }


    public override string PrintNameOfEffect()
    {
        return "Cast on Death";

    }

    public override BuffEffect Copy()
    {
        return new SkillOnDeathBuffEffect(useOnKiller, skillID);
    }
}
