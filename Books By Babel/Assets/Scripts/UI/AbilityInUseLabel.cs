using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInUseLabel : MonoBehaviour
{
    public TMP_Text InUseLabel;

    public void InitLabel(string text)
    {
        InUseLabel.text = text;
        gameObject.SetActive(true);
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }
}
