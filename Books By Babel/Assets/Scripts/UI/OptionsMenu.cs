using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {

    public BoardManager boardManager;
    public HotkeyMenu hotkeymenu;
    public SaveLoadPanel panel;

	public void  ToggleOn()
    {
        gameObject.SetActive(true);
    }


    public void ToggleOff()
    {
        hotkeymenu.ToggleOff();
        gameObject.SetActive(false);
    }


    public void SaveGame()
    {
        panel.TurnOnSavePanel();
    }

    public void LoadSaveGame()
    {
        panel.TurnOnLoadPanel();
    }


    public void HotkeysMenu()
    {
        boardManager.inputFSM.SwitchState(new HotkeyMenuState(boardManager, hotkeymenu));
    }


    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
