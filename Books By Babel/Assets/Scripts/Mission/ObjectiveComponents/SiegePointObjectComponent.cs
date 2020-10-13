using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SiegePointObjectComponent : ObjectiveComponent
{
    private MapCoords position;

    public SiegePointObjectComponent(int x, int y)
    {
        position = new MapCoords(x, y);
    }

    public SiegePointObjectComponent(MapCoords coords)
    {
        this.position = coords;
    }

    public override ObjectiveComponent Copy()
    {
        return new SiegePointObjectComponent(position);
    }

    public override bool ObjectiveComplete(BoardManager bm)
    {
        List<Actor> actors = Globals.GetBoardManager().spawner.actors;

        foreach (Actor actor in actors)
        {
            if (actor.ActorsController() is PlayerController)
            {
                if (position.IsEqual(actor.actorData.GetPosition()))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public override string PrintProgress()
    {
        return "Move to: " + position.X + ", " + position.Y;
    }
}
