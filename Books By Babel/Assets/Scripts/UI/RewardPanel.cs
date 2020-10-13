using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour {


    public TMP_Text rewardText;
    Mission currMission;

    public void InitRewardList(BoardManager bm)
    {
        gameObject.SetActive(true);
        string tempText = "";

        currMission = bm.currentMission;

        if(currMission.mainReward != null)
        {
            tempText = currMission.mainReward.ToString();
        }

        if(currMission.CheckIfSideOjbectiveComplete(bm))
        {
            tempText += "\n " + "Side rewards: ";
            tempText += "\n " + currMission.sideRewards.ToString();
        }

        if (currMission.CheckIfSecretOjbectiveComplete(bm))
        {
            tempText += "\n " + "Secret rewards: ";
            tempText += "\n " + currMission.secretRewards.ToString();
        }

        rewardText.text = tempText;
    }

    public void Collect()
    {
        SavedFile stat = (SavedFile)(SaveLoadManager.LoadFile(FilePath.CurrentSaveFilePath));

        if (currMission.end_cutscenekey != "")
        {
            Globals.cutsceneData = new CutsceneData(Globals.campaign.GetCutsceneCopy(currMission.end_cutscenekey), stat, true);
            CustomeSceneLoader.LoadCutsceneScene();
        }
        else
        {
            stat.SwitchScene();

        }

    }
}
