using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BuffIconDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;


    private Buff current_buff;
    private TMP_Text tooltip;
    private GameObject panel;

    //We could just abstact this out so that Aura buffs have their own icon class
    private bool aurabuffEffectApplied = false;
    private List<GameObject> auraVisuals = new List<GameObject>();
    private GameObject prefab;

    public void InitIcon(Buff b, TMP_Text text_toolTip, GameObject panel, GameObject auraVisual)
    {
        this.current_buff = b;
        this.tooltip = text_toolTip;

        icon.sprite = Globals.GetSprite("IconSprites", current_buff.iconKey);
        this.prefab = auraVisual;
        this.panel = panel;


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel.gameObject.SetActive(false);

        if(aurabuffEffectApplied)
        {
            ClearAuraRange();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        string s = "Description: " + current_buff.GetHotbarDescription();
        if(current_buff.tempBuff)
        {
            s += "\n" + "Turn Remaining: " + current_buff.turnDuration;
        }


        foreach (BuffEffect effect in current_buff.effects)
        {
            if(effect is AuraBuffEffect)
            {
                aurabuffEffectApplied = true;
                //print the visuals
                PrintAuraEffectVisual(effect as AuraBuffEffect);
            }
        }

        tooltip.text = s;

        panel.gameObject.SetActive(true);

    }

    void ClearAuraRange()
    {
        int x = auraVisuals.Count - 1;

        for (int i = x; i >= 0; i--)
        {
            GameObject.Destroy(auraVisuals[i].gameObject);
            GameObject.Destroy(auraVisuals[i]);
        }


        auraVisuals = new List<GameObject>();
    }

    void PrintAuraEffectVisual(AuraBuffEffect e)
    {
        auraVisuals = new List<GameObject>();

        AuraBuffEffect effect = e;

        foreach (MapCoords coords in effect.effectMap.Keys)
        {
           auraVisuals.Add(  Instantiate<GameObject>(prefab, new Vector3(coords.X, coords.Y, 0), Quaternion.identity));
            Debug.Log("Printed AuraVsiual");
        }
    }


}
