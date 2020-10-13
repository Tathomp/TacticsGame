using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapMainMenuInputState : WorldMapInputState
{
    WorldMapMenu menu;

    public WorldMapMainMenuInputState(WorldMapManager manager, WorldMapUIManager ui, WorldMapMenu menu) 
        : base(manager, ui)
    {
        this.menu = menu;
    }

    public override void EnterState()
    {
        menu.ToggleOn();

    }

    public override void ExitState()
    {
        menu.ToggleOff();

    }

    public override void ProcessInput()
    {
        if (inputHandler.CancelButtonPressed())
        {
            // toggle of worldmaplocation
            inputFSM.SwitchState(new NavigateWorldInputState(manager, ui));
        }
    }
}
