using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutsceneActionTransitionToBattle : CutSceneAction
{

    private string missionID;

    public CutsceneActionTransitionToBattle(string missionID)
    {
        this.missionID = missionID;
    }

    public override CutSceneAction Copy()
    {
        return new CutsceneActionTransitionToBattle(missionID);
    }

    public override IEnumerator ExecuteAction(CutsceneController controller, bool playNextNode = true)
    {
        Campaign c = Globals.campaign;

        SavedFile state = new SavedFileMission(c, c.GetMissionData(missionID));
        FilePath.CurrentSaveFilePath = SaveLoadManager.AutoSaveCampaignProgress(state);

        state.SwitchScene();
        yield return null;
    }
}
