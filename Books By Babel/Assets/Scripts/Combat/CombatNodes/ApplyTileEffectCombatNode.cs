using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyTileEffectCombatNode : CombatNode
{
    private string tileeffect_toapply_id;

    public ApplyTileEffectCombatNode(string tileeffect_toapply_id, Actor source, TileNode targetedTile) : base(source, targetedTile)
    {
        this.tileeffect_toapply_id = tileeffect_toapply_id;
    }

    public override void ApplyEffect()
    {
        targetedTile.AddTileEffect(Globals.campaign.GetTileDatabaseContainer().Effects.GetCopy(tileeffect_toapply_id));
    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
       
    }
}
