using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Delete class it's useless
public class PrebattleMenu : MonoBehaviour
{
    public BoardManager bm;
    public PartyEditPanel partyEditPanel;
    public PartyListPanel partyListPanel;



    public void ExamineBattlefield()
    {
       // bm.inputFSM.SwitchState(new ExamineBattlefieldInputState(bm, this));

        //switch to a new input state for examining the battle 
        //close the map


        //move camera around battlefield
        //remove a placed unit
        //move a placed unit to another

        CloseMenu();
    }



    public void PartyMenu()
    {
        partyEditPanel.InitPanel();
        CloseMenu();
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
    

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void ExitPartyMenu()
    {
        partyEditPanel.gameObject.SetActive(false);
        OpenMenu();
    }
}
