using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapEnchantmentSkillEffect : SkillEffect
{
    string enchantmentToApply;

    public MapEnchantmentSkillEffect(string enchantmentToApply)
    {
        this.enchantmentToApply = enchantmentToApply;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        WorldEnchantmentCombatNode cnode = new WorldEnchantmentCombatNode(source, target, enchantmentToApply);

        combat.actorDamageMap.Add(cnode);
    }

    public override SkillEffect Copy()
    {
        return new MapEnchantmentSkillEffect(enchantmentToApply);
    }
}
