using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapMenu : MonoBehaviour
{

    public PartyEditPanel partyPanel;
    public SaveLoadPanel saveGamePanel;


    public void ToggleOn()
    {
        gameObject.SetActive(true);
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }


    public void SaveGame()
    {
        /*SaveStateWorldMap state = new SaveStateWorldMap(Globals.campaign);

        SaveLoadManager.SaveCampaignProgress(state, "Save Slot 1");

        Quit();*/

        ToggleOff();
        saveGamePanel.TurnOnSavePanel();
    }


    public void LoadGame()
    {
        ToggleOff();
        saveGamePanel.TurnOnLoadPanel();
    }
    //Quit WIthout saving
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");

    }


    //Init party menu
    public void DisplayPartyMenu()
    {
        this.gameObject.SetActive(false);
        //might need to switch input states
        partyPanel.InitPanel();
    }
}
