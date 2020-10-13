using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class TileMapEnchantmentEffect
{
    public abstract void Apply(TileNode tilenode);
    public abstract void Remove(TileNode tilenode);
    public abstract TileMapEnchantmentEffect Copy();
}
