using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class EditTagConditionalPanel : MonoBehaviour
{
    public CreationSuiteManager csmanager;
    public TMP_Dropdown tags, matchingType;

    public EditEffectConditonals conditionalPanel;
    private MatchingTagConditional cond;

    public void InitPanel(Conditional c)
    {
        conditionalPanel.SwitchContexts(this.gameObject);
        cond = c as MatchingTagConditional;
        conditionalPanel.currConditional = c;
        UpdateDisplay();
    }

    public void SaveData()
    {
        if(matchingType.options.Count > 0)
        {
            cond.tagToMatch = tags.options[tags.value].text;
            cond.type = (MatchingTagConditional.MatchingType)(Enum.Parse(typeof(MatchingTagConditional.MatchingType), matchingType.options[matchingType.value].text));
        }
    }

    public void UpdateDisplay()
    {
        DisplayTagList();
        DisplayMatchingType();
    }

    private void DisplayMatchingType()
    {
        matchingType.ClearOptions();

        foreach (MatchingTagConditional.MatchingType t in Enum.GetValues(typeof(MatchingTagConditional.MatchingType)))
        {
            matchingType.options.Add(new TMP_Dropdown.OptionData(t.ToString()));

            if (t == cond.type)
            {
                matchingType.value = matchingType.options.Count - 1;
            }
        }

        tags.RefreshShownValue();
    }


    private void DisplayTagList()
    {
        tags.ClearOptions();

        foreach (string t in csmanager.currentCampaign.properties)
        {
            tags.options.Add(new TMP_Dropdown.OptionData(t));

            if(t == cond.tagToMatch)
            {
                tags.value = tags.options.Count - 1;
            }
        }

        tags.RefreshShownValue();
    }
}
