using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RemoveBuffTileEffectComponent : TileEffectComponent
{
    public string buffToAdd;

    public RemoveBuffTileEffectComponent(string buffToAdd)
    {
        this.buffToAdd = buffToAdd;
    }

    public TileEffectComponent Copy()
    {
        return new RemoveBuffTileEffectComponent(buffToAdd);
    }

    public void ExecuteEffect(TileNode tilenode)
    {

        if(tilenode.HasActor())
        {
            tilenode.actorOnTile.actorData.buffContainer.RemoveBuff(tilenode.actorOnTile.actorData, buffToAdd);
        }
        
    }

    public float GetSCore(Actor ai, TileNode node)
    {
        return 0;
    }

}
