using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockMoveBuffEffect : BuffEffect
{
    public override void OnStartTurn(ActorData actor)
    {
        actor.blockMove = true;
    }

    public override BuffEffect Copy()
    {
        return new BlockMoveBuffEffect();
    }

    public override string PrintNameOfEffect()
    {
        return "Block Move";

    }
}
