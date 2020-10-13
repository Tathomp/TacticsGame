using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: SAFE TO DELETE

public class PlaceUnitState : BoardInputState
{
    int x, y;
    Party party;
    PartyListPanel actorList;

    ExamineBattlefieldInputState examinStae;

    public PlaceUnitState(BoardManager bm, PartyListPanel list, ExamineBattlefieldInputState state, int x, int y) : base(bm)
    {
        this.x = x;
        this.y = y;

        party = bm.party;
        actorList = list;

        this.examinStae = state;
    }

    public override void EnterState()
    {
        TextButton b;

        foreach (ActorData data in party.partyCharacter)
        {
            if (data.selected == false)
            {
                b = actorList.PopulateList(data);

                b.button.onClick.AddListener(delegate { ActorClicked(data); });
            }
        }
    }

    public override void ExitState()
    {
        actorList.ToogleOff();
    }

    public override void ProcessInput()
    {

    }

    public void ActorClicked(ActorData data)
    {
        boardManager.spawner.GenerateActor(data, boardManager, x, y);
        data.selected = true;

        boardManager.inputFSM.SwitchState(examinStae);
    }


    /*
    public PlaceUnitState(BoardManager boardManager, ActorData CurrData) 
        : base(boardManager)
    {
        actorDataToPlace = CurrData;
        this.boardManager = boardManager;
        actors = boardManager.spawner;

        if (boardManager.Selector == null)
        {
            boardManager.Selector = new Selector(boardManager);
        }

        charSelectionPanel = boardManager.ui.characterSelectionInfoPanel;
        selector = boardManager.Selector;
        currentMission = boardManager.currentMission;
        vaidplacement = boardManager.currMap.playerSpawnLocations;
    }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {
        charSelectionPanel.UpDatePlacementNumber(boardManager.party.NumberOfSelected(), currentMission.maxUnitsAllowed);

    }

    public override void ProcessInput()
    {
        selector.ProccessInput(inputHandler);

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Globals.MouseToWorld();

            if (pos != Vector2.negativeInfinity 
                && (vaidplacement.Contains(new MapCoords { X = (int)pos.x, Y = (int)pos.y })) 
                && boardManager.pathfinding.tiles[(int)pos.x,(int) pos.y].actorOnTile == null )
            {
                PlaceUnit(pos.x, pos.y);
            }

        }
        else if(inputHandler.SelectionPressed())
        {
            PlaceUnit(selector.mapPosX, selector.mapPosY);
        }
    }

    void PlaceUnit(int x, int y)
    {
        actors.GenerateActor(actorDataToPlace, boardManager, x, y);

        actorDataToPlace.selected = true;

        inputFSM.SwitchState(new CharacterPlacementState(boardManager));

        if (boardManager.party.NumberOfSelected() == currentMission.maxUnitsAllowed)
        {
            //this is the part that transitions us to the start of the fight
            inputFSM.SwitchState(new UsersTurnState(boardManager));
            boardManager.turnManager.CalculateFastest();
        }
        else
        {
            
            inputFSM.SwitchState(new CharacterPlacementState(boardManager));
        }
    }

    void PlaceUnit(Vector3 v3)
    {
        PlaceUnit(v3.x, v3.y);
    }

    void PlaceUnit(float x, float y)
    {
        PlaceUnit((int)x, (int)y);
    }
    */
}
