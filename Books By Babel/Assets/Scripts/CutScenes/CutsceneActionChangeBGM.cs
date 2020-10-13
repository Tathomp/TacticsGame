using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutsceneActionChangeBGM : CutSceneAction
{
    public string bgm_key;

    public CutsceneActionChangeBGM(string bgm_key)
    {
        this.bgm_key = bgm_key;
    }

    public override CutSceneAction Copy()
    {
        return new CutsceneActionChangeBGM(bgm_key);
    }

    public override IEnumerator ExecuteAction(CutsceneController controller, bool playNextNode = true)
    {
        controller.bgm.ChangeSong(bgm_key);

        yield return null;
    }
}
