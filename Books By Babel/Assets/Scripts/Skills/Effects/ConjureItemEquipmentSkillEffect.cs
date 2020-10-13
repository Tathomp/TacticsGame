using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConjureItemEquipmentSkillEffect : SkillEffect
{
    private string itemKEy;

    public ConjureItemEquipmentSkillEffect(string itemKEy)
    {
        this.itemKEy = itemKEy;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        if(target.HasActor())
        {
            target.actorOnTile.EquipItem(Globals.campaign.GetItemCopy(itemKEy));
        }
    }

    public override SkillEffect Copy()
    {
        return new ConjureItemEquipmentSkillEffect(itemKEy);
    }
}
