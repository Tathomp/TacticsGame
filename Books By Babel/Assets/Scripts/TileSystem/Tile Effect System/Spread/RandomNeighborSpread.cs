using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomNeighborSpread : EffectSpreadBehavior
{
    int chanceToSpread;

    public RandomNeighborSpread(int chanceToSpread)
    {
        this.chanceToSpread = chanceToSpread;
    }

    public EffectSpreadBehavior Copy()
    {
        return new RandomNeighborSpread(chanceToSpread);
    }

    public void Spread(TileNode node, TileEffect effect)
    {
        foreach (TileNode n in node.neighbors)
        {
            int i = Random.Range(1, 100);

            if (i <= chanceToSpread)
            //if (true)
            {
                if (n.SameTileEffect(effect) == false)
                {
                    //we can spread the effect to the next tile
                    TileEffect newEffect = (TileEffect)effect.Copy();



                    n.ProccessTags(newEffect.attributes);

                    //n.queuedEffects.Enqueue(newEffect);
                }
            }
        }

       
    }
}
