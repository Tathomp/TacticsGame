using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInUseState : BoardInputState
{
    IUseable currentSkill;

    TileSelction selection;
    Selector selector;
    Pathfinding pathfinding;
    Actor currentActor;

    Combat combat;
    PreviewManager previewManager;
    AbilityInUseLabel label;

    bool[,] range;
    TileNode startNode;


    public AbilityInUseState(BoardManager boardManager, Actor currentActo, TileNode startNode, IUseable currentSkil, Combat c = null) 
        : base(boardManager)
    {
        this.currentSkill = currentSkil;

        selection = boardManager.tileSelection;
        selector = boardManager.Selector;
        pathfinding = boardManager.pathfinding;
        this.currentActor = currentActo;
        this.label = boardManager.ui.SkillInUseName;

        previewManager = boardManager.ui.previewmanager;
        combat = c;
        this.startNode = startNode;
    }


    public override void EnterState()
    {
        //show range
        range = pathfinding.GetSkillRange(currentSkill, currentActor, currentActor.GetPosX(), currentActor.GetPosY());
        //filter that

        //back to bool

        //pass to pupulation

        selection.PopulateMovementRange(range);

        label.InitLabel(currentSkill.GetName());        
    }


    public override void ExitState()
    {
        boardManager.ui.CleanUpPreview();
        selection.ClearAoE();
        label.ToggleOff();
    }


    public override void ProcessInput()
    {
        //display current skill range;

        if (range[selector.mapPosX, selector.mapPosY])
        {

            if (selector.ChangedPosition())
            {
                List<TileNode> nodes = currentSkill.GetTargetedTiles(currentActor, pathfinding.GetTileNode(selector.mapPosX, selector.mapPosY));

                if (nodes == null)
                {
                    return;
                }

                selection.PopulateSkillEffect(nodes);
                previewManager.ClearPreview();

                TileNode node = pathfinding.GetTileNode(selector.mapPosX, selector.mapPosY);

                if (combat == null)
                {
                    combat = new Combat(currentActor);

                }
                else
                {
                    combat.source = currentActor;
                }

                AnimationData data = AnimationData.NewAntionData(currentSkill, boardManager.pathfinding.GetTileNode(currentActor), node);

                previewManager.InitPreview(combat.DisplayCombatPreview(data));

                selector.posChanged = false;
            }
        }

        selector.ProccessInput(inputHandler);
     
       
        
        if (inputHandler.IsKeyPressed(KeyBindingNames.Select) || Input.GetMouseButtonDown(0))
        {
            if (range[selector.mapPosX, selector.mapPosY])
            {
                // We could move this stuff to the animation state to make sure that the 
                // animations line up with the animations
                //
                TileNode node = pathfinding.GetTileNode(selector.mapPosX, selector.mapPosY);
                AnimationData data = new AnimationData();
                data.skillUsed = currentSkill;
                data.DestNode = node;
                data.sourceNode = startNode;
                combat.animationDatas.Add(data);

                //combat.PoopulateCombat(data);

                if(currentSkill is Skill)
                {
                    Skill temp = currentSkill as Skill;

                    if(temp.nextSkill != null)
                    {
                        selection.ClearMovementRange();

                        if(temp.nextSkill.useActorSource)
                        {
                            node = pathfinding.GetTileNode(currentActor);
                        }
                        temp = Globals.campaign.contentLibrary.skillDatabase.GetData(temp.nextSkill.skillKey);

                        inputFSM.SwitchState(new AbilityInUseState(boardManager, currentActor, node, temp, combat));
                    }
                    else
                    {
                        PlayAnimations();
                    }
                }
                else
                {
                    PlayAnimations();
                }

            }
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            previewManager.ShiftUp();
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            previewManager.ShfitDown();
        }
        else if(inputHandler.IsKeyPressed(KeyBindingNames.Cancel) || Input.GetMouseButtonDown(1))
        {

            inputFSM.SwitchState(new UsersTurnState(boardManager));

           // selector.MoveTo(currentActor.GetPosX(), currentActor.GetPosY());

        }

    }


    private void PlayAnimations()
    {        
        inputFSM.SwitchState(new PlayingAnimationInputState(boardManager,
            combat));

    }
}
