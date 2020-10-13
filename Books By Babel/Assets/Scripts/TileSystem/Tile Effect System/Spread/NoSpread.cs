using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoSpread : EffectSpreadBehavior
{
    public EffectSpreadBehavior Copy()
    {
        return new NoSpread();
    }

    //never spreads the effect to other tiles
    public void Spread(TileNode node, TileEffect effect)
    {
        return;
    }
}
