using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActorBuffMapEnchantmentEffect : ActorMapEnchantmentEffect
{

    public string buffKeyToAdd;

    public ActorBuffMapEnchantmentEffect(string buffKeyToAdd)
    {
        this.buffKeyToAdd = buffKeyToAdd;
    }

    public override void Apply(Actor actor)
    {
        Buff b = Globals.campaign.contentLibrary.buffDatabase.GetCopy(buffKeyToAdd);

        actor.actorData.buffContainer.ApplyBuff(actor.actorData, null, b);
    }

    public override void Remove(Actor actor)
    {
        actor.actorData.buffContainer.RemoveBuff(actor.actorData, buffKeyToAdd);
    }

    public override ActorMapEnchantmentEffect Copy()
    {
        return new ActorBuffMapEnchantmentEffect(buffKeyToAdd);
    }
}
