using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileEffect : DatabaseEntry
{
    public bool remove;
    public string descript;

    public EffectLengthBehavior effectLength;
    public EffectSpreadBehavior effectSpread;

    public List<TileEffectComponent> init, turn, actorEnter, actorExit, end;

    public List<string> attributes;

    public string tempID;
    public string animationID;

    public bool removeOnTrigger;

    public TileEffect(string key, string tempID, EffectLengthBehavior lb, EffectSpreadBehavior sb) : base(key)
    {
        init = new List<TileEffectComponent>();
        turn = new List<TileEffectComponent>();
        actorEnter = new List<TileEffectComponent>();
        actorExit = new List<TileEffectComponent>();
        end = new List<TileEffectComponent>();

        attributes = new List<string>();

        effectLength = lb;
        effectSpread = sb;

        removeOnTrigger = false;

        this.tempID = tempID;
        this.animationID = "";
    }


    public void InitEffects(TileNode node)
    {
        foreach (TileEffectComponent e in init)
        {
            e.ExecuteEffect(node);
        }
    }


    public void ProcessTurn(TileNode node)
    {
        effectSpread.Spread(node, this);

        foreach (TileEffectComponent e in turn)
        {
            e.ExecuteEffect(node);
        }
    }

    public virtual void EndEffects(TileNode node)
    {
        foreach (TileEffectComponent e in end)
        {
            e.ExecuteEffect(node);
        }

        // maybe move code 
        
    }

    public void EnterEffects(TileNode node)
    {
        foreach (TileEffectComponent effectComponent in actorEnter)
        {
            effectComponent.ExecuteEffect(node);
        }

        remove = removeOnTrigger;

    }

    public void ExitEffects(TileNode node)
    {
        foreach (TileEffectComponent effectComponent in actorExit)
        {
            effectComponent.ExecuteEffect(node);
        }
    }


    public virtual string GetDescription()
    {
        return descript;
    }

    public override DatabaseEntry Copy()
    {
        TileEffect e = new TileEffect(key, tempID,
            effectLength.Copy(),
            effectSpread.Copy());

        e.descript = descript;
        e.remove = remove;
        e.animationID = animationID;
        e.removeOnTrigger = removeOnTrigger;

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



    public static void InitTileEffectVisual(TileNode tileNode, TileEffect effect)
    {

        if (effect.animationID == "")
        {
            return;
        }
        else
        {
            int x = tileNode.data.posX;
            int y = tileNode.data.posY;

            TileEffectSprite s = Globals.GenerateTileEffectSprite(tileNode.data.posX, tileNode.data.posY, effect.animationID);

            s.InitEffectSprite(effect.tempID, x, y);

            Globals.GetBoardManager().EffectBoard[x, y] = s;

            tileNode.effectSprites.Add(s);

        }
    }

 
}
