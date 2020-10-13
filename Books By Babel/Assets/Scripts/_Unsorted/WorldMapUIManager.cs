using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WorldMapUIManager : MonoBehaviour
{
    //editor
    public WorldMapManager worldMapManager;
    public WorldMapLocationMenu worldMapLocationMenu;
    public WorldMapMenu menu;
    public WorldMapLocationInfoPanel infoPanel;
    public TextMeshProUGUI worldLabel;
    public MissionListContainer missionListContainer;

    public void Awake()
    {
       // worldMapLocationMenu.ToggleOff();
      //  menu.ToggleOff();

        
    }

    public void RefreshMissionList()
    {
        missionListContainer.InitList(Globals.campaign.GetMissionHandler().MissionsAccepted);
    }

    public void ToggleOnWorldMapLocationMenu(WorldMapLocationGameObject node)
    {
        worldMapLocationMenu.InitLocationMenu(node, worldMapManager);
    }

    public void InitWorldMap()
    {
        worldLabel.text = worldMapManager.currWorldMap.worldMapName;

        RefreshMissionList();
    }

    public void ToggleOffWolrdMapInfo()
    {
        worldMapLocationMenu.gameObject.SetActive(false);
        missionListContainer.gameObject.SetActive(false);
        infoPanel.gameObject.SetActive(false);
    }

    public void ToggleOnWolrdMapInfo()
    {
        worldMapLocationMenu.gameObject.SetActive(true);
        missionListContainer.gameObject.SetActive(true);
        infoPanel.gameObject.SetActive(true);
        if(worldMapManager.worldMapInput != null)
        worldMapManager.worldMapInput.SwitchState(new NavigateWorldInputState(worldMapManager, this));
    }
}
