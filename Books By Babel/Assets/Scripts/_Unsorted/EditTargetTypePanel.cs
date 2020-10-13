using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditTargetTypePanel : MonoBehaviour
{
    public DropdownMenu selectedTargetType;
    public TMP_Text label;

    //
    public EditSingleTargetType singlepanel;
    public AoeTargetPanel aoeTargetPanel;
    public BlockTargetPanel blockTargetPanel;
    public ConeTargetingPanel coneTargetpanel;
    public LineTargetingPanel lineTargetPanel;
    public RandomTargetEditPanel randomPanel;

    //
    private EditTargetPanel currGame;
    private ITargetable currTargetType;


    public void DisplayNewTargetType()
    {

        List<string> impassTiles = new List<string>();
        bool stopOnOccupied = false;

        switch (selectedTargetType.GetValue())
        {
            case "Single Target":
                singlepanel.InitDisplay(new SingleTarget(impassTiles, stopOnOccupied));
                ActivateNewPanel(singlepanel);
                break;
            case "AoE Target":
                break;
            case "Block Target":
                break;
            case "Cone Target":
                break;
            case "Line Target":
                break;
            case "Random Target":
                break;
        }

    }


    

    public void DisplayTargetTypeData(ITargetable ttd)
    {
        selectedTargetType.droptDown.value = 0;

        if (ttd is SingleTarget)
        {
            //open the single target menu
            //populat the data
            label.text = "Single Target";
            singlepanel.InitDisplay(ttd as SingleTarget);
            selectedTargetType.droptDown.value = 0;

            ActivateNewPanel(singlepanel);
        }
        else if (ttd is AoeTarget)
        {
            //open the single target menu
            //populat the data
            label.text = "AoE Target";
            aoeTargetPanel.InitDisplay(ttd as AoeTarget);
            selectedTargetType.droptDown.value = 1;

            ActivateNewPanel(aoeTargetPanel);
        }
        else if (ttd is BlockTarget)
        {
            //open the single target menu
            //populat the data
            label.text = "Block Target";
            blockTargetPanel.InitDisplay(ttd as BlockTarget);
            ActivateNewPanel(blockTargetPanel);
            selectedTargetType.droptDown.value = 2;

        }
        else if (ttd is ConeTarget)
        {
            //open the single target menu
            //populat the data
            label.text = "Cone Target";
            coneTargetpanel.InitDisplay(ttd as ConeTarget);
            ActivateNewPanel(coneTargetpanel);
            selectedTargetType.droptDown.value = 3;

        }
        else if (ttd is LineTarget)
        {
            //open the single target menu
            //populat the data
            label.text = "Line Target";
            lineTargetPanel.InitDisplay(ttd as LineTarget);
            ActivateNewPanel(lineTargetPanel);
            selectedTargetType.droptDown.value = 4;

        }
        else if (ttd is RandomTargeting)
        {
            //open the single target menu
            //populat the data
            label.text = "Random Target";
            randomPanel.InitDisplay(ttd as RandomTargeting);
            ActivateNewPanel(randomPanel);
            selectedTargetType.droptDown.value = 5;

        }
    }


    public void ActivateNewPanel(EditTargetPanel go)
    {
        if(currGame != null)
        {
            currGame.gameObject.SetActive(false);
        }

        currGame = go;
        currGame.gameObject.SetActive(true);
    }

}

