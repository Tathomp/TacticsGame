using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewConditonalMenu : MonoBehaviour
{
    public EditEffectConditonals conditonalPanel;

    public void NewRng()
    {
        conditonalPanel.AddCondintional(new RandomRollConditional(50));
        Exit();
    }

    public void NewTag()
    {
        conditonalPanel.AddCondintional(new MatchingTagConditional(
            conditonalPanel.tagEditPanel.csmanager.currentCampaign.properties[0]
            , MatchingTagConditional.MatchingType.Actor));
        Exit();
    }

    public void NewStatThreshold()
    {
        conditonalPanel.AddCondintional(new StatThresholdConditional(StatTypes.Defenese, StatContainerType.Current, 40, false));
        Exit();
    }


    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }

}
