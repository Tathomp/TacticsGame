using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BuffEffect : IDisplayInfo
{
    public List<Conditional> conditionalsRequired = new List<Conditional>();


    #region Data Manip
    //public virtual void OnApply(ActorData actor) { }
    public virtual void OnApply(ActorData actor, ActorData source) { }
    public virtual void OnRemove(ActorData actor) { }
    public virtual void OnDeath(ActorData actor, ActorData kiler) { }
    public virtual void OnStartTurn(ActorData actor) { }
    public virtual void OnActorSpawn(ActorData actor) { }
    #endregion

    public virtual void OnDamageInflicted(Combat combat, AnimationData currentData) { }
    public virtual void OnTargeted(Combat combat, AnimationData currentData) { }
    public virtual void ReactionEffect(Combat combat, CombatNode node) { }
    public virtual void OnMove(ActorData actor, TileNode startTile, TileNode destTile) { }

    public virtual void ModSkillCost(Actor source, Skill skillToMod) { }

    //maybe some kind of OMoveAlongPath method, take in the list of tilenodes path

    public abstract string PrintNameOfEffect();

    public virtual string GetHotbarDescription()
    {
        return "";
    }

    public abstract BuffEffect Copy();

    public  string GetName()
    {
        throw new System.NotImplementedException();
    }

    public string GetIconFilePath()
    {
        throw new System.NotImplementedException();
    }

    public bool ConditionasMet(Combat c, AnimationData currentData)
    {
        return ConditionsMet(c.source, currentData.DestNode, currentData.skillUsed as Skill);
    }

    public bool ConditionsMet(Actor actor, TileNode target, Skill skill)
    {
        foreach (Conditional conditional in conditionalsRequired)
        {
            if(conditional.ConditionMet(actor, target, skill) == false)
            {
                return false;
            }
        }

        return true;
    }

    public void CopyConditionals(BuffEffect be)
    {
        foreach (Conditional conditional in conditionalsRequired)
        {
            be.conditionalsRequired.Add(conditional.Copy());
        }

    }
}
