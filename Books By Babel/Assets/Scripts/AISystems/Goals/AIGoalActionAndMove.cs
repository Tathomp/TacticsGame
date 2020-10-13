using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class AIGoalActionAndMove : AIGoal
{
    float baseMovement = .2f;

    public override IEnumerator CalculateActions(List<AIAction> validActions, BoardManager gm, Actor ai)
    {

        if (ai.CanAttack())
        {

            List<TileNode> movementNodes = GetTileNodeMovementRange(gm, ai);

            /// Store the actual position of the ai so that we can restore it later
            int originalX = ai.GetPosX();
            int originalY = ai.GetPosY();


            //Just going to get skills for now, expand to equipment and inventory
            //
            List<Skill> avaliableSkills = ai.actorData.JobDataState.GetAllSkillsOffCoolDown(ai);



            //stress test more skills
            // 7 * 3 = 21 skills to consider
            // a little inaccurate since coping this many skills would take more time than just processing them but ya know
            //
            Debug.Log("Skill count: " + avaliableSkills.Count);

            // So we're going to cycle through the possible movement options
            // use that as the source to find the range for each target type
            // use each node in that range for the target of each type

            //foreach tilenode                                   /pretend we moved there
            //foreach skill                                      /pretend we used that skill from there to find a rance
            //foreach tilenode in range                         /pretend we used the skill
            //record the combat data for that skill used

            int n = 0;

            //cycle through every place we can move
            foreach (TileNode movementOption in movementNodes)
            {
                // Basically we need to actually change the positional data of the ai to
                // accurately predict the best ability to use

                //We should also be checking if the unit is calculating from the area tha tit's starting from
                if(movementOption.data.posX == originalX && movementOption.data.posY == originalY)
                {
                    Debug.Log("Calculating from starting position");
                }


                ai.actorData.gridPosX = movementOption.data.posX;
                ai.actorData.gridPosY = movementOption.data.posY;

                //this might serve some purpose that i don't understand
               // yield return null;


                //cycle through every skill that can be used
                foreach (Skill skillOption in avaliableSkills)
                {
                    //find the range of the skill
                    // Get a list of tiles associated with that skill's range
                    //
                    List<TileNode> nodesInRange = gm.pathfinding.GetNodes(
                        gm.pathfinding.ValidBFS(skillOption, movementOption, ai
                        ));

                    //gm.tileSelection.PopulateAttackRange(nodesInRange);

                    foreach (TileNode nodeInSkillrange in nodesInRange) //should be valid targets for the skill based on when it's being used from
                    {

                        /// So now we are cycling through a list of valid targets, 
                        /// so we'll use the skill on this target and calculate the score
                        ///

                        //yield return null;
                        Combat testCombat = new Combat(ai);


                        AnimationData animationData = AnimationData.NewAntionData(skillOption, gm.pathfinding.GetTileNode(ai), nodeInSkillrange);
                        testCombat.animationDatas.Add(animationData);
                        testCombat.PoopulateCombat(animationData);

                        // So now we have a combat damage map we can use to determine the score and create an action




                        //check to make sure there is an actor here and that the actor is player controlled
                        // so we dont just randomaly blow up our own ppl
                        // further work: give negative score if allied units would be damaged?

                        AttackEnemyAction action = new AttackEnemyAction(nodeInSkillrange, movementOption, ai, skillOption);


                        CalculateDamageScore(testCombat, action);

                        CalcuateTileCost(movementOption, ai, action);
                        CalculateBuffScore(testCombat, action);
                        CacluateSummonScore(testCombat, action);

                        validActions.Add(action); //make this is like a heap or something later

                        /*
                        if (score != 0)
                        {
                            Debug.Log("Target Node: " + nodeInSkillrange.data.posX + ", " + nodeInSkillrange.data.posY +
                            ".  Skilled Used: " + skillOption.GetKey() +
                            ". Move Target: " + movementOption.data.posX + ", " + movementOption.data.posY +
                            "Score: " + action.GetScore());
                        }*/


                    }
                }
            }

            //Debug.Log("Time compexity kinda: " + n);

            //POTENTIAL OPTIMIZATION: we theoretically know the ranges of skills and the aoe of there target
            //types so maybe we can just do math to cut out tiles where we know there wont be anything

            //Set the position back to where the actor ACTUALLY is
            ai.actorData.gridPosX = originalX;
            ai.actorData.gridPosY = originalY;

            //yield return null;
        }

        yield return null;

    }

    void CacluateSummonScore(Combat testCombat, AIAction action)
    {
        foreach (CombatNode node in testCombat.actorDamageMap)
        {
            if(node is SummonCombatNode)
            {
                action.SetScore(0.6f);
            }



        }
    }

    void CalculateBuffScore(Combat testCombat, AIAction action)
    {
        int unitHits = 0;
        int score = 0;

        foreach (CombatNode node in testCombat.actorDamageMap)
        {
            if(node is BuffCombatNode)
            {
                BuffCombatNode n = node as BuffCombatNode;

                //lets figure out how to establish if the buff is positive or negative
                if(
                   ( node.target.ActorsController().PlayerControlled() == true && n.buffToApply.IsBuff == false)
                    ||
                    (node.target.ActorsController().PlayerControlled() == false && n.buffToApply.IsBuff == true)
                    )
                {
                    //This means we're either buffing our allies or debuffing our enemies

                    if(n.target.actorData.buffContainer.CanBuffBeApplied(n.buffToApply))
                    {
                        action.SetScore(0.5f);

                    }

                }



            }
        }
    }

    void CalculateDamageScore(Combat testCombat, AIAction action)
    {
        int unitHit = 0;
        float score = 0;

        foreach (CombatNode cnode in testCombat.actorDamageMap)
        {
            if (cnode is HealthChangeCombatNode)
            {
                HealthChangeCombatNode node = (HealthChangeCombatNode)cnode;

                if (node.target != null)
                {
                    score = (node.target.GetCurrentStats(StatTypes.Health) + node.ChangeHealth) /
                        ((float)node.target.GetMaxStats(StatTypes.Health));

                    //This scoring system breaks with aoe abilities
                    //The last will determine the overall score
                    //we basically have to flip this idk it's dumb hopefully i explain latter
                    if (node.target.actorData.controller.PlayerControlled())
                    {
                        unitHit++;

                        score = (1 - score) + (unitHit * .1f);
                    }
                    else
                    {
                        //if an allied unit is hit, maybe we should subtract the number of units hit to asjust more accurately?

                        score = -.1f; // we'll just make it so that enemies try to never attack an ally
                    }

                    action.SetScore(score);
                }
                else
                {
                    // Debug.Log("node " + node.targetedTile.data.posX + ", " + node.targetedTile.data.posY + " didn't have any actor targets for some reason?");
                }
            }
        }

    }

    void CalcuateTileCost(TileNode node, Actor ai, AIAction action)
    {
        foreach (TileEffect effect in node.tileEffects)
        {
            foreach (TileEffectComponent component in effect.turn)
            {
                if(component.GetSCore(ai, node) != 0)
                {
                    action.SetScore(baseMovement + component.GetSCore(ai, node));
                   // GameObject.Find("Debug").GetComponent<DebugDisplay>().UpDateList(action.uncondensedCosted);
                }
            }
        }
    }


    void CalulateBuffScore(Combat testCombat, AIAction action, ref float score)
    {

    }
}
