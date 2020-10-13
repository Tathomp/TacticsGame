using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class LocationComponent
{
    public abstract TextButton GenerateButtion(TextButton button, WorldMapLocationMenu menu);
    public abstract string GetDescription();
}
