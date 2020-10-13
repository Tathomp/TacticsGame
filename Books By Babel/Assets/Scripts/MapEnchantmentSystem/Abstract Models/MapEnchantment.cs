using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapEnchantment : DatabaseEntry
{
    // I think we're just going to apply buffs to actors and tiles with this system
    //

    public ActorMapEnchantmentEffect actorEffect;
    public TileMapEnchantmentEffect tileEffect;
    public string bg_color_gradient_itd;

    public MapEnchantment(string key) : base(key)
    {
        bg_color_gradient_itd = "default";
    }

    public override DatabaseEntry Copy()
    {
        MapEnchantment temp = new MapEnchantment(key);
        if(actorEffect != null)
        {
            temp.actorEffect = actorEffect.Copy();

        }

        if(tileEffect != null)
        {
            temp.tileEffect = tileEffect.Copy();
        }

        temp.bg_color_gradient_itd = bg_color_gradient_itd;

        return temp;
    }


    public void ApplyTileEffect(TileNode tilenode)
    {
        if(tileEffect == null)
        {
            return;
        }

        tileEffect.Apply(tilenode);
    }

    public void RemoveTileEffect(TileNode tilenode)
    {
        if (tileEffect == null)
        {
            return;
        }

        tileEffect.Remove(tilenode);
    }

    public void ApplyActorEffects(Actor actor)
    {
        if (actorEffect == null)
        {
            return;
        }

        actorEffect.Apply(actor);
    }

    public void RemoveActorEffects(Actor actor)
    {
        if (actorEffect == null)
        {
            return;
        }

        actorEffect.Remove(actor);
    }
}



