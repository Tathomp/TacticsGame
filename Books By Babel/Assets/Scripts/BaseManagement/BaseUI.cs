using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseUI : MonoBehaviour {

    public MissionSelectionPanel missionSelectionPanel;
    public PartySelectionPanel partySelectionPanel;
    public TalentPanelManager talentPanel;
    public RelationshipPanel relationshipPanel;
    public ShopsListPanel ShopListPanel;
    public CutsceneController cutsceneController;

    public GameObject MenuPanel;

    public Campaign campaign;


    // Use this for initialization
    void Awake ()
    {
        CollapseAll();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// This can be deleted probably
    /// </summary>
    public void ToggleOnMissionSelect()
    {
        CollapseAll();

      //  missionSelectionPanel.ToggleOn(campaign.GetAllAvaliableMissions());
    }

    public void ToggleOnPartySelection()
    {
        CollapseAll();
        partySelectionPanel.ToggleOn(campaign.currentparty.partyCharacter);
    }

    public void ToggleOnRelationships()
    {
        CollapseAll();
        relationshipPanel.InitPanel(campaign.currentparty.partyCharacter, campaign.relationshipMap);
    }

    public void CollapseAll()
    {
        missionSelectionPanel.gameObject.SetActive(false);
        relationshipPanel.ToggleOff();
        partySelectionPanel.gameObject.SetActive(false);
        talentPanel.gameObject.SetActive(false);
        partySelectionPanel.characterEditor.equipmentPanel.gameObject.SetActive(false);
        //partySelectionPanel.characterEditor.inventoryPanel.gameObject.SetActive(false);
        ShopListPanel.ToggleOff();
    }

    public void CollapseAllAndSome()
    {
        CollapseAll();
        MenuPanel.gameObject.SetActive(false);
    }

    public void ExpandMenu()
    {
        MenuPanel.gameObject.SetActive(true);
    }

    public void ShopListButtonClicked()
    {
        CollapseAll();
       // ShopListPanel.InitShopList(campaign.CampaignShops);
    }

    public void SaveCampagin()
    {
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
