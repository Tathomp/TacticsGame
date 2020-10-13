using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MatchingTagConditional : Conditional
{
    public string tagToMatch;
    public enum MatchingType { Skill, Tile, Actor, PrimaryJob, SecondaryJob, Race }
    public MatchingType type;

    public MatchingTagConditional(string tagToMatch, MatchingType type)
    {

        this.tagToMatch = tagToMatch;
        this.type = type;
    }

    ///
    /// Actor is the user of the skill. So if we want to check the job of the target, we'll have to do something else

    public override bool ConditionMet(Actor actor, TileNode target, Skill skill)
    {
        switch(type)
        {
            case MatchingType.Actor :
                {
                    return actor.actorData.actorPropertyTags.Contains(tagToMatch);
                }
            case MatchingType.Skill:
                {
                    return skill.tags.Contains(tagToMatch);
                }
            case MatchingType.Tile:
                {
                    return target.type.attributes.Contains(tagToMatch);
                }
            case MatchingType.PrimaryJob:
                {
                    return actor.actorData.primaryJob == tagToMatch;
                }
            case MatchingType.SecondaryJob:
                {
                    return actor.actorData.secondaryJob == tagToMatch;
                }
        }

        return true;
    }

    public override Conditional Copy()
    {
        return new MatchingTagConditional(tagToMatch, type);
    }

    public override string DisplayCondition(Actor actor, TileNode target, Skill skill)
    {
        throw new System.NotImplementedException();
    }
}
