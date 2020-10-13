using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoPreviewPanel : MonoBehaviour {

    public TMP_Text text;

    public void ChangeText(string s)
    {
        ToggleOn();
        text.text = s;
    }

    public void ToggleOn()
    {
        this.gameObject.SetActive(true);

    }

    public void ToggleOff()
    {
        this.gameObject.SetActive(false);

    }
}
