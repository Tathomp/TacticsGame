using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    // editor
    public Sprite notSelected, selected;
    public SpriteRenderer spriteReder;

    public void Select()
    {
        spriteReder.sprite = selected;
    }

    public void DeSelect()
    {
        spriteReder.sprite = notSelected;
    }
}
