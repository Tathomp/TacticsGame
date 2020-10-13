using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NextSkillNode
{
    public string skillKey;
    public bool useActorSource;

    public NextSkillNode(string nextSkill, bool useActorSource)
    {
        this.skillKey = nextSkill;
        this.useActorSource = useActorSource;
    }

    public NextSkillNode Copy()
    {
        return new NextSkillNode(skillKey, useActorSource);
    }
}
