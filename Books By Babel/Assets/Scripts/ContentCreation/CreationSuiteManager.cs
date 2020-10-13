using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreationSuiteManager : MonoBehaviour
{
    [HideInInspector]
    public Campaign currentCampaign;

    //Editor
    public MapEditingPanel mapEditingPanel;

    public bool HasCampaignBeenChanged;

    private GameObject currentActiveObject;

    public TMP_Text debug;
    string s = "";

    public InputFSM inputStateMachine;

    //public const string default_campaign_name = "default.cmp";
    public const string default_campaign_name = "default.cmp";

    // Start is called before the first frame update
    void Start()
    {
        GenerateDemoCampaign de = new GenerateDemoCampaign();
        
        s += "Debug" + "\n";
        debug.text = s;
        currentCampaign = SaveLoadManager.LoadFile(FilePath.CampaignFolder + default_campaign_name) as Campaign;
        currentCampaign = de.campaign; 
        inputStateMachine = new InputFSM(new BlockUserInputState());
    }

    private void Update()
    {
        inputStateMachine.ProcessInput();
    }


    public void MapEditingPanelButton()
    {
        CloseAllOtherPanels();

        mapEditingPanel.InitEditingPanel(currentCampaign);
    }

    public void CloseAllOtherPanels()
    {
        mapEditingPanel.ToggleOff();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NewCampaign()
    {
        currentCampaign = new Campaign("", Globals.GenerateRandomHex());
    }

    public void SaveCampaign()
    {
        Debug.Log("Save campaign");

        SaveLoadManager.Savecampaign(currentCampaign);
    }

    public void LoadCampaign()
    {
        Debug.Log("Load campaign");
    }

    public void SetCurrentActiveObject(GameObject obj)
    {
        if (currentActiveObject != null)
        {
            currentActiveObject.SetActive(false);
        }

        obj.SetActive(true);
        currentActiveObject = obj;
    }
}
