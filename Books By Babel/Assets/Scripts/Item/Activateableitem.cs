using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Activateableitem : IUseable
{
    //Should almost certianly be made into a string ID
    public Skill ActivatableSkill {get; protected set;}
    public List<string> animControllerID;
    public string iconName, parentName;
    public int cooldown;


    public Activateableitem(Skill activatableSkill, string parentName)
    {
        this. ActivatableSkill = (Skill) activatableSkill.Copy();
        this.animControllerID = new List<string>();

        this.parentName = parentName;
        cooldown = 1;
    }
    

    public Activateableitem Copy()
    {
        Activateableitem item = new Activateableitem(ActivatableSkill, parentName);
        item.animControllerID = animControllerID;
        item.cooldown = cooldown;
        return item;
    }


    public string GetHotbarDescription()
    {
        return ActivatableSkill.GetHotbarDescription();
    }


    public int GetMaxRange(Actor data)
    {
        return ActivatableSkill.GetMaxRange(data);
    }


    public int GetMinRange(Actor data)
    {
        return ActivatableSkill.GetMinRange(data);
    }


    public string GetName()
    {
        string s = Globals.campaign.GetItemCopy(parentName).Name + ": " + ActivatableSkill.skillName;

        return s;
    }


    public List<TileNode> GetTargetedTiles(Actor source, TileNode center)
    {
        return ActivatableSkill.GetTargetedTiles(source, center);
    }


    public List<TileNode> GetFinalTargetedTiles(Actor source, TileNode center)
    {
        return ActivatableSkill.GetFinalTargetedTiles(source, center);
    }


    public void ProcessEffects(Combat c, Actor source, TileNode tile)
    {
        ActivatableSkill.ProcessEffects(c, source, tile);
    }


    public void ProcessTags(Actor source, List<TileNode> center)
    {
        ActivatableSkill.ProcessTags(source, center);
    }


    /*public void Use(Combat combat)
    {
        ActivatableSkill.Use(combat);
    }*/

    public List<string> GetTags()
    {
        return ActivatableSkill.tags;
    }


    public List<string> GetAnimControllerID()
    {
        return animControllerID;
    }

    public string GetIconFilePath()
    {
        return iconName;
    }

    public TargetFiltering GetTargetFiltering()
    {
        return ActivatableSkill.GetTargetFiltering();
    }

    public string GetSFXKey()
    {
        return ((IUseable)ActivatableSkill).GetSFXKey();
    }

    public bool FilterTileNode(Actor source, TileNode center)
    {
        return ((IUseable)ActivatableSkill).FilterTileNode(source, center);
    }

    public void PayCosts(Actor actor)
    {
        ((IUseable)ActivatableSkill).PayCosts(actor);
    }

    public bool CanPayCost(Actor actor)
    {
        return ((IUseable)ActivatableSkill).CanPayCost(actor);
    }

    public ITargetable GetTargetType()
    {
        return ((IUseable)ActivatableSkill).GetTargetType();
    }

    public string GetKey()
    {
        return ((IUseable)ActivatableSkill).GetKey();
    }

    public int GetCoolDown()
    {
        return cooldown;
    }
}
