using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConjureItemInventorySkillEffect : SkillEffect
{
    private string item_key;


    public ConjureItemInventorySkillEffect(string item_key)
    {
        this.item_key = item_key;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        if(target.HasActor())
        {
            combat.actorDamageMap.Add(new ConjureItemInventoryCombatNode(source, target, item_key));
        }
    }

    public override SkillEffect Copy()
    {
        return new ConjureItemInventorySkillEffect(item_key);
    }
}
