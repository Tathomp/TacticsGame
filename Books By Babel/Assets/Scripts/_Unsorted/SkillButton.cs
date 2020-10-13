using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillButton : TextButton, IPointerEnterHandler, IPointerExitHandler
{
    private IDisplayInfo currSkill;
    private SkillTooltipPanel panel;

    public void InitButton(IDisplayInfo skill, SkillTooltipPanel panel)
    {
        this.currSkill = skill;
        this.panel = panel;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.InitPanel(currSkill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel.ToggleOff();
    }

    private void OnDestroy()
    {
        panel.ToggleOff();
    }
}
