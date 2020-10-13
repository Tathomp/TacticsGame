using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionListContainer : MonoBehaviour
{
    public WorldMapManager manager;

    public RectTransform mainContainer, sideContainer, partyContainer, reoccuringContainer;
    public MissionLabel labelPrefab;
    //
    List<MissionLabel> missionLabelList = new List<MissionLabel>();
    public RectTransform newMask;

    
    public void InitList(List<string> missions)
    {
        ClaerList();

        foreach (string item in missions)
        {
            Mission m = Globals.campaign.GetMissionData(item);

            MissionLabel l = Instantiate(labelPrefab);
            l.InitMissionLabel(m, newMask);
            l.GetComponent<Button>().onClick.AddListener(delegate { MissionLabelClicked(m); });

            switch (m.missionType)
            {
                case MissionType.Main:
                    {
                        l.transform.SetParent(mainContainer);
                        ToggleContainer(mainContainer);
                        break;
                    }
                case MissionType.Side:
                    {
                        l.transform.SetParent(sideContainer);
                        ToggleContainer(sideContainer);
                        break;
                    }
                case MissionType.Party:
                    {
                        l.transform.SetParent(partyContainer);
                        ToggleContainer(partyContainer);
                        break;
                    }
                case MissionType.Reoccuring:
                    {
                        l.transform.SetParent(reoccuringContainer);
                        ToggleContainer(reoccuringContainer);
                        break;
                    }
            }            
        }
    }

    private void MissionLabelClicked(Mission m)
    {
        WorldMapSelector s = manager.worldMapelectorInstance;

        WorldMapLocationGameObject n = manager.locationDictionary[m.mapName];

        s.MoveSelectorTo(n.location.coords.X, n.location.coords.Y);
    }

    public void ClaerList()
    {
        for (int i = missionLabelList.Count -1; i >= 0; i--)
        {
            missionLabelList[i].GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(missionLabelList[i].gameObject);
            Destroy(missionLabelList[i]);
        }

        missionLabelList = new List<MissionLabel>();
    }





    #region Toggles
    public void ToggleMainContainer()
    {
        ToggleContainer(mainContainer);
    }

    public void ToggleSideContainer()
    {
        ToggleContainer(sideContainer);
    }
    public void TogglePartyContainer()
    {
        ToggleContainer(partyContainer);
    }
    public void ToggleReoccuringContainer()
    {
        ToggleContainer(reoccuringContainer);
    }
    #endregion


    #region General Methods
    private void ToggleContainer(RectTransform container)
    {
        bool on;

        if(container.childCount == 1)
        {
            return;
        }
        else
        {
            on = (container.GetChild(1).gameObject.activeInHierarchy);           

            
        }


        if(on)
        {
            TurnOffContainer(container);
        }
        else
        {
            TurnOnContainer(container);
        }
    }


    private void TurnOnContainer(RectTransform container)
    {
        for (int i = 1; i < container.childCount; i++)
        {
            container.GetChild(i).gameObject.SetActive(true);
        }
        container.sizeDelta = new Vector2(container.sizeDelta.x, 30 * container.childCount);


    }

    private void TurnOffContainer(RectTransform container)
    {
        for (int i = 1; i < container.childCount; i++)
        {
            container.GetChild(i).gameObject.SetActive(false);
        }

        container.sizeDelta = new Vector2(container.sizeDelta.x, 30);

    }

    #endregion
}
