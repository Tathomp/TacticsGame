using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLocationInputState : WorldMapInputState
{
    private WorldMapLocationMenu locationMenu;

    public WorldLocationInputState(WorldMapManager manager, WorldMapUIManager ui)
        : base(manager, ui)
    {
    }

    public override void EnterState()
    {
        this.locationMenu = ui.worldMapLocationMenu;
    }

    public override void ExitState()
    {

    }

    public override void ProcessInput()
    {
        if (inputHandler.CancelButtonPressed())
        {
            // toggle of worldmaplocation
            locationMenu.ToggleOff();
            inputFSM.SwitchState(new NavigateWorldInputState(manager, ui));
        }
    }

}
