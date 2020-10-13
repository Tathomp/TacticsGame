using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingAnimationInputState : BoardInputState
{
    private Coroutine currCoroutine;

    private List<AnimationObject> animations;

    private Combat combat;

    public PlayingAnimationInputState(BoardManager boardManager, Combat combat)
        : base(boardManager)
    {
        this.combat = combat;

        animations = new List<AnimationObject>();
    }

    public override void EnterState()
    {
        boardManager.tileSelection.ClearAllRange();
        currCoroutine =  boardManager.StartCoroutine(PlayAnimations());
    }

    public override void ExitState()
    {

    }

    public override void ProcessInput()
    {
       if(inputHandler.IsKeyPressed(KeyBindingNames.Select) ||
            inputHandler.IsKeyPressed(KeyBindingNames.Cancel))
        {
            // Cancel out
            CancelAnimations();
        }
    }


    IEnumerator PlayAnimations()
    {
        bool calculateCombat = true; //basically making it so we still calculate combat after
        
        foreach (AnimationData item in combat.animationDatas)
        {
            List<string> ids = item.skillUsed.GetAnimControllerID();

            for (int i = 0; i < ids.Count; i++)
            {
                calculateCombat = false;

                // SO we think this is the problem currently
                //


                AnimationObject animObj = Globals.GenerateAnimationObject(ids[i], item.sourceNode, item.DestNode);

                animObj.InitAnimatorObject(ids[i], item.DestNode.data.posX, item.DestNode.data.posY, false, this);
                animations.Add(animObj);

                if (i == ids.Count - 1)
                {
                    animObj.lastObj = true;
                }

                SFXController.sfxInstance.ChangeSong(item.skillUsed.GetSFXKey());

                yield return new WaitForSeconds(0.5f);
            }
        }
        
        if(calculateCombat)
        {
            ApplyCombatDamage();
        }



        /*
        foreach (string animation in animationIDs)
        {

            AnimationObject animObj = AnimationObject.SpawnAnimationObject(startNode);
            animObj.InitAnimatorObject(animation, targetNode);

            

            yield return new WaitForSeconds(0.5f);
        }

        ApplyCombatDamage();
        */
    }


    private void CancelAnimations()
    {
        boardManager.StopCoroutine(currCoroutine);


        foreach (AnimationObject obj in animations)
        {
            GameObject.Destroy(obj.gameObject);
            GameObject.Destroy(obj);
        }


        inputFSM.SwitchState(new UsersTurnState(boardManager));
    }

    public void ApplyCombatDamage()
    {

        // CombatManager.InitCombat(startNode.actorOnTile, targetNode, skillInUse);
        //inputFSM.SwitchState(new UsersTurnState(boardManager));

        Globals.GetBoardManager().StartCoroutine(ApplyCombat());


    }

    IEnumerator ApplyCombat()
    {
        combat.UseSkill();
        combat.PayCostsCDAttack();
        yield return new WaitForSeconds(0.5f);

        //boardManager.turnManager.CalculateFastest();
        if(combat.source.actorData.controller.PlayerControlled())
        {
            boardManager.inputFSM.SwitchState(new ActionMenuState(boardManager, combat.source));

        }
        else
        {
            // it's the AI's turn so we're still doing this i guess
            // I think this will break things when we get ot the point where the ai can attack then move
            // so good luck future me
            boardManager.inputFSM.SwitchState(new UsersTurnState(boardManager));

        }

        yield return new WaitForSeconds(0.5f);

    }
}
