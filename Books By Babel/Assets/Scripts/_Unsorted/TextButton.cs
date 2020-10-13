using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextButton : MonoBehaviour
{
    public TMP_Text textToChange;
    public Button button;

    public void ChangeText(string text)
    {
        textToChange.text = text;
    }
}
