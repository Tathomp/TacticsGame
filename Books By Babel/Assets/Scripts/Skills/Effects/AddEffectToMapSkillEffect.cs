using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AddEffectToMapSkillEffect : SkillEffect
{
    public Event eventToAdd;

    public AddEffectToMapSkillEffect(Event eventToAdd)
    {
        this.eventToAdd = eventToAdd;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        combat.actorDamageMap.Add( new AddEventCombatNode(source, target, (eventToAdd.Copy() as Event)));
    }

    public override SkillEffect Copy()
    {
        return new AddEffectToMapSkillEffect(eventToAdd.Copy() as Event);
    }
}


