using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadCampaignCreationPanel : MonoBehaviour
{
    //ui hooks
    public CreationSuiteManager manager;
    public Button prefab;
    public Transform container;
    public TMP_Text nam, description;

    //privates
    List<Button> campaignButtons = new List<Button>();
    Campaign currCampaign;


    public void InitLoadCampaignPanel()
    {

        ClearButtons();

        List<string> files = Globals.ParseFileNames(FilePath.CampaignFolder, FilePath.CampExt);

        foreach (string item in files)
        {
            Button t = Instantiate<Button>(prefab, container);
            t.transform.GetChild(0).GetComponent<Text>().text = item;
            t.onClick.AddListener(delegate { CampaignButtonClicked(item); });
            campaignButtons.Add(t);
        }




        gameObject.SetActive(true);
        manager.SetCurrentActiveObject(this.gameObject);
    }

    public void CampaignButtonClicked(string s)
    {
       currCampaign = SaveLoadManager.LoadCampaign(s);

        PrintCamaignInfo();
    }

    private void PrintCamaignInfo()
    {
        nam.text = currCampaign.CampaignName;
        description.text = currCampaign.CampaignDescrtiption;
    }

    public void LoadCampaign()
    {
        manager.currentCampaign = currCampaign;
    }


    private void OnDisable()
    {
        ClearButtons();
    }

    public void ClearButtons()
    {
        int x = campaignButtons.Count - 1;
        for (int i = x; i >= 0; i--)
        {
            Destroy(campaignButtons[i].gameObject);
            Destroy(campaignButtons[i]);
        }

        campaignButtons = new List<Button>();
    }

}
