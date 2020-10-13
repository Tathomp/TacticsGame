using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AddInteractionSkillEffect : SkillEffect
{
    public Interaction interactionToAdd;

    public AddInteractionSkillEffect(Interaction interactionToAdd)
    {
        this.interactionToAdd = interactionToAdd;
    }

    // we probably have to change the interation dictionary to a 2d array that represents the battlefield
    //    to make things easier to keep track of idk

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        //maybe this is where we make sure there isn't a interaction already here
        //s
        combat.actorDamageMap.Add(new AddInteractionCombatNode(target.data.posX, target.data.posY, interactionToAdd.Copy(), source, target));
    }

    public override SkillEffect Copy()
    {
        return new AddInteractionSkillEffect(interactionToAdd.Copy());
    }
}
