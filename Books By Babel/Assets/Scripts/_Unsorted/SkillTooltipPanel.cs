using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTooltipPanel : MonoBehaviour
{
    public TMP_Text text;
    private IDisplayInfo skill;


    public void InitPanel(IDisplayInfo skill)
    {
        this.skill = skill;
        text.text = skill.GetHotbarDescription();


        gameObject.SetActive(true);
    }


    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }
}
