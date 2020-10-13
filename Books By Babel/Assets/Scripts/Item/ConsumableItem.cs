using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// PROBABLY SAFE TO DELETE
/// </summary>
[System.Serializable]
public class ConsumableItem : IHotbar, IUseable
{
    public Skill consumeableEffect;
    public List<string> animControllerID;
    public string itemParentKey; //item key
    public int cooldown;

    public ConsumableItem(Skill key, string itemParent)
    {
        consumeableEffect = key;
        this.itemParentKey = itemParent;
        this.animControllerID = new List<string>();

        cooldown = 1;
    }

    public  ConsumableItem Copy()
    {
        ConsumableItem i = new ConsumableItem(consumeableEffect, itemParentKey);
        i.consumeableEffect = consumeableEffect.Copy() as Skill;

        i.animControllerID = animControllerID;

        return i;
    }

    public string GetHotbarDescription()
    {
        return consumeableEffect.descript;
    }

    public int GetMaxRange(Actor data)
    {
        return consumeableEffect.GetMaxRange(data);
    }

    public int GetMinRange(Actor data)
    {
        return consumeableEffect.GetMinRange(data);
    }

    public string GetName()
    {
        string s = Globals.campaign.GetItemCopy(itemParentKey).Name + ": " + consumeableEffect.skillName;

        return s;
    }

    public List<string> GetTags()
    {
        return consumeableEffect.tags;
    }

    public List<TileNode> GetTargetedTiles(Actor source, TileNode center)
    {
        return consumeableEffect.GetTargetedTiles(source, center);
    }

    public List<TileNode> GetFinalTargetedTiles(Actor source, TileNode center)
    {
        return consumeableEffect.GetTargetedTiles(source, center);
    }


    public void ProcessEffects(Combat c, Actor source, TileNode tile)
    {
        consumeableEffect.ProcessEffects(c, source, tile);
    }

    public void ProcessTags(Actor source, List<TileNode> center)
    {
        consumeableEffect.ProcessTags(source, center);
    }
    /*
    public void Use(Combat combat)
    {
        consumeableEffect.Use(combat);
    }*/

    public List<string> GetAnimControllerID()
    {
        return animControllerID;
    }

    public string GetIconFilePath()
    {
        throw new System.NotImplementedException();
    }

    public TargetFiltering GetTargetFiltering()
    {
       return consumeableEffect.GetTargetFiltering();
    }

    public string GetSFXKey()
    {
        return ((IUseable)consumeableEffect).GetSFXKey();
    }

    public bool FilterTileNode(Actor source, TileNode center)
    {
        return ((IUseable)consumeableEffect).FilterTileNode(source, center);
    }

    public void PayCosts(Actor actor)
    {
        ((IUseable)consumeableEffect).PayCosts(actor);
    }

    public bool CanPayCost(Actor actor)
    {
        return ((IUseable)consumeableEffect).CanPayCost(actor);
    }

    public ITargetable GetTargetType()
    {
        return ((IUseable)consumeableEffect).GetTargetType();
    }

    public string GetKey()
    {
        return ((IUseable)consumeableEffect).GetKey();
    }

    public int GetCoolDown()
    {
        return cooldown;
    }
}
