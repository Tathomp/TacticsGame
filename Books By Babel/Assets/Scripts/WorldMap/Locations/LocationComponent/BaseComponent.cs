using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class BaseComponent : LocationComponent
{
    public string BaseMapName { get; protected set; }

    public BaseComponent(string mapname)
    {
        BaseMapName = mapname;
    }

    public override TextButton GenerateButtion(TextButton button, WorldMapLocationMenu menu)
    {
        button.button.onClick.AddListener(delegate { BaseButtonClicked(); });
        button.ChangeText(BaseMapName);

        return button;
    }

    void BaseButtonClicked()
    {
        Campaign campaign = Globals.campaign;
        SaveStateBase save = new SaveStateBase(campaign);

        SaveLoadManager.AutoSaveCampaignProgress(save);

        SceneManager.LoadScene("BaseScene");
    }

    public override string GetDescription()
    {
        return "";
    }
}
