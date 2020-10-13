using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Conditional
{
    public abstract bool ConditionMet(Actor actor, TileNode target, Skill skill);

    public abstract string DisplayCondition(Actor actor, TileNode target, Skill skill);

    public abstract Conditional Copy();
}

public abstract class ComplextConditional : Conditional
{
    public List<Conditional> conditonals = new List<Conditional>();

    public ComplextConditional ()
    {
        conditonals = new List<Conditional>();
    }

    public ComplextConditional(List<Conditional> c)
    {
        conditonals = new List<Conditional>();

        foreach (Conditional conditional in c)
        {
            conditonals.Add(conditional.Copy());
        }
    }


}

