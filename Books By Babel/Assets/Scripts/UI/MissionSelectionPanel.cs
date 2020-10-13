using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSelectionPanel : MonoBehaviour {

    public Button buttonPrefab;

    public MissionInfoPanel missionInfoPanel;

    List<Button> missionBUttons;
    //butttons
    //on click, display details about mission, give options to embark

    private void Awake()
    {
        missionBUttons = new List<Button>();

    }

    private void OnDisable()
    {
        ClearButtons();
        missionInfoPanel.gameObject.SetActive(false);
    }

    public void ToggleOn(List<Mission> missions)
    {
        missionBUttons = new List<Button>();

        gameObject.SetActive(true);
        PopulateMissionButtons(missions);
    }

    public void PopulateMissionButtons(List<Mission> missions)
    {
        /*
        for (int i = 0; i < missions.Count; i++)
        {
            Debug.Log(missions[i].completed);
            if (false)
            {
                Debug.Log("mission completed");
                missions.RemoveAt(i);
                i--;
            }
            else
            {
                Debug.Log("mission not completed");

                Button temp = Instantiate(buttonPrefab, this.transform);
                temp.transform.GetChild(0).GetComponent<Text>().text = missions[i].MissionName;
                temp.onClick.AddListener(delegate { MissionButtonClicked(missions[i]); });
                missionBUttons.Add(temp);
            }
        }
        */


        foreach (Mission mission in missions)
        {
            if (!mission.completed)
            {
                Button temp = Instantiate(buttonPrefab, this.transform);
                temp.transform.GetChild(0).GetComponent<Text>().text = mission.MissionName;
                temp.onClick.AddListener(delegate { MissionButtonClicked(mission); });
                missionBUttons.Add(temp);
            }
        }
    }

    public void ClearButtons()
    {
        int amt = missionBUttons.Count - 1;

        for (int i = amt; i >= 0; i--)
        {
            missionBUttons[i].onClick.RemoveAllListeners();
            Destroy(missionBUttons[i].gameObject);
            Destroy(missionBUttons[i]);
        }
    }

    public void MissionButtonClicked(Mission mission)
    {
        Debug.Log(mission.mapName);
       // missionInfoPanel.WriteMissionInfo(mission);
    }
}
