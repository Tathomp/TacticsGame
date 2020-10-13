using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour {

    public TextButton butotnPrefab;
    public BoardManager boardManager;
    public ScrollListScaleableContent scrollcontent;

    int currIndex = 0;
    int maxIndex;

    Selector selector;
    InputFSM inputFSM;

    public void InitMenu(BoardManager boardManager)
    {
        this.boardManager = boardManager;

        selector = boardManager.Selector;
        inputFSM = boardManager.inputFSM;

    }

    private void OnDisable()
    {
        //menuButtons[1].Select();
        boardManager.tileSelection.ClearMovementRange();
        ResetBUttons();
    }

    public void ToggleOn()
    {
        
        currIndex = 0;
        gameObject.SetActive(true);

    }

    void ResetBUttons()
    {
        scrollcontent.CleanUp();
    }

    public void InitSelection(Actor actor)
    {
        ResetBUttons();


        //create button for movement
     
            TextButton move = Instantiate<TextButton>(butotnPrefab, scrollcontent.contentTransform);

            move.button.onClick.AddListener(delegate { Movement(actor); });
            scrollcontent.AddToList(move);
            move.ChangeText ("Move");

            move.button.interactable = actor.CanMove();
        
  

        // create button for attack
       
            bool canAttack = actor.CanAttack();
        /*
            TextButton attack = Instantiate<TextButton>(butotnPrefab, scrollcontent.contentTransform);
            attack.button.onClick.AddListener(delegate { Attack(actor); });
            scrollcontent.AddToList(attack);
            attack.ChangeText("Attack");
            attack.button.interactable = canAttack;


            TextButton generic = Instantiate<TextButton>(butotnPrefab, scrollcontent.contentTransform);
            generic.button.onClick.AddListener(delegate { GenericAbilities(actor); });
            scrollcontent.AddToList(generic);
            generic.ChangeText( "Generic Abilities");
            generic.button.interactable = canAttack;
            */

            TextButton primary = Instantiate<TextButton>(butotnPrefab, scrollcontent.contentTransform);
            primary.button.onClick.AddListener(delegate { PrimaryAbility(actor); });
            scrollcontent.AddToList(primary);
            primary.ChangeText( "Skills");
            primary.button.interactable = canAttack;



            if (actor.actorData.secondaryJob != "")
            {/*
                TextButton secondary = Instantiate<TextButton>(butotnPrefab, scrollcontent.contentTransform);
                secondary.button.onClick.AddListener(delegate { SecondaryAbility(actor); });
                scrollcontent.AddToList(secondary);
                secondary.ChangeText( Globals.campaign.GetJobsData().JobDB.GetCopy(
                actor.actorData.secondaryJob).AbilitNames );
                secondary.button.interactable = canAttack;*/


             }



        //Button for picking up an item
        TextButton items = Instantiate<TextButton>(butotnPrefab, scrollcontent.contentTransform);
        items.button.onClick.AddListener(delegate { ItemButtons(actor); });
        scrollcontent.AddToList(items);
        items.ChangeText( "Items");

        //create button for wait
        TextButton wait = Instantiate<TextButton>(butotnPrefab, scrollcontent.contentTransform);
        wait.button.onClick.AddListener(delegate { Wait(actor); });
        scrollcontent.AddToList(wait);
        wait.ChangeText( "Wait" );


        scrollcontent.AdjustContentLength();

        TextButton cancel = Instantiate<TextButton>(butotnPrefab, scrollcontent.contentTransform);
        scrollcontent.AddToList(cancel);
        cancel.ChangeText("Back");
        cancel.button.onClick.AddListener(delegate { BackOutOfActionMenu(); } );
        
    }

    
    public void BackOutOfActionMenu()
    {
        gameObject.SetActive(false);
        boardManager.inputFSM.SwitchState(new UsersTurnState(boardManager));
    }



    public void InitAltSelection(TileNode targetTile, Mission currMission)
    {
        ResetBUttons();

        //print movement range of selected unit

        //check if there's an interaction with the tile
        //
        string tileKey = targetTile.data.posX + "" + targetTile.data.posY;

        Actor actor = boardManager.turnManager.currFastest;
        string currActor = actor.actorData.GetKey();
        int distance = boardManager.pathfinding.GenerateMovementPath(actor, targetTile.data.posX, targetTile.data.posY).Count - 1;


        Debug.Log("Distance: " + distance);

        if (currMission.interactionMap.ContainsKey(tileKey))
        {
            Interaction i = currMission.interactionMap[tileKey];
            if (i.MeetsRequirement(distance, currActor))
            {
                TextButton wait = Instantiate<TextButton>(butotnPrefab, scrollcontent.contentTransform);
                wait.button.onClick.AddListener(delegate { InteractionClicked(i, currMission, tileKey); });
                scrollcontent.AddToList(wait);
                wait.ChangeText( "Interact with tile");
            }
        }

        if (targetTile.HasActor())
        {
            tileKey = targetTile.actorOnTile.actorData.GetKey();

            boardManager.tileSelection.PopulateMovementRange(boardManager.pathfinding.GetMovementRange(targetTile.actorOnTile.actorData));

            //inspect button
            TextButton inspect = Instantiate<TextButton>(butotnPrefab, scrollcontent.contentTransform);
            inspect.button.onClick.AddListener(delegate { InspectActor(targetTile.actorOnTile); });
            scrollcontent.AddToList(inspect);
            inspect.ChangeText( "Inspect");

            if (currMission.interactionMap.ContainsKey(tileKey))
            {

                Interaction i = currMission.interactionMap[tileKey];
                if (i.MeetsRequirement(distance, currActor))
                {

                    TextButton wait = Instantiate<TextButton>(butotnPrefab, scrollcontent.contentTransform);
                    wait.button.onClick.AddListener(delegate { InteractionClicked(i, currMission, tileKey); });
                    scrollcontent.AddToList(wait);
                    wait.ChangeText("Interact with actor");
                }
            }

            // we still need to spawn a button to inspect the unit 
        }

        if (scrollcontent.buttonConatiner.HasButtons())
            gameObject.SetActive(true);
        else
        {
            gameObject.SetActive(false);
            boardManager.inputFSM.SwitchState(new UsersTurnState(boardManager));
        }
    }

    public void InspectActor(Actor actor)
    {
        gameObject.SetActive(false);
        boardManager.inputFSM.SwitchState(new CharacterInfoState(boardManager, actor));

    }

    public void InteractionClicked(Interaction interaction, Mission currMission, string key)
    {
        interaction.ExecuteInteraction(currMission);

        //We'll remove the interaction
        //Also move back to the user turn state
        currMission.interactionMap.Remove(key);
        boardManager.inputFSM.SwitchState(new UsersTurnState(boardManager));

        gameObject.SetActive(false);
    }


    public void AdjustMenu(int x)
    {
        currIndex += x;

        if(currIndex < 0)
        {
            currIndex = maxIndex;
        }
        else if(currIndex > maxIndex)
        {
            currIndex = 0;
        }
        scrollcontent.buttonConatiner.SelectButton(currIndex);
    }

    public void Selection()
    {
        scrollcontent.buttonConatiner.ClickSelectedButton(currIndex);
    }


    void Movement(Actor actor)
    {
        inputFSM.SwitchState(new MoveSelectionState(boardManager, actor));
    }


    void Wait(Actor actor)
    {
        //boardManager.inputFSM.SwitchState(new SelectWaitDirectionState(actor));
        actor.Wait();
        Globals.GetBoardManager().turnManager.CalculateFastest();
        gameObject.SetActive(false);

        /*
        actor.Wait();
        boardManager.turnManager.CalculateFastest();
        inputFSM.SwitchState(new UsersTurnState(boardManager));*/
    }


    void Attack(Actor actor)
    {
        inputFSM.SwitchState(new AbilityInUseState(boardManager, actor, boardManager.pathfinding.GetTileNode(actor), Globals.campaign.contentLibrary.skillDatabase.GetCopy("attack_skill")));
        //inputFSM.SwitchState(new AttackInputState(boardManager, actor));
        gameObject.SetActive(false);
    }

    void PrimaryAbility(Actor actor)
    {
        boardManager.inputFSM.SwitchState(new AbilitySelectState(
            boardManager, actor,
            ToIUseableSList(actor.actorData.JobDataState.GetAllLearnedSkills(
                actor.actorData.race, 
                actor.actorData.primaryJob, 
                actor.actorData.secondaryJob
            
            ))));
    }

    void SecondaryAbility(Actor actor)
    {
        boardManager.inputFSM.SwitchState(new AbilitySelectState(
        boardManager, actor,
        ToIUseableSList(actor.actorData.JobDataState.GetALlLearnedSkillsForJob(actor.actorData.secondaryJob))));
    }

    void GenericAbilities(Actor actor)
    {
        List<Skill> temp = actor.actorData.JobDataState.GetTalentList("generic_skills", new List<Skill>());
        temp = actor.actorData.JobDataState.GetTalentList(actor.actorData.race,temp);


        boardManager.inputFSM.SwitchState(new AbilitySelectState(
        boardManager, actor,
        ToIUseableSList(temp)));
    }


    void ItemButtons(Actor actor)
    {
        boardManager.inputFSM.SwitchState(new AbilitySelectState(
        boardManager, actor,
        actor.actorData.inventory.GetAllUseableItems()));
    }


    List<IUseable> ToIUseableSList(List<Skill> skills)
    {
        List<IUseable> useables = new List<IUseable>();

        foreach (Skill skill in skills)
        {
            useables.Add(skill);
        }

        return useables;
    }
}
