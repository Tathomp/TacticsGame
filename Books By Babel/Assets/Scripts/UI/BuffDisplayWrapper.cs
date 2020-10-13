using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class BuffDisplayWrapper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public TMP_Text buffName;

    CharacterInfoPreviewPanel infoText;
    IDisplayInfo buff;

    public void InitPreview(CharacterInfoPreviewPanel t, IDisplayInfo b)
    {
        infoText = t;
        buff = b;

        buffName.text = b.GetName();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoText.ChangeText(buff.GetHotbarDescription());

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoText.ToggleOff();
    }
}
