using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PropertyMapsContainer
{
    public PropertyTagMap<int, TileEffect> tileEffectMap;
    public PropertyTagMap<float, ResistanceLevel> skillEffectMap;

    public PropertyMapsContainer()
    {
        tileEffectMap = new PropertyTagMap<int, TileEffect>();
        skillEffectMap = new PropertyTagMap<float, ResistanceLevel>();
    }
}
