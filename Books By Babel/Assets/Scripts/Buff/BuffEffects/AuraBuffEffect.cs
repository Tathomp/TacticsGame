using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AuraBuffEffect : BuffEffect
{
    public string buffToApply;
    public int range;


    public Dictionary<MapCoords, string> effectMap;

    public AuraBuffEffect(string buffToApply, int range)
    {
        this.buffToApply = buffToApply;
        this.range = range;

        effectMap = new Dictionary<MapCoords, string>();
    }

    public override BuffEffect Copy()
    {
        AuraBuffEffect b = new AuraBuffEffect(buffToApply, range);

        foreach (MapCoords coords in effectMap.Keys.ToList())
        {
            b.effectMap.Add(coords, effectMap[coords]);
        }

        return b;
    }

    public override void OnActorSpawn(ActorData actor)
    {
        OnApply(actor, actor);
    }


    public override void OnApply(ActorData actor, ActorData source)
    {
        OnRemove(actor);

        if (Globals.currState == GameState.Combat)
        {
            GenerateTileNodeDictionary(actor, actor.gridPosX, actor.gridPosY);
        }
    }

    public override void OnRemove(ActorData actor)
    {

        MapCoords[] keys = effectMap.Keys.ToArray();

        foreach (MapCoords coords in keys)
        {
            TileNode node = Globals.GetBoardManager().pathfinding.GetTileNode(coords);

            node.RemoveTileEffect(effectMap[new MapCoords(node.data.posX, node.data.posY)]);
        }

        effectMap = new Dictionary<MapCoords, string>();

    }

    public override void OnMove(ActorData actor, TileNode startTile, TileNode destTile)
    {
        OnRemove(actor);

        GenerateTileNodeDictionary(actor, destTile.data.posX, destTile.data.posY);

    }



    private void GenerateTileNodeDictionary(ActorData sourcID, int startX, int startY)
    {
        Pathfinding pf = Globals.GetBoardManager().pathfinding;

        //Change faction and tile type effects here
        List<TileNode> auraNodes = pf.GetNodes(pf.UnWeightedBFS(range, 0, startX, startY));

        effectMap = new Dictionary<MapCoords, string>();

        foreach (TileNode node in auraNodes)
        {
           // if (!(node.data.posX == startX && node.data.posY == startY))
            {
                AuraTileEffect effect = new AuraTileEffect("Aura Tile Effect",
                    sourcID,
                    Globals.GenerateRandomHex(),
                new NoLengthLimit(), new NoSpread(), buffToApply);
                node.AddTileEffect(effect);
                effectMap.Add(new MapCoords(node.data.posX, node.data.posY), effect.tempID);
            }
        }
    }

    public override string PrintNameOfEffect()
    {
        return "Aura Buff";

    }
}
