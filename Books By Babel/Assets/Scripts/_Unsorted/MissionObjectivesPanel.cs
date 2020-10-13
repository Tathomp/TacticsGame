using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionObjectivesPanel : MonoBehaviour
{
    //Editor
    public TMP_Text text;

   public void InitMissionObjectivePanel(Mission currMission)
   {
        text.text = currMission.MissionName + "\n" + currMission.PrintObjectGoals();
        gameObject.SetActive(true);
   }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }
}
