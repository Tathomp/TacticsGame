using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EffectSpreadBehavior
{
    //could rework this into an abstract class
    //method to see if we should spread
    //method to actually do the spread in a particular pattern
    EffectSpreadBehavior Copy();
    void Spread(TileNode node, TileEffect effect);

}
