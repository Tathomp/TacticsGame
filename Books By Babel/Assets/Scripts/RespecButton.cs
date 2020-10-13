using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RespecButton : MonoBehaviour
{
    public TalentPanelManager panelManager;
    public TMP_Text costText;

    public void OnEnable()
    {
        costText.text = Globals.campaign.RespecModel.DisplayCost();
    }

    public void RespecButtonClicked()
    {
        RespecCost c = Globals.campaign.RespecModel;

        if(c.CanPayCost())
        {
            c.PayCost();
            c.ResetTalents(panelManager.currentJob.GetKey(), panelManager.currActor);
        }
    }
}
