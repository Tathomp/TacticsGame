using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditCampaignPanel : MonoBehaviour
{

    public CreationSuiteManager manager;
    public TMP_InputField campaign_name;
    public TMP_InputField campaign_descript;


    public void InitialzeCampaignPanel()
    {
        gameObject.SetActive(true);

        manager.SetCurrentActiveObject(this.gameObject);

        PopulateCampaignText();
    }


    public void SaveChanges()
    {
        if (manager.currentCampaign != null)
        {
             manager.currentCampaign.CampaignName = campaign_name.text;
            manager.currentCampaign.CampaignDescrtiption = campaign_descript.text;
        }
    }


    void PopulateCampaignText()
    {
        if (manager.currentCampaign != null)
        {
            campaign_name.text = manager.currentCampaign.CampaignName;
            campaign_descript.text = manager.currentCampaign.CampaignDescrtiption;
        }
    }
}
