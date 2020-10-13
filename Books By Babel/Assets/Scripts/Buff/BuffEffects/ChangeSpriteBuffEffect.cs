using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChangeSpriteBuffEffect : BuffEffect
{
    public string newAnimationController;
    public string prevAnimationController;

    public string newPortraitpath;
    public string prevPortraitpath;

    public ChangeSpriteBuffEffect()
    {
        newAnimationController = "";
        prevAnimationController = "";

        newPortraitpath = "";
        prevPortraitpath = "";
    }


    public override BuffEffect Copy()
    {
        ChangeSpriteBuffEffect effect = new ChangeSpriteBuffEffect();

        effect.newAnimationController = newAnimationController;
        effect.prevAnimationController = prevAnimationController;

        effect.newPortraitpath = newPortraitpath;
        effect.prevPortraitpath = prevPortraitpath;


        return effect;
    }

    public override string GetHotbarDescription()
    {
        throw new System.NotImplementedException();
    }

    public override void OnApply(ActorData actor, ActorData source)
    {
        if(Globals.currState == GameState.Combat)
        {
            Actor temp = Globals.GetBoardManager().spawner.GetActor(actor);

            temp.GetComponent<Animator>().runtimeAnimatorController = Globals.GEtAnatimationController(newAnimationController);

        }


        if(newAnimationController != "")
        {
            prevAnimationController = actor.animationController;
            actor.animationController = newAnimationController;
        }


        if (newPortraitpath != "")
        {
            prevPortraitpath = actor.portraitFilePath;
            actor.portraitFilePath = newPortraitpath;
        }

    }

    public override void OnRemove(ActorData actor)
    {
        actor.animationController = prevAnimationController;


        if (Globals.currState == GameState.Combat)
        {
            Actor temp = Globals.GetBoardManager().spawner.GetActor(actor);
            temp.GetComponent<Animator>().runtimeAnimatorController = Globals.GEtAnatimationController(actor.animationController);
        }


        if (prevAnimationController != "")
        {
            actor.animationController = prevAnimationController;
        }


        if (prevPortraitpath != "")
        {
            actor.portraitFilePath = prevPortraitpath;
        }

    }

    public override string PrintNameOfEffect()
    {
        return "Change Sprite";

    }
}
