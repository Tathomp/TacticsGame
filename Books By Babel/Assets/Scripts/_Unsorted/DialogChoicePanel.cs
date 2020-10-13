using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogChoicePanel : MonoBehaviour
{
    public TMP_Text[] choiceOne;

    public int currSelection;
    private ChoiceAction currAction;


    public Color selected, notSelected;

    public void InitDialogChoice(ChoiceAction action)
    {
        currAction = action;

        currSelection = 0;

        choiceOne[0].text = action.choices[0];
        choiceOne[1].text = action.choices[1];

        ToggleOn();

        ShiftSelection(0);
    }

    public void TurnOffChoices()
    {
        foreach (TextMeshProUGUI choice in choiceOne)
        {
            choice.gameObject.SetActive(false);
        }
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }


    public void ToggleOn()
    {
        gameObject.SetActive(true);
    }

    public void ShiftSelectionUp()
    {
        ShiftSelection(-1);
    }

    public void ShiftSelectionDown()
    {
        ShiftSelection(1);
    }

    public void ShiftSelection(int i)
    {
        currSelection = Mathf.Abs((currSelection + i) % 2);


        //change visuals
        choiceOne[0].faceColor = notSelected;
        choiceOne[1].faceColor = notSelected;

        choiceOne[currSelection].faceColor = selected;
    }

    public string GetEventFlag()
    {
        return currAction.eventFlagMission[currSelection];
    }

    public string GetMissionFlag()
    {
        return currAction.missionFlagToSet[currSelection];
    }

    public string GetNextCutscene()
    {
        return currAction.nextCutscene[currSelection];
    }
}
