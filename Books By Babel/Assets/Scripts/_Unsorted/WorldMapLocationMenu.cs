using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapLocationMenu : MonoBehaviour
{
    //editor
    public TMP_Text text;
    public TextButton MissionButtonPrefab;
    public Transform buttonContainer;

    public ShopMenuPanel shopPanel;
    public BarDisplayPanel barPanel;
    public MissionInfoPanel missionInfoPanel;


    //private
    private List<TextButton> missionButtons = new List<TextButton>();
    
    private MapCoords pos;
    private WorldMapManager wmm;

   public void InitLocationMenu(WorldMapLocationGameObject location, WorldMapManager wmm)
    {

        if(location == null)
        {
           // ToggleOff();
            return;
        }

        missionInfoPanel.ToggleOff();
        barPanel.ToggleOff();

        ClearButtons();

        pos = location.location.coords;
        this.wmm = wmm;

        missionButtons = new List<TextButton>();

        text.text = location.location.AreaName;

        foreach (Mission mission in location.missions)
        {
            InstantiateMissionButton(mission);
        }

        foreach (LocationComponent loc in location.location.locationcomponents)
        {
            TextButton temp = Instantiate(MissionButtonPrefab, buttonContainer);

            missionButtons.Add(loc.GenerateButtion(temp, this));
        }


        this.gameObject.SetActive(true);
    }

    private void InstantiateMissionButton(Mission mission)
    {
        TextButton temp = Instantiate(MissionButtonPrefab, buttonContainer);

        temp.ChangeText( mission.MissionName);
        temp.button.onClick.AddListener(delegate { MissionButtonClicked(mission); });
        missionButtons.Add(temp);
    }

    private void MissionButtonClicked(Mission mission)
    {
        missionInfoPanel.WriteMissionInfo(mission, pos.X, pos.Y);
        wmm.currWorldMap.currentPos = pos;
        //switch to a new input state here?
    }

    public void ToggleOff()
    {
        ToggleOffPanels();
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ClearButtons();
    }

    public void ClearButtons()
    {
        int amt = missionButtons.Count - 1;

        for (int i = amt; i >= 0; i--)
        {
            missionButtons[i].button.onClick.RemoveAllListeners();
            Destroy(missionButtons[i].gameObject);
            Destroy(missionButtons[i]);
        }

        missionButtons = new List<TextButton>();

    }

    public void ToggleOffPanels()
    {
        shopPanel.CloseShop();
        barPanel.ToggleOff();
        missionInfoPanel.ToggleOff();
}
}
