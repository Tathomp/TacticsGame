using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable    ]
public class AdjustMovementCostTileEffectComponent : TileEffectComponent
{
    //So in the creation ui we can bundle two of these together to restore the default cost if we want
    //

    private string movementKey;
    private int value;

    public AdjustMovementCostTileEffectComponent(string movementKey, int value)
    {
        this.movementKey = movementKey;
        this.value = value;
    }

    public TileEffectComponent Copy()
    {
        return new AdjustMovementCostTileEffectComponent(movementKey, value);
    }

    public void ExecuteEffect(TileNode tilenode)
    {
        if(tilenode.type.MovementTypeCostMap.ContainsKey(movementKey))
        tilenode.type.MovementTypeCostMap[movementKey] += value;
    }

    public float GetSCore(Actor ai, TileNode node)
    {
        return 0;
    }
}
