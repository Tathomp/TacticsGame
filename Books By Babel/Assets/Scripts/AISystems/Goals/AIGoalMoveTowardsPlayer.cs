using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIGoalMoveTowardsPlayer : AIGoal
{
    public override IEnumerator CalculateActions(List<AIAction> validActions, BoardManager bm, Actor ai)
    {


        MapCoords goal = PosOfClosestEnemy(ai, bm);
        //need to associate some kind of scoring for this
        // currently it's just going to be one so that it's alway valid for when there's not a target, but the
        // ai will always perform some action if it can
        
        MoveToPlayerAction move = new MoveToPlayerAction(ai, goal);

        move.SetScore(.2f); //should furth elaborate on this

        validActions.Add(move);

        yield return null;

    }
}
