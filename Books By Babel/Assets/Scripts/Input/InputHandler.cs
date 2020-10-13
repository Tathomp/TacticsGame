using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    public HotKeys hotkeys;

    public InputHandler()
    {
        hotkeys = new HotKeys();
        hotkeys.GenerateDefaultKeys();
    }

    public bool IsKeyPressed(KeyBindingNames nameOfKey)
    {
        if(hotkeys.hotkeys.ContainsKey(nameOfKey))
        {
            return Input.GetKeyDown(hotkeys.hotkeys[nameOfKey]);
        }
        else
        {
            Debug.Log("Key not assigned: " + nameOfKey);
            return false;
        }

    }
    public bool CancelButtonPressed()
    {
        return IsKeyPressed(KeyBindingNames.Cancel) || Input.GetMouseButtonDown(1);
    }

    public bool SelectionPressed()
    {
        return MouseButtonClicked() || Input.GetMouseButtonDown(0);
    }

    public bool MouseButtonClicked()
    {
        return Input.GetMouseButton(0);
    }
}
