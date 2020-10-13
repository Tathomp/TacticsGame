using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChangeHealthEffect : SkillEffect
{
    //public bool heals;
    public List<DamageObject> dmgObjData; // copy
    public bool heal;
    public string parentSkill;
    protected int deltaH;

    public ChangeHealthEffect(List<DamageObject> dmg, string parentSkill, bool heal=false)
    {
        this.dmgObjData = dmg;
        this.heal = heal;
        this.parentSkill = parentSkill;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode center)
    {
        if (center.actorOnTile == null)
        {
            //This might cause problems
            //
            return;
        }

        Actor target = center.actorOnTile;

        if(target.ActorsController().PlayerControlled())
        {
            Debug.Log("");
        }

       CalculateChange(combat, source, target);

        foreach (AnimationData data in combat.animationDatas)
        {
            if(data.skillUsed.GetKey() == parentSkill)
            {
                ProcessTags(data.skillUsed.GetTags(), target.actorData.actorPropertyTags);
            }
        }
        //ProcessTags(combat.skillInUse.GetTags(), target.actorData.actorPropertyTags);

        HealthChangeCombatNode node = new HealthChangeCombatNode(source, center, target, deltaH);


        combat.actorDamageMap.Add(node);
    }

    void ProcessTags(List<string> skillTags, List<string> actorTags)
    {
        PropertyTagMap<float, ResistanceLevel> skillEffectMap = Globals.campaign.GetPropertyMaps().skillEffectMap;

        foreach (string skill in skillTags)
        {
            foreach (string actor in actorTags)
            {
                if(skillEffectMap.EntryExists(skill,actor))
                {
                    Tuple<float, ResistanceLevel> entry = skillEffectMap.GetEffect(skill, actor);

                    if(entry.ele2 == ResistanceLevel.Vulnerable)
                    {
                        deltaH = -Mathf.Abs(deltaH);
                    }
                    else if( entry.ele2 == ResistanceLevel.Immune)
                    {
                        deltaH = 0;
                    }

                }
            }
        }
    }


    int CalculateValue(Actor source, Actor target)
    {
        return Formulas.CalculateDamageChange(source, target, dmgObjData);


      //  return Mathf.RoundToInt((source.GetCurrentStats(typeBones) * bonusrate) - target.GetCurrentStats(resistPenalty) * resistRate);
    }

    public void CalculateChange(Combat combat, Actor source, Actor target)
    {
        //deltaH = value;



        deltaH = CalculateValue(source, target);

        if (heal)
        {
            deltaH = Mathf.Abs(deltaH);
        }
        else
        {
            deltaH = -Mathf.Abs(deltaH);

        }

        
    }


    public override SkillEffect Copy()
    {
        List<DamageObject> t = new List<DamageObject>();

        foreach (DamageObject damage in dmgObjData)
        {
            t.Add(damage.Copy());
        }


        return new ChangeHealthEffect(t, parentSkill, heal);

    }
}
