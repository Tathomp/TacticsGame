using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SelectedCharacterPanel : MonoBehaviour
{
    public BoardManager bm;
    public PartyEditPanel editPanel;

    public TMP_Text characterName;
    public Image portrait;

    public ActorData currentData;

    public void InitSelectedCharacterPanel(ActorData data)
    {
        characterName.text = data.Name;
        portrait.sprite = Globals.GetPortrait(data.portraitFilePath);
        gameObject.SetActive(true);
    }


    public void ExitBattle()
    {
        // We'll have to think about how we handle cutscene choices ya know
        // make sure major flags aren't actually pushed if we back out
        // WE could cache the choices

        CustomeSceneLoader.LoadWorldMap();
    }

    public void StartFight()
    {

        if (bm.party.NumberOfSelected() == 0)
        {
            //Prevents the user from starting the game with zero units deployed
            return;
        }
        bm.ui.TurnInfoPanels();

        bm.inputFSM.SwitchState(new UsersTurnState(bm));
        bm.turnManager.CalculateFastest();
        CloseMenu();

    }

    public void OpenPanel()
    {
        editPanel.InitPanel(currentData);
    }

    public void CloseEditPanel()
    {
        currentData = editPanel.currentData;

        InitSelectedCharacterPanel(currentData);
        editPanel.ToggleOff();

        if(currentData != null)
        {
            bm.inputFSM.SwitchState(new ExamineBattlefieldInputState(bm, currentData));
        }
    }

    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }



}
