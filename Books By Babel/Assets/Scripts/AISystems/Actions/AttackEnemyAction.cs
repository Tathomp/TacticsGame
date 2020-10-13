using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackEnemyAction : AIAction
{
    TileNode skillTarget, moveTarget;
    Actor source;
    IUseable skillToUse;


    

    public AttackEnemyAction(TileNode targetTile, TileNode moveTarget, Actor source, IUseable skillToUse)
    {
        this.skillTarget = targetTile;
        this.moveTarget = moveTarget;
        this.source = source;
        this.skillToUse = skillToUse;

    }


    public override IEnumerator ExecuteAction(Actor source, BoardManager bm)
    {
        bm.Selector.MoveTo(moveTarget.data.posX, moveTarget.data.posY);
        //move to the target tile
        int pathLeng = MoveToTarget(source, bm, moveTarget.data.posX, moveTarget.data.posY);

        // use skill
        Combat c = new Combat(source);

        yield return new WaitForSeconds(waitTime * pathLeng);

        AnimationData data = AnimationData.NewAntionData(skillToUse,
            Globals.GetBoardManager().pathfinding.GetTileNode(source),
            skillTarget);


        bm.tileSelection.ClearAllRange();
        //AttackRange(bm, source);

        bm.tileSelection.PopulateMovementRange(
            bm.pathfinding.GetSkillRange(skillToUse, source, source.GetPosX(), source.GetPosY()));
        bm.ui.SkillInUseName.InitLabel(skillToUse.GetName());


        bm.Selector.MoveTo(skillTarget.data.posX, skillTarget.data.posY);
        yield return new WaitForSeconds(.75f);
        bm.tileSelection.ClearAllRange();

        //targeted tiles
        List<TileNode> nodesInRange = skillToUse.GetTargetedTiles(source, skillTarget);
        bm.tileSelection.PopulateAttackRange(nodesInRange);
        yield return new WaitForSeconds(.75f);

        //targeted tiles cleared
        bm.tileSelection.ClearAllRange();

        bm.ui.SkillInUseName.ToggleOff();
        c.animationDatas.Add(data);
        
        ///HACK
        ///This is hacked as fuck

        source.Wait();
        bm.inputFSM.SwitchState(new PlayingAnimationInputState(bm, c));
        bm.turnManager.CalculateFastest();
        /*
        c.animationDatas.Add(data);
         * 
        c.UseSkill();
        c.PayCostsCDAttack();
        source.Wait();


        bm.turnManager.CalculateFastest(); //this is when the state machine changes
        */


    }

    public void AttackRange(BoardManager bm, Actor currentActor)
    {
       bool[,]  range = bm.pathfinding.ValidBFS(
                skillToUse.GetMaxRange(currentActor),
                skillToUse.GetMinRange(currentActor),
                moveTarget.data.posX,
                moveTarget.data.posY,
                ((Skill)skillToUse).targetType.immpassableTiles,
                ((Skill)skillToUse).targetType.stopOnOccupied);

        //run range through a tilenode graph
        // we could just move this to skill
        // and pass a tilenode as the parameter
        if (skillToUse.GetTargetFiltering() != null)
        {
            for (int x = 0; x < range.GetLength(0); x++)
            {
                for (int y = 0; y < range.GetLength(1); y++)
                {
                    if (range[x, y])
                    {
                        TileNode node = bm.pathfinding.GetTileNode(x, y);

                        if (skillToUse.FilterTileNode(currentActor, node) == true)
                        {
                            range[x, y] = false;
                        }
                    }
                }
            }
        }


        bm.tileSelection.PopulateAttackRange(range);


    }

}

public class EmptyAction : AIAction
{
    public EmptyAction() : base()
    {

    }

    public override IEnumerator ExecuteAction(Actor source, BoardManager bm)
    {
        throw new System.NotImplementedException();
    }
}