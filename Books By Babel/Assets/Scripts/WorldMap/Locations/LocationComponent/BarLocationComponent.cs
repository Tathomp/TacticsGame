using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BarLocationComponent : LocationComponent
{
    public string barKey;
   
    public BarLocationComponent(string key)
    {
        this.barKey = key;
    }

    public override TextButton GenerateButtion(TextButton button, WorldMapLocationMenu menu)
    {

        Bar bar = Globals.campaign.GetcutScenedataContainer().barDatabase.GetData(barKey);

        button.button.onClick.AddListener(delegate { BarButtonClicked(menu, bar); });
        button.ChangeText( bar.BarName);

        return button;
    }

    public void BarButtonClicked(WorldMapLocationMenu menu, Bar bar)
    {
        menu.ToggleOffPanels();
        menu.barPanel.InitBarDisplayPanel(bar);
    }

    public override string GetDescription()
    {
        Bar bar = Globals.campaign.GetcutScenedataContainer().barDatabase.GetData(barKey);
        return "Tavern: " + bar.BarName + "\n";
    }
}
