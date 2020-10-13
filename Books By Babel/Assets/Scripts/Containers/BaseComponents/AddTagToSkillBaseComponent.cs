using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AddTagToSkillBaseComponent
{
    public string tagToAdd;

    public AddTagToSkillBaseComponent(string tagToAdd)
    {
        this.tagToAdd = tagToAdd;
    }


    public void AddTagToSkill(Skill skill)
    {
        AddTagToList(skill.tags);
    }

    public void RemoveTagFromSkill(Skill skill)
    {
        RemoveTagFromList(skill.tags);

    }

    public void AddTagToTile(TileNode node)
    {
        AddTagToList(node.type.attributes);
    }


    public void RemoveTagFromTile(TileNode node)
    {
        RemoveTagFromList(node.type.attributes);

    }

    public void AddTagToActor(ActorData data)
    {
        AddTagToList(data.actorPropertyTags);
    }

    public void RemoveTagFromActor(ActorData data)
    {
        RemoveTagFromList(data.actorPropertyTags);

    }

    private void AddTagToList(List<string> tags)
    {
        if(tags.Contains(tagToAdd) == false)
        tags.Add(tagToAdd); //could check to make sure we dont add twice
    }

    private void RemoveTagFromList(List<string> tags)
    {
        if(tags.Contains(tagToAdd))
        tags.Remove(tagToAdd); //could check to make sure we dont remove twice

    }
}
