using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveStateBase : SavedFile
{
    public string baseID;

    public SaveStateBase(Campaign campaign, string basekey = "baselevel")
        : base(campaign)
    {
        this.baseID = basekey;
    }

    public override void SwitchScene()
    {
        SceneManager.LoadScene("BaseScene");
    }
}
