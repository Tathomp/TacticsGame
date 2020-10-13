using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CombatNode
{
    //could just be a list of things to apply to the actor
    //damange, buffs, items add, removed, changes in pos
    //the actor is the key to the combatndoe dictionary
    public Actor source;
    public TileNode targetedTile;

    public Actor target;


    public CombatNode(Actor source, TileNode targetedTile)
    {


        this.source = source;
        this.targetedTile = targetedTile;

        if(targetedTile.actorOnTile != null)
        {
            target = targetedTile.actorOnTile;
        }
    }

    public abstract void ApplyEffect();

    public abstract void UpDatePreview(PreviewUIPanel panel);



    public bool ActorIsTargeted()
    {
        return target != null;
    }




}
