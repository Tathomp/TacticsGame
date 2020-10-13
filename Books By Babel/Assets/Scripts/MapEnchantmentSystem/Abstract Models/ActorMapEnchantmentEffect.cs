using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ActorMapEnchantmentEffect
{
    public abstract void Apply(Actor actor);
    public abstract void Remove(Actor actor);
    public abstract ActorMapEnchantmentEffect Copy();
}
