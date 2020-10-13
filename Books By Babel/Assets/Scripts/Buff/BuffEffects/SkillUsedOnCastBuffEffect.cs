using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillUsedOnCastBuffEffect : BuffEffect
{
    public string skillIDToUse;
    public bool castOnTarget; //cetners on the target if true, on the caster if false

    public SkillUsedOnCastBuffEffect(string skillIDToUse, bool castOnTarget)
    {
        this.skillIDToUse = skillIDToUse;
        this.castOnTarget = castOnTarget;
    }

    public override BuffEffect Copy()
    {
        SkillUsedOnCastBuffEffect e = new SkillUsedOnCastBuffEffect(skillIDToUse, castOnTarget);
        CopyConditionals(e);

        return e;
    }
    /*



             */

    public override void OnDamageInflicted(Combat combat, AnimationData currentData)
    {
        //if(true)
        if(ConditionasMet(combat, currentData))
        {
            Skill s = Globals.campaign.contentLibrary.skillDatabase.GetCopy(skillIDToUse);
            TileNode node;

            if(castOnTarget)
            {
                node = currentData.DestNode;
            }
            else
            {
                node = currentData.sourceNode;
            }

            //  Look at the combat class
            //  think about how to add stuff to the combate node map
            //  I think we all we need to do here is add the approiate animatino data

            AnimationData data = AnimationData.NewAntionData(s, currentData.sourceNode, node);
            combat.animationQueue.Enqueue(data);
            /*  

         */
        }

    }

    public override string PrintNameOfEffect()
    {
        return "Use skill on cast";
    }
}
