using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DrainHealth : ChangeHealthEffect
{
    public DrainHealth(List<DamageObject> obj, string parentSkill, bool heal = false) 
        : base(obj, parentSkill, heal)
    {

    }



    ///Should we also process the tags again for the drain effect?
    ///We just make this a toogle
    ///we could make toggle fors for swap, drain(always heal), invert(if a holy tagged thing actually damaged the undead tagged actor then the drain effect would heal the user)
    ///
    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        if(target.actorOnTile == null)
        {
            return;
        }

        base.ActorEffect(combat, source, target);

        int healthToHeal = deltaH;
        healthToHeal = Mathf.Abs(healthToHeal);



        TileNode tile = Globals.GetBoardManager().pathfinding.GetTileNode(source.GetPosX(), source.GetPosY());

        HealthChangeCombatNode node = new HealthChangeCombatNode(source, tile, source, healthToHeal);

        combat.actorDamageMap.Add(node);

    }

    public override SkillEffect Copy()
    {
        List<DamageObject> t = new List<DamageObject>();

        foreach (DamageObject damage in dmgObjData)
        {
            t.Add(damage.Copy());
        }


        return new DrainHealth(t, parentSkill, heal);
    }
}
