using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverOverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ToolTipPanel target;
    public string TextToDisplay;

    public void InitHoverOverText(ToolTipPanel target, string text)
    {
        this.target = target;
        TextToDisplay = text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(target != null)
        target.OpenPanel(TextToDisplay);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (target != null)
        target.gameObject.SetActive(false);
    }
}


public class ToolTipPanel : MonoBehaviour
{

    public TMP_Text text;

    public void OpenPanel(string s)
    {
        text.text = s;
        gameObject.SetActive(true);
    }

}
