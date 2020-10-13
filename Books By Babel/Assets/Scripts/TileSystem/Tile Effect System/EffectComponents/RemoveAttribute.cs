using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RemoveAttribute : TileEffectComponent
{
    public string tagToRemove;

    public RemoveAttribute(string tag)
    {
        tagToRemove = tag;
    }

    public TileEffectComponent Copy()
    {
        return new RemoveAttribute((string)tagToRemove.Clone());
    }

    public void ExecuteEffect(TileNode tilenode)
    {
        tilenode.type.attributes.Remove(tagToRemove);

    }

    public float GetSCore(Actor ai, TileNode node)
    {
        return 0;
    }
}
