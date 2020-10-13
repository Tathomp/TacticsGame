using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarDisplayPanel : MonoBehaviour
{
    // Editor
    public WorldMapManager worldMapManager;
    public TMP_Text BarNameLabel, MissionDetails;
    public ScrollListScaleableContent item_container;
    public TextButton missionButtonPrefab;

    // Member vars
    private Bar currBar;
    private List<TextButton> buttonList;

    private Mission currMission;
    private CutScene currCutscene;

    public void InitBarDisplayPanel(Bar bar)
    {
        worldMapManager.worldMapInput.SwitchState(new BlockUserInputState());


        currBar = bar;
        BarNameLabel.text = bar.BarName;

        buttonList = new List<TextButton>();


        //print mission bar
        PopulateMisisonButtons();


        gameObject.SetActive(true);
    }

    public void PopulateCinematics()
    {
        CleanButtonList();
        

        List<string> cs_keys = Globals.campaign.GetCutscenesWatched();

        foreach (string key in cs_keys)
        {
            PrintCinematicBuff(key);
        }

        item_container.AdjustContentLength();
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }

    public void BackButton()
    {
        worldMapManager.worldMapUIManager.ToggleOnWolrdMapInfo();
        
    }

    public void PopulateMisisonButtons()
    {
        CleanButtonList();

        List<string> missionKeys = Globals.campaign.GetMissionHandler().GetAvaliableMissions(currBar);

        foreach (string key in missionKeys)
        {
            PrintButton(key);
        }

        item_container.AdjustContentLength();

    }

    private void PrintCinematicBuff(string k)
    {
        TextButton missionButton = Instantiate(missionButtonPrefab, item_container.contentTransform);
        CutScene cs = Globals.campaign.GetCutsceneCopy(k);
        missionButton.ChangeText( cs.GetKey() );

        buttonList.Add(missionButton);


        missionButton.button.onClick.AddListener(delegate { CutsceButtonClicked(cs); });

    }

    private void CutsceButtonClicked(CutScene cs)
    {
        currMission = null;
        currCutscene = cs;

        MissionDetails.text = cs.GetKey();
    }


    private void PrintButton(string key)
    {
        Mission mission = Globals.campaign.GetMissionHandler().GetEntryData(key);
        TextButton missionButton = Instantiate(missionButtonPrefab, item_container.contentTransform);
        missionButton.ChangeText(mission.MissionName);



        buttonList.Add(missionButton);

        missionButton.button.onClick.AddListener( delegate { MissionButtonClicked(mission); } );

    }

    private void MissionButtonClicked(Mission mission)
    {
        currMission = mission;
        currCutscene = null;

        MissionDetails.text = currMission.MissionName + "\n" + currMission.descript;
    }

    private void OnDisable()
    {
        CleanButtonList();

    }

    private void CleanButtonList()
    {
        for (int i = buttonList.Count - 1; i >= 0; i--)
        {
            buttonList[i].button.onClick.RemoveAllListeners();
            GameObject.Destroy(buttonList[i].gameObject);
            GameObject.Destroy(buttonList[i]);

        }

        buttonList = new List<TextButton>();
    }


    public void AcceptButton()
    {
        if(currCutscene != null)
        {
            WatchCutscene();
        }
        else if(currMission != null)
        {
            AcceptMission();
        }
    }

    private void WatchCutscene()
    {
        SaveStateWorldMap wm_state = new SaveStateWorldMap(Globals.campaign);

        Globals.cutsceneData = new CutsceneData(currCutscene, wm_state, false);       


        CustomeSceneLoader.LoadCutsceneScene();
    }

    private void AcceptMission()
    {
        Globals.campaign.GetcutScenedataContainer().missionHandler.MissionsAccepted.Add(currMission.GetKey());

        Bar temp = currBar;

        //Reset bar ui
        ToggleOff();
        InitBarDisplayPanel(temp);
        worldMapManager.InitIcons();

    }
}
