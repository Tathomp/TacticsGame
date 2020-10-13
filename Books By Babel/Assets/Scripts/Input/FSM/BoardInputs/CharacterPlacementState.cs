using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: Delete class

public class CharacterPlacementState : BoardInputState
{
    public Party party;

    BattleUIManager characterSelection;

    ActorData currActor;

    public CharacterPlacementState(BoardManager boardManager)
        : base(boardManager)
    {
        party = boardManager.party;
        //toggle on ui for character select
        characterSelection = boardManager.ui;
    }



    public override void EnterState()
    {

       //S characterSelection.PopulateSelection(party);

        boardManager.tileSelection.ClearAvaliable();
        boardManager.tileSelection.PopulatePlacement(boardManager.currMap.playerSpawnLocations);

    }


    public override void ExitState()
    {
        characterSelection.CloseSelection();
        //should also remove the listeners on the buttons
    }


    public override void ProcessInput()
    {
        //i dont think this is needed any more
       
    }
}
