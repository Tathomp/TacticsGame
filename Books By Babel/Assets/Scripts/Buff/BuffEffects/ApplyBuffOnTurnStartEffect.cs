using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ApplyBuffOnTurnStartEffect : BuffEffect
{
    public string buffToApply;

    public ApplyBuffOnTurnStartEffect(string buffToApply)
    {
        this.buffToApply = buffToApply;
    }

    public override void OnStartTurn(ActorData actor)
    {
        if(ConditionsMet(Globals.GetBoardManager().spawner.GetActor(actor), null, null))
        {
            actor.buffContainer.ApplyBuff(actor, actor, Globals.campaign.contentLibrary.buffDatabase.GetCopy(buffToApply));
        }
    }

    public override BuffEffect Copy()
    {
        ApplyBuffOnTurnStartEffect b = new ApplyBuffOnTurnStartEffect(buffToApply);

        CopyConditionals(b);

        return b;

    }

    public override string PrintNameOfEffect()
    {
        return "Apply Buff";

    }
}
