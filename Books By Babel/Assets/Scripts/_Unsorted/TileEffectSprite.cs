using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEffectSprite : MonoBehaviour {

    public string currentEffect;
    public MapCoords coords;

    public void InitEffectSprite(string effect, int x, int y)
    {
        currentEffect = effect;
        coords = new MapCoords(x, y);
    }

    public void ResetAnimation()
    {
        GetComponent<Animator>().Rebind();
    }
}
