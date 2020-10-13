using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TravelLocationComponent : LocationComponent
{
    public string mapKey;
    public MapCoords startPos;

    public override TextButton GenerateButtion(TextButton button, WorldMapLocationMenu menu)
    {
        //load world button
        //we need to save the stat
        button.ChangeText( mapKey);
        button.button.onClick.AddListener(delegate { ButtonClicked(); });


        return button;
    }

    public override string GetDescription()
    {
        return "";
    }

    public void ButtonClicked()
    {
        Globals.campaign.worldMapDictionary[mapKey].ChangeCurrentPos(startPos);
        Globals.campaign.currentWorldMap = mapKey;

        
        FilePath.CurrentSaveFilePath = SaveLoadManager.AutoSaveCampaignProgress( new SaveStateWorldMap(Globals.campaign));

        CustomeSceneLoader.LoadWorldMap();


    }
}
