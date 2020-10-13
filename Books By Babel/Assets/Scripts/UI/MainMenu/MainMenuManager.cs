using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Unity editor
    public NewGameSelection newgameSelectionPanel;
    public SaveGamePanel saveGameSelectionPanel;

    // hidden public fields
    [HideInInspector]
    public Party party;
    [HideInInspector]
    public Campaign campaign;

    public int stresstestLevel;
    public bool generateDemoCampaign;
    //Toolbox toolbox;
    ContentLibrary contentContainer;

    private void Start()
    {
        FilePath.CreateDefaultPaths();
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void GenerateDefaultCampaign()
    {
        GenerateDemoCampaign demo = new GenerateDemoCampaign();
        campaign = demo.campaign;
        StressTest(stresstestLevel);


      //  GenerateDemoDayCampaign d = new GenerateDemoDayCampaign();
      //  campaign = d.campaign;
      //  StressTest(stresstestLevel);



        Debug.Log("Generate campaigns");
    }

    private void StressTest(int level)
    {
        int mission = 50 * level;
        int items = 100 * level;
        int skills = 50 * level;
        int actors = 100 * level;

        for (int i = 0; i < mission; i++)
        {
            Mission m = Globals.campaign.GetMissionDataCopy("test_mission_00");
            m.ChangeKey(i.ToString());
            campaign.GetMissionHandler().AddMission(m);
        }

        for (int i = 0; i < items; i++)
        {
            Item item = (Item)campaign.GetItemCopy(("synth_helm"));
            item.ChangeKey(i.ToString());
            campaign.GetAllItems().AddEntry(item);

        }

        for (int i = 0; i < skills; i++)
        {
            Skill item = (Skill)campaign.contentLibrary.skillDatabase.GetCopy("fire_ball");
            item.ChangeKey(i.ToString());
            campaign.contentLibrary.skillDatabase.AddEntry(item);
        }

        for (int i = 0; i < actors; i++)
        {
            ActorData item = (ActorData)campaign.contentLibrary.actorDB.GetCopy(("abagail"));
            item.ChangeKey(i.ToString());
            campaign.contentLibrary.actorDB.AddEntry(item);
        }

        Debug.Log("Missions: " + mission + " Items: " + items + " Skills: " + skills + " Actors: " + actors);



        SaveStateBase sf = new SaveStateBase(campaign);
        SaveLoadManager.Savecampaign(campaign);
    }

    public void NewGame()
    {
        newgameSelectionPanel.InitNewGameSelection();
    }


    public void LoadEditior()
    {
        SceneManager.LoadScene("CreationSuiteScene");
    }


    public void LoadBattleScene()
    {
        saveGameSelectionPanel.InitSaveGameSelection();
    }


    /*
     Test transform
     new race, undead
     new job, necro
     no secondary,

     
     */
}
