using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TileEffectComponent
{
    void ExecuteEffect(TileNode tilenode);
    TileEffectComponent Copy();

    float GetSCore(Actor ai, TileNode node);
}
