using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RemoveBuffSkillEffect : SkillEffect
{

    bool removetrait;
    string key, tag;
    bool removeall;

    RemoveBuffCombatNode.RemoveType removeBuff;

    public RemoveBuffSkillEffect(bool removetrait, string key, string tag, bool removeall, RemoveBuffCombatNode.RemoveType removeBuff)
    {
        this.removetrait = removetrait;
        this.key = key;
        this.tag = tag;
        this.removeall = removeall;
        this.removeBuff = removeBuff;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        if(target.HasActor())
        {
            combat.actorDamageMap.Add(new RemoveBuffCombatNode(source, target, removetrait, removeall, key, tag, removeBuff));
        }
    }

    public override SkillEffect Copy()
    {
        return new RemoveBuffSkillEffect(removetrait, key, tag, removeall, removeBuff);
    }
}
