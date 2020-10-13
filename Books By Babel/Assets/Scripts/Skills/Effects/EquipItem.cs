using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipItemSkillEffect : SkillEffect
{

    public string itemKey;
    public bool tempItem;

    public EquipItemSkillEffect(string itemKey, bool tempItem)
    {
        this.itemKey = itemKey;
        this.tempItem = tempItem;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        if(target.HasActor())
        {
            Item i = Globals.campaign.GetItemCopy(itemKey);
            i.DisappearsInventory = tempItem;
            target.actorOnTile.EquipItem(i);
        }
    }

    public override SkillEffect Copy()
    {
        return new EquipItemSkillEffect(itemKey, tempItem);
    }

}
