using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockAttackEffect : BuffEffect
{
    public override void OnStartTurn(ActorData actor)
    {
        actor.blockAttack = true;
    }

    public override BuffEffect Copy()
    {
        return new BlockAttackEffect();
    }

    public override string PrintNameOfEffect()
    {
        return "Block Attack";

    }
}
