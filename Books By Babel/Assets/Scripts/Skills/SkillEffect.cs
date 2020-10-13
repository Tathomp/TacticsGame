using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SkillEffect
{

    public abstract void ActorEffect(Combat combat, Actor source, TileNode target);


    public abstract SkillEffect Copy();

}
