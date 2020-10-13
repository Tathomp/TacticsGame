using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChangeTargetPositionSkillEffect : SkillEffect
{
    int maxRange;
    bool push; //if true push away, if false pull towards


    public ChangeTargetPositionSkillEffect(int range, bool push)
    {
        this.maxRange = range;
        this.push = push;
    }

    public override void ActorEffect(Combat combat, Actor source, TileNode target)
    {

        

        TileNode dest;
        LineTarget line = new LineTarget(new List<string> { "wall" }, true);

        if(target.HasActor() == false)
        {
            return;
        }


        if(push)
        {
            dest = DestTile(source.GetPosX(), source.GetPosY(), target.data.posX, target.data.posY);

            List<TileNode> path = line.TargetTiles(target.actorOnTile, dest);

            if(path.Count == 0)
            {
                return;
            }

            //We'll just use the code for the teleport unit skill
            TeleportUnitSkillEffect t = new TeleportUnitSkillEffect();

            t.ActorEffect(combat, target.actorOnTile , path[path.Count-1]);
        }
        else
        {
            //push
            dest = DestTile(target.data.posX, target.data.posY, source.GetPosX(), source.GetPosY());

            List<TileNode> path = line.TargetTiles(target.actorOnTile, dest);

            if (path.Count == 0)
            {
                return;
            }

            //We'll just use the code for the teleport unit skill
            TeleportUnitSkillEffect t = new TeleportUnitSkillEffect();

            t.ActorEffect(combat, target.actorOnTile, path[path.Count - 1]);
        }




    }

    public override SkillEffect Copy()
    {
        return new ChangeTargetPositionSkillEffect(maxRange, push);
    }


    public TileNode DestTile(int startX, int startY, int destX, int destY)
    {
        int x = startX;
        int y = startY;

        if (startY < destY && startX - destX == 0) // 
        {
            //target should be pushed directly above
            x = 0;
            y = 1;
        }
        else if (startY > destY && startX - destX == 0) // 
        {
            //target should be pushed directly below

            x = 0;
            y = -1;
        }
        else if (startX < destX && startY - destY == 0) // 
        {
            //target should be pushed directly right
            x = 1;
            y = 0;
        }
        else if (startX > destX && startY - destY == 0) // 
        {
            //target should be pushed directly left
            x = -1;
            y = 0;
        }
        else if (startX < destX && startY < destY)
        {
            //right and up
            x = 1;
            y = 1;
        }
        else if (startX < destX && startY > destY)
        {
            //right and down
            x = 1;
            y = -1;
        }
        else if (startX > destX && startY < destY)
        {
            //left and up
            x = -1;
            y = 1;
        }
        else if (startX > destX && startY > destY)
        {
            //left and down
            x = -1;
            y = -1;
        }
        int temprange = maxRange -1;

        int newX = destX + x * temprange;
        int newY = destY + y * temprange;
        TileNode validDest = null;


        while (temprange >= 0 && Globals.GetBoardManager().pathfinding.InRange(newX, newY) == false)
        {
            temprange--;
            newX = destX + x * temprange;
            newY = destY + y * temprange;
        }


        if(push == false)
        {
            // put corner cases of map edges here
        }

        validDest = Globals.GetBoardManager().pathfinding.GetTileNode(newX, newY);

        return validDest;
    }
}
