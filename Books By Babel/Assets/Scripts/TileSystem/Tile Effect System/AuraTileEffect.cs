using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AuraTileEffect : TileEffect
{

    private string buffToAdd;
    public ActorData sourceID;

    public AuraTileEffect(string key, ActorData source, string tempID, EffectLengthBehavior lb, EffectSpreadBehavior sb, string buffToAdd) : base(key, tempID, lb, sb)
    {
        this.buffToAdd = buffToAdd;
        sourceID = source;

        actorEnter.Add(new AddBuffTileEffectComponent(buffToAdd, sourceID));
        init.Add(new AddBuffTileEffectComponent(buffToAdd, sourceID));

        actorExit.Add(new RemoveBuffTileEffectComponent(buffToAdd));
        end.Add(new RemoveBuffTileEffectComponent(buffToAdd));

    }

    public override void EndEffects(TileNode node)
    {
        if (node.HasActor())
        {
            //if there are more stacks of a buff on the tile than are the max stack for that buff on the actor, don't remove the
            // the buff from the actor
            // EX.  2 stacks of the the buff on the tile
            //      actor can only have one stack of that buff
            //      don't remove the buff, but remove the effect from the tile

            int effectCount = 0;
            int buffStackLimit = Globals.campaign.contentLibrary.buffDatabase.GetCopy(buffToAdd).maxStacks;

            BuffContainer bc = node.actorOnTile.actorData.buffContainer;



            foreach (TileEffect e in node.tileEffects)
            {
                if(e.GetKey() == key)
                {
                    effectCount++;
                }
            }

            if(effectCount > buffStackLimit)
            {
                //node.tileEffects.Remove(this);
                return;
            }

        }


        base.EndEffects(node);        

    }

    public override string GetDescription()
    {
        return buffToAdd + " (Source: " + sourceID + ")";
    }

    public override DatabaseEntry Copy()
    {
        AuraTileEffect e = new AuraTileEffect(key, sourceID, tempID, effectLength, effectSpread, buffToAdd);
        e.tempID = tempID;

        e.descript = descript;
        e.remove = remove;


        foreach (string s in attributes)
        {
            e.attributes.Add(s);
        }

        foreach (TileEffectComponent ec in init)
        {
            e.init.Add(ec.Copy());
        }

        foreach (TileEffectComponent ec in turn)
        {
            e.turn.Add(ec.Copy());
        }

        foreach (TileEffectComponent ec in actorEnter)
        {
            e.actorEnter.Add(ec.Copy());
        }

        foreach (TileEffectComponent ec in actorExit)
        {
            e.actorExit.Add(ec.Copy());
        }

        foreach (TileEffectComponent ec in end)
        {
            e.end.Add(ec.Copy());
        }


        return e;
    }
}
