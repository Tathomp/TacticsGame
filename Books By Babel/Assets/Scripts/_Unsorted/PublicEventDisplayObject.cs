using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PublicEventDisplayObject : MonoBehaviour
{
    public TMP_Text textLabel;

    private Event e;
   

    public void InitDisplayObject(Event e)
    {
        this.e = e;
        textLabel.text = e.DisplayText();
    }
}
