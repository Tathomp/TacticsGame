using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyListPanel : MonoBehaviour
{
    public ScrollListScaleableContent actorList;
    public TextButton button;

    public TextButton PopulateList(ActorData actor)
    {
        gameObject.SetActive(true);

        TextButton tmp = Instantiate<TextButton>(button, actorList.contentTransform);
        tmp.ChangeText(actor.Name);
        actorList.AddToList(tmp);
        return tmp;
    }

    public void ToogleOff()
    {
        actorList.CleanUp();
        gameObject.SetActive(false);
    }
}

