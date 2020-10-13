using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileMapBuffEnchantmentEffect : TileMapEnchantmentEffect
{

    public string tileEffectKeyToAdd;
    protected string tempID;

    public TileMapBuffEnchantmentEffect(string tileEffectKeyToAdd)
    {
        this.tileEffectKeyToAdd = tileEffectKeyToAdd;
    }

    public override void Apply(TileNode tilenode)
    {
        TileEffect e = Globals.campaign.GetTileData().Effects.GetCopy(tileEffectKeyToAdd);
        tempID = e.tempID;
        tilenode.AddTileEffect(e);
    }

    public override void Remove(TileNode tilenode)
    {
        tilenode.RemoveTileEffect(tempID);

    }

    public override TileMapEnchantmentEffect Copy()
    {
        return new TileMapBuffEnchantmentEffect(tileEffectKeyToAdd) { tempID = tempID};
    }
}
