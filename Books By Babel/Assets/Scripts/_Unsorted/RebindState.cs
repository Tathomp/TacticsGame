using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebindState : BoardInputState
{
    KeyBindingNames currBinding;
    HotKeys hotkeys;
    HotkeyMenu hotkeyMenu;

    public RebindState(BoardManager boardManager, KeyBindingNames currBinding) 
        : base(boardManager)
    {
        this.currBinding = currBinding;
    }

    public override void EnterState()
    {
        hotkeyMenu = boardManager.ui.optionsMenu.hotkeymenu;
        hotkeys = inputHandler.hotkeys;
    }

    public override void ExitState()
    {
        hotkeyMenu.ClearMenu();
    }

    public override void ProcessInput()
    {
        if(Input.anyKeyDown)
        {

            Array k =  Enum.GetValues(typeof(KeyCode));

            foreach (KeyCode kc in k)
            {
                if(Input.GetKeyDown(kc))
                {

                    Array n = Enum.GetValues(typeof(KeyBindingNames));

                    foreach (KeyBindingNames name in n)
                    {
                        if (hotkeys.hotkeys.ContainsKey(name))
                        {
                            if (hotkeys.hotkeys[name] == kc)
                            {
                                hotkeys.hotkeys[name] = KeyCode.None;
                                break;
                            }
                        }
                    }

                    hotkeys.hotkeys[currBinding] = kc;


                    boardManager.inputFSM.SwitchState(new HotkeyMenuState(boardManager, hotkeyMenu));
                    return;

                }
            }

        }
    }
}
