using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : TextButton {
    [HideInInspector]
    public ActorData actorOnButton;
    public Image sprite;

    public void ChangeSprite(Sprite s)
    {
        sprite.sprite = s;
    }
}
