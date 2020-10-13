using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AddBuffTileEffectComponent : TileEffectComponent
{
    public string buffToAdd;
    public ActorData source;

    public AddBuffTileEffectComponent(string buffToAdd, ActorData source)
    {
        this.buffToAdd = buffToAdd;
        this.source = source;
    }

    public TileEffectComponent Copy()
    {
        return new AddBuffTileEffectComponent(buffToAdd, source);
    }

    public void ExecuteEffect(TileNode tilenode)
    {
        if(tilenode.HasActor() == false)
        {
            return;
        }

        ///well check to make sure this buff doesn't over lap with another buff before trying to
        
        //use buff key to see what max stack is, if current stack is < max stack, apply the buff, otherwise do nothign

        Buff b = Globals.GetBoardManager().campaign.contentLibrary.buffDatabase.GetCopy(buffToAdd);
        ActorData data = tilenode.actorOnTile.actorData;

        if (data.buffContainer.CanBuffBeApplied(b))
        {

            data.buffContainer.ApplyBuff(data, source, b);
        }
        

    }

    public float GetSCore(Actor ai, TileNode node)
    {
        return 0;
    }
}
