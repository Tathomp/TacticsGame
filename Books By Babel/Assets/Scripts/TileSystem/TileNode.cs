using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNode
{
    public TileData data;
    public TileTypes type;
    public List<TileNode> neighbors;

    public Actor actorOnTile;

    public TileNode prevNode;
    public Tile tileGO;

    public List<TileEffectSprite> effectSprites;
    public List<TileEffect> tileEffects;

    private Queue<TileEffect> queuedEffects;


    public TileNode(Tile tile)
    {
        tileGO = tile;

        this.data = tileGO.data;
        this.type = tileGO.type;
        neighbors = new List<TileNode>();
        effectSprites = new List<TileEffectSprite>();
        tileEffects = new List<TileEffect>();

        queuedEffects = new Queue<TileEffect>();
    }

    public void ProccessTags(List<string> tags)
    {
        PropertyTagMap<int, TileEffect> ptm = Globals.GetPorpertyMap();

        queuedEffects = new Queue<TileEffect>();


        foreach (string attrib in type.attributes)
        {
            foreach (string tag in tags)
            {
                //Do comparison to find out if we need to do anything
                if (ptm.EntryExists(tag, attrib))
                {
                    Tuple<int, TileEffect> tuple = ptm.GetEffect(tag, attrib);

                    int procChance = tuple.ele1;
                    int roll = Random.Range(0, 100);
                    TileEffect effect = tuple.ele2;


                    if ( roll <= procChance)
                    {
                        if(SameTileEffect(effect) == false)
                        {

                            queuedEffects.Enqueue((TileEffect)effect.Copy());
                            //effect.InitEffects(this);
                            //tileEffects.Add((TileEffect)effect.Copy());
                            //TileEffect.InitTileEffectVisual(this, effect);
                        }                     

                    }
                    else
                    {
                       // Debug.Log("Roll: " + roll + " Proc Chance: " + procChance);
                    }
                }
            }
        }

        //ProcessEffectQueue();
    }


    public void ProccessTurn()
    {
        for (int i = 0; i < tileEffects.Count; i++)
        {
            tileEffects[i].ProcessTurn(this);

            if (tileEffects[i].effectLength.EndETimerOver())
            {
                tileEffects[i].EndEffects(this);

                // Remove the sprite caused by the effect
                // maybe move this to the effect end method for more general
                // use

                for (int j = 0; j < effectSprites.Count; j++)
                {
                    if (effectSprites[j].currentEffect
                        == tileEffects[i].tempID)
                    {
                        GameObject.Destroy(effectSprites[j].gameObject);
                        effectSprites.RemoveAt(j);
                    }
                }

                tileEffects.RemoveAt(i);

            }
        }
                
    }

    public void RemoveTileEffect(string tempId)
    {
        for (int i = 0; i < effectSprites.Count; i++)
        {
            if(effectSprites[i].currentEffect == tempId)
            {
                GameObject.Destroy(effectSprites[i].gameObject);
                GameObject.Destroy(effectSprites[i]);
                effectSprites.RemoveAt(i);
                break;
            }
        }

        for (int i = 0; i < tileEffects.Count; i++)
        {
            if(tileEffects[i].tempID == tempId)
            {
                tileEffects[i].EndEffects(this);
                tileEffects.RemoveAt(i);
                break; // could also just decrement i and keep the loop running if we need to destroy multiple obj with the same 
                        //temp id
            }
        }
    }

    public void RemoveTileEffect(TileEffect effect)
    {
        

        foreach (TileEffectSprite sprite in effectSprites)
        {
            if(sprite.currentEffect == effect.tempID)
            {
                GameObject.Destroy(sprite.gameObject);
                effectSprites.Remove(sprite);

                effect.EndEffects(this);
                tileEffects.Remove(effect);
                return;
            }
        }


        effect.EndEffects(this);
        tileEffects.Remove(effect);
    }

    public void InitTileEffectsVisuals()
    {
        if (tileEffects != null)
        {
            foreach (TileEffect e in tileEffects)
            {
                TileEffect.InitTileEffectVisual(this, e);
            }
        }
        else
        {
            tileEffects = new List<TileEffect>();
        }
    }


    public void AddTileEffect(TileEffect e)
    {
        if (SameTileEffect(e) == false)
        {
            TileEffect.InitTileEffectVisual(this, e);
            tileEffects.Add(e);
            e.InitEffects(this);
        }
    }


    //We can modify this to handle stacks of the same tile effect if we awant too
    public bool SameTileEffect(TileEffect e)
    {
        foreach (TileEffect f in tileEffects)
        {
            if(f.GetKey() == e.GetKey())
            {
                return true;
            }
        }

        return false;
    }


    public void ProcessEffectQueue()
    {
        while(queuedEffects.Count > 0)
        {

            TileEffect newEffect = queuedEffects.Dequeue();
            tileEffects.Add(newEffect);
            newEffect.InitEffects(this);
            TileEffect.InitTileEffectVisual(this, newEffect);
        }

    }


    /// Fire Effects
    /// 
    public void EnterTileEffects()
    {

        foreach (TileEffect effect in tileEffects)
        {
            effect.EnterEffects(this);
        }

        for (int i = 0; i < tileEffects.Count - 1; i++)
        {
            if(tileEffects[i].remove)
            {
                RemoveTileEffect(tileEffects[i]);
            }
        }
    }

    public void ExitTileEffects()
    {
        foreach (TileEffect effect in tileEffects)
        {
            effect.ExitEffects(this);
        }
    }

    public void InitTileEffect()
    {
        foreach (TileEffect effect in tileEffects)
        {
            effect.InitEffects(this);
        }
    }

    public bool HasActor()
    {
        return actorOnTile != null;
    }
}
