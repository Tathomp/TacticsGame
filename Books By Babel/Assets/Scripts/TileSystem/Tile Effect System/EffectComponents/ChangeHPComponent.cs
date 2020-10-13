using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChangeHPComponent : TileEffectComponent
{
    public int amt;


    public ChangeHPComponent(int amt)
    {
        this.amt = amt;
    }

    public TileEffectComponent Copy()
    {
        return new ChangeHPComponent(amt);
    }

    public void ExecuteEffect(TileNode tilenode)
    {
        if(tilenode.actorOnTile != null)
        {
            Debug.Log(tilenode.actorOnTile.name + "takes " + amt + "damge from tile " + tilenode.data.posX + ", " + tilenode.data.posY);
            tilenode.actorOnTile.ChangeHealth(Formulas.TileEffectDamage(amt));
        }
    }

    public float GetSCore(Actor ai, TileNode node)
    {
        if(amt >= 0)
        {
            return .1f;
        }
        else
        {
            return -.1f;
        }
    }
}
