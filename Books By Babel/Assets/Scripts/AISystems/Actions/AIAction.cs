using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AIAction
{
    protected float waitTime = .3f;

    public List<float> uncondensedCosted;

    public AIAction()
    {
        uncondensedCosted = new List<float>();

    }

    public float GetScore()
    {

        if(uncondensedCosted.Count == 1)
        {
            return uncondensedCosted[0];
        }
        else if(uncondensedCosted.Count == 0)
        {
            return 0f;
        }

        ///////
        float sum = 0f;

        foreach (float fl in uncondensedCosted)
        {
            sum += fl;
        }

        float avg = sum / uncondensedCosted.Count;

        
        float temp = uncondensedCosted[0];

        for (int i = 1; i < uncondensedCosted.Count; i++)
        {
            temp = temp * uncondensedCosted[i];
        }

       

        float modfactor = 1f - (1f / uncondensedCosted.Count);
        float adjustvalue = (1 - avg) * modfactor;
        float finalscore = avg + (adjustvalue * avg);

        

        return finalscore;
    }


    public void SetScore(float score)
    {
       uncondensedCosted.Add(score);
        //cost += score;
    }

    public abstract IEnumerator ExecuteAction(Actor source, BoardManager bm);






    protected int MoveToTarget(Actor source, BoardManager bm, int x, int y)
    {

        bool[,] validOption = bm.pathfinding.WeightedBFS(source.GetCurrentStats(StatTypes.MovementRange),
            source.GetPosX(), source.GetPosY(), source.actorData.movement);

        List<TileNode> path =
             bm.pathfinding.GenerateMovementPath(source, x, y,
             source.GetCurrentStats(StatTypes.MovementRange));

        if (path == null)
        {
            return 0; 
        }

        bm.tileSelection.PopulateMovementRange(validOption);
        bm.tileSelection.PopulateMovementPath(path, validOption);

        source.MoveAlongPath(path);

        return path.Count;
    }
}
