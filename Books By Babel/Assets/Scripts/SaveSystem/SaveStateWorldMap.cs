using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveStateWorldMap : SavedFile
{
    public SaveStateWorldMap(Campaign campaign) : base(campaign)
    {

    }

    public override void SwitchScene()
    {
        SceneManager.LoadScene("WorldMapScene");
    }
}
