using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UseSkillEffect : SkillEffect
{
    public string skillKey;

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        Skill skill = Globals.campaign.contentLibrary.skillDatabase.GetCopy(skillKey);

        skill.ProcessEffects(combat, source, target);

    }

    public override SkillEffect Copy()
    {
        UseSkillEffect effect = new UseSkillEffect();
        effect.skillKey = skillKey;

        return effect;
    }
}
