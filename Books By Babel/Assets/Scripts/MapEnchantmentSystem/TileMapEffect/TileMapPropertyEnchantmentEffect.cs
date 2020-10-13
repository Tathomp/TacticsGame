using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileMapPropertyEnchantmentEffect : TileMapEnchantmentEffect
{
    //This fires tags on to the tilenodes
    //A different class should be used to apply properties to the nodes
    //

    List<string> tags;

    public TileMapPropertyEnchantmentEffect(List<string> tags)
    {
        this.tags = tags;
    }

    public override void Apply(TileNode tilenode)
    {
        tilenode.ProccessTags(tags);
        tilenode.ProcessEffectQueue();
    }

    public override TileMapEnchantmentEffect Copy()
    {
        return new TileMapPropertyEnchantmentEffect(tags);
    }

    public override void Remove(TileNode tilenode)
    {
        Debug.Log(this + " has been removed from " + tilenode);
    }
}
