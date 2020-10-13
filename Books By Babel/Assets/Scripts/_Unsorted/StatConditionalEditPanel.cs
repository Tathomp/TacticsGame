using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatConditionalEditPanel : MonoBehaviour
{
    public TMP_Dropdown statType, containerType;
    public TMP_InputField threshold;
    public Toggle lessThan;
    public EditEffectConditonals conditionalPanel;

    private StatThresholdConditional cond;


    public void InitPanel(Conditional cond)
    {
        conditionalPanel.SwitchContexts(this.gameObject);

        this.cond = cond as StatThresholdConditional;
        conditionalPanel.currConditional = cond;

        UpdateDisplay();

    }

    public void SaveState()
    {
        if (containerType.options.Count > 0 && threshold.text != "")
        {
            cond.type = (StatTypes)(Enum.Parse(typeof(StatTypes), statType.options[statType.value].text));
            cond.containerType = (StatContainerType)(Enum.Parse(typeof(StatContainerType), containerType.options[containerType.value].text));
            cond.threshold = int.Parse(threshold.text);
            cond.lessThan = lessThan.isOn;
        }
    }

    public void UpdateDisplay()
    {
        StatTypeDropDown();
        ContainerDropDown();

        lessThan.isOn = cond.lessThan;
        threshold.text = cond.threshold + "";
    }


    private void StatTypeDropDown()
    {
        statType.options.Clear();

        foreach (StatTypes item in Enum.GetValues(typeof(StatTypes)))
        {
            statType.options.Add(new TMP_Dropdown.OptionData(item.ToString()));

            if(item == cond.type)
            {
                statType.value = statType.options.Count - 1;
            }
        }
        statType.RefreshShownValue();

    }


    private void ContainerDropDown()
    {
        containerType.options.Clear();

        foreach (StatContainerType item in Enum.GetValues(typeof(StatContainerType)))
        {
            containerType.options.Add(new TMP_Dropdown.OptionData(item.ToString()));

            if (item == cond.containerType)
            {
                containerType.value = containerType.options.Count - 1;
            }
        }

        containerType.RefreshShownValue();
    }
}
