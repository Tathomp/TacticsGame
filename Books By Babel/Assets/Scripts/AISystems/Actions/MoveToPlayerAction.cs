using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerAction : AIAction
{
    public Actor source;
    public MapCoords target;

    public MoveToPlayerAction(Actor source, MapCoords target)
    {
        this.source = source;
        this.target = target;
    }

    public override IEnumerator ExecuteAction(Actor source, BoardManager bm)
    {


        int pathLeng = MoveToTarget(source, bm, target.X, target.Y);
        



        yield return new WaitForSeconds(waitTime * pathLeng);

        bm.tileSelection.ClearAllRange();

        source.Wait();
        bm.turnManager.CalculateFastest();

    }
}
