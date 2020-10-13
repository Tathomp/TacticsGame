using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChangeTileTypeEffect : SkillEffect
{
    string currTileType;

    public ChangeTileTypeEffect(string currTileType)
    {
        this.currTileType = currTileType;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {
        ChangeTileEffectCombatNode node = new ChangeTileEffectCombatNode(currTileType, source, target);

        combat.actorDamageMap.Add(node);
    }

    public override SkillEffect Copy()
    {
        return new ChangeTileTypeEffect(currTileType);
    }
}

