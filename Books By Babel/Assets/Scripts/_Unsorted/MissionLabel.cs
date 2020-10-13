using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MissionLabel : TextButton
{


    private Mission currentMission;

    public void InitMissionLabel(Mission m, RectTransform newMask)
    {
        currentMission = m;
        // maskRect = newMask;
        ChangeText( m.MissionName);

    }
}
