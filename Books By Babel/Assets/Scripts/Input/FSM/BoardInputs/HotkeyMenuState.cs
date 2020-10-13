using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotkeyMenuState : BoardInputState
{
    HotkeyMenu hotkeyMenu;
    HotKeys hotkeys;


    public HotkeyMenuState(BoardManager boardManager, HotkeyMenu menu) 
        : base(boardManager)
    {
        hotkeyMenu = menu;
    }

    public override void EnterState()
    {
        hotkeys = inputHandler.hotkeys;
        hotkeyMenu.InitList(hotkeys);
    }

    public override void ExitState()
    {
    }

    public override void ProcessInput()
    {
        if(inputHandler.IsKeyPressed(KeyBindingNames.Cancel))
        {
            hotkeyMenu.ToggleOff();
            inputFSM.SwitchState(new OptionsMenuState(boardManager));
        }
 
    }
}
