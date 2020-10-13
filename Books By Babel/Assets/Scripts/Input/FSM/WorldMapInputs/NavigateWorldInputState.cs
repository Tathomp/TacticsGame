using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateWorldInputState : InputState
{
    private WorldMapManager manager;
    private WorldMapUIManager uiManager;

    private WorldMapSelector selector;
    private WorldAvatar playerAvatar;
    private WorldMap currentMap;

    public NavigateWorldInputState(WorldMapManager manager, WorldMapUIManager ui)
    {
        this.manager = manager;
        uiManager = ui;
    }

    public override void EnterState()
    {
        this.selector = manager.worldMapelectorInstance;
        this.playerAvatar = manager.playerAvatarGO;
        this.currentMap = manager.currWorldMap;

        selector.UpdateLocationNode();
    }

    public override void ExitState()
    {

    }

    public override void ProcessInput()
    {

        if (inputHandler.IsKeyPressed(KeyBindingNames.Select) || Input.GetMouseButtonDown(0))
        {

            selector.ProcessInput(inputHandler);

            if(selector.SelectorHasMoved() == false)
            {
                return;
            }

            if (selector.CurrentLocationNode != null)
            {

                //switch to a menu state here
                inputFSM.SwitchState(new WorldLocationInputState(manager, uiManager));
                uiManager.ToggleOnWorldMapLocationMenu(selector.CurrentLocationNode);

                if(selector.mapPosX == playerAvatar.position.X &&
                    selector.mapPosY == playerAvatar.position.Y)
                {

                }
                else
                {
                    GetPath();

                    //playerAvatar.MoveTo(selector.mapPosX, selector.mapPosY);
                }
            }
            else
            {
                Debug.Log("there is NOT a location here");

            }
        }
        else if(inputHandler.CancelButtonPressed())
        {
            inputFSM.SwitchState(new WorldMapMainMenuInputState(manager, uiManager, uiManager.menu));
        }
    }

    private void GetPath()
    {
        List<LocationNode> nodes = manager.GeneratePath(playerAvatar.position.X, 
            playerAvatar.position.Y, selector.mapPosX, selector.mapPosY);

        foreach (LocationNode node in nodes)
        {
            Debug.Log("Path: " + node.coords.X + ", " + node.coords.Y);
        }


        playerAvatar.position = new MapCoords(selector.mapPosX, selector.mapPosY);

        playerAvatar.MoveAlongPath(nodes);

    }
}
