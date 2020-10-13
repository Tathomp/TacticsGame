using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;

public class NewGameSelection : MonoBehaviour {

    //prefab
    public TextButton button;

    //display
    public TMP_Text text;
    public Transform ButtonContainer;
    public Image image;
    //public SpriteRe image;
    public ModifierPanel modpanel;

    //vars
    private string currentCampaignSelect;
    private List<TextButton> campButtons = new List<TextButton>();

    [HideInInspector]
    public Campaign curretnCampaign;

	public void InitNewGameSelection()
    {
        gameObject.SetActive(true);

       // Globals.CleanButtons(campButtons);

        //DirectoryInfo dir = new DirectoryInfo(FilePath.CampaignFolder);
        List<string> files = Globals.ParseFileNames(FilePath.CampaignFolder, FilePath.CampExt);

        foreach (string s in files)
        {
            
            Debug.Log(s);
            // Here we'll instantiate a button
            TextButton b = Instantiate<TextButton>(button, ButtonContainer);
            b.ChangeText(((Campaign)SaveLoadManager.LoadFile(FilePath.CampaignFolder + s)).CampaignName);
            b.button.onClick.AddListener(delegate { ButtonClicked(s); });
            campButtons.Add(b);
        }
    
        if(campButtons.Count > 0)
        {
            campButtons[0].button.onClick.Invoke();
        }

    }

    public void ButtonClicked(string campaignname)
    {
        curretnCampaign = SaveLoadManager.LoadCampaign(campaignname);

        Debug.Log(FilePath.CampaignFolder + campaignname);
        text.text = "Name: " + "\n" + curretnCampaign.CampaignName + "\n" + "\n" + "Description: " + "\n"
            + curretnCampaign.CampaignDescrtiption;

        currentCampaignSelect = campaignname;

        image.sprite = Globals.GetSprite(FilePath.CampaignThumbnailAtlas, curretnCampaign.thumbnailName);

        modpanel.InitMOdiferPanel(curretnCampaign.campaignModifier);

    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }
   

    private void OnDisable()
    {
        CleanUpButtons();
    }


    private void CleanUpButtons()
    {
        for (int i = campButtons.Count - 1; i >= 0; i--)
        {
            campButtons[i].button.onClick.RemoveAllListeners();
            Destroy(campButtons[i].gameObject);
            Destroy(campButtons[i]);
        }

        campButtons = new List<TextButton>();
    }

    public void StartNewGame()
    {
        modpanel.ApplyChanges();


        SavedFile state = new SaveStateWorldMap(curretnCampaign);


        FilePath.CurrentSaveFilePath = SaveLoadManager.AutoSaveCampaignProgress(state);
        Globals.campaign = curretnCampaign;


        if(curretnCampaign.initalCutscene != "")
        {
            Globals.cutsceneData = new CutsceneData(curretnCampaign.GetCutsceneCopy(curretnCampaign.initalCutscene), state, true);
            CustomeSceneLoader.LoadCutsceneScene();
        }
        else if(curretnCampaign.initalCombat != "")
        {
            state = new SavedFileMission(curretnCampaign, curretnCampaign.GetMissionData(curretnCampaign.initalCombat));
            FilePath.CurrentSaveFilePath = SaveLoadManager.AutoSaveCampaignProgress(state);

            state.SwitchScene();


        }
        else
        {
            state.SwitchScene();
        }

    }
}
