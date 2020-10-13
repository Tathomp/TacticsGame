using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnOrderIcon : MonoBehaviour
{

    public Image sr;
    public TMP_Text text;
    [HideInInspector]
    public Actor currActor;
    public Button buttonComponet; 

    public void InitIcon(Actor actor)
    {
        currActor = actor;
        text.text = currActor.actorData.Name;
        sr.sprite = actor.GetComponent<SpriteRenderer>().sprite;

        // It's probably better to do like a mask with the scolling text long term
        float t = text.fontSize / 2 * currActor.actorData.Name.Length + 32;

        this.GetComponent<RectTransform>().sizeDelta = new Vector2(t, this.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void Update()
    {
        sr.sprite = currActor.GetComponent<SpriteRenderer>().sprite;

    }
}
