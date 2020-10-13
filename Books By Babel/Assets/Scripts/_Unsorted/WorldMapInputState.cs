using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldMapInputState : InputState
{
    protected WorldMapManager manager;
    protected WorldMapUIManager ui;

    public WorldMapInputState(WorldMapManager manager, WorldMapUIManager ui)
    {
        this.manager = manager;
        this.ui = ui;
    }
}
