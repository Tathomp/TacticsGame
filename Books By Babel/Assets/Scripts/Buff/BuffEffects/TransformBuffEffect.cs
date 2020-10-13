using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransformBuffEffect : BuffEffect
{
    public SwitchJobsEffect primary, secondary, race;
    public ChangeSpriteBuffEffect newSprite;
    public StanceBuffEffect stanceEffect;


    public override BuffEffect Copy()
    {
        TransformBuffEffect e = new TransformBuffEffect();

        if(primary != null)
        {
            e.primary = (SwitchJobsEffect) primary.Copy();
        }

        if (secondary != null)
        {
            e.secondary = (SwitchJobsEffect)secondary.Copy();
        }


        if (newSprite != null)
        {
            e.newSprite = (ChangeSpriteBuffEffect)newSprite.Copy();
        }


        if (race != null)
        {
            e.race = (SwitchJobsEffect)race.Copy();
        }


        if (stanceEffect != null)
        {
            e.stanceEffect = (StanceBuffEffect)stanceEffect.Copy();
        }


        return e;
    }


    public override void OnApply(ActorData actor, ActorData source)
    {
        if (primary != null)
        {
            primary.OnApply(actor, source);
        }

        if (secondary != null)
        {
            secondary.OnApply(actor, source);
        }


        if (newSprite != null)
        {
            newSprite.OnApply(actor, source);
        }

        if (race != null)
        {
            race.OnApply(actor, source);
        }

        if (stanceEffect != null)
        {
            stanceEffect.OnApply(actor, source);
        }
    }


    public override void OnDeath(ActorData actor, ActorData killer)
    {
        if (primary != null)
        {
            primary.OnDeath(actor, killer);
        }

        if (secondary != null)
        {
            secondary.OnDeath(actor, killer);
        }


        if (newSprite != null)
        {
            newSprite.OnDeath(actor, killer);
        }

        if (race != null)
        {
            race.OnDeath(actor, killer);
        }

        if (stanceEffect != null)
        {
            stanceEffect.OnDeath(actor, killer);
        }
    }


    public override void OnRemove(ActorData actor)
    {
        if (primary != null)
        {
            primary.OnRemove(actor);
        }

        if (secondary != null)
        {
            secondary.OnRemove(actor);
        }


        if (newSprite != null)
        {
            newSprite.OnRemove(actor);
        }

        if (race != null)
        {
            race.OnRemove(actor);
        }

        if (stanceEffect != null)
        {
            stanceEffect.OnRemove(actor);
        }
    }


    public override void OnStartTurn(ActorData actor)
    {
        if (primary != null)
        {
            primary.OnStartTurn(actor);
        }

        if (secondary != null)
        {
            secondary.OnStartTurn(actor);
        }


        if (newSprite != null)
        {
            newSprite.OnStartTurn(actor);
        }

        if (race != null)
        {
            race.OnStartTurn(actor);
        }

        if (stanceEffect != null)
        {
            stanceEffect.OnStartTurn(actor);
        }
    }


    public override string PrintNameOfEffect()
    {
        return "Transform";

    }
}
