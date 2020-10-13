using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionInfoPanel : MonoBehaviour {

    public TMP_Text text, rewardDescript;

    Mission currMission;
    int x, y;

	public void WriteMissionInfo(Mission mission, int x, int y)
    {
        currMission = mission;

        gameObject.SetActive(true);

        string info = "Map name: " + mission.mapName +"\n";
        info += "Mission Name: " + mission.MissionName +"\n";
        info += mission.descript;

        text.text = info;

        this.x = x;
        this.y = y;

        PrintRewards(mission);
    }


    void PrintRewards(Mission mission)
    {
        string r = "Rewards: \n";

        if (mission.mainReward != null)
        {
            List<Reward> rewards = mission.mainReward.rewards;
            
            foreach (Reward reward in rewards)
            {
                r += reward.RewardString() + "\n";
            }
        }
        
        rewardDescript.text = r;
    }

    public void EmbarkButton()
    {
        ///idk
        ///
        ///Maybe we save where the player was on the map here?

        SavedFileMission save = new SavedFileMission(Globals.campaign, currMission);
        FilePath.CurrentSaveFilePath = SaveLoadManager.AutoSaveCampaignProgress(save);


        if (currMission.int_cutsceneKey == "")
        {
            //No cutscene to play, go straight into the battle
            SceneManager.LoadScene("BoardScene");
        }
        else
        {
            //Play the cutscene first
            Globals.cutsceneData = new CutsceneData(Globals.campaign.GetCutsceneCopy(currMission.int_cutsceneKey), save, true);
            SceneManager.LoadScene("CutsceneScene");
        }


    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }
}
