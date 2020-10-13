using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChangeTileTypeTileEffectCompontent : TileEffectComponent
{
    protected string tt;

    public ChangeTileTypeTileEffectCompontent(string tile)
    {
        this.tt = tile;
    }

    public TileEffectComponent Copy()
    {
        return new ChangeTileTypeTileEffectCompontent(tt);
    }

    public void ExecuteEffect(TileNode tilenode)
    {
        int x = tilenode.data.posX;
        int y = tilenode.data.posY;


        string newtype = tt;

        /*List<TileTypes> t = Globals.GetBoardManager().campaign.contentLibrary.currentTileDatabase.Tiles;
        TileTypes newType;
        
        foreach (TileTypes tile in t)
        {
            if (tile.TileName == tt)
            {
                newtype = tile;
            }
        }
        */
        //Globals.GetBoardManager().pathfinding.tiles[x, y].type = newtype;


        //Ajdust ActorStats
        TileTypes newType = Globals.campaign.GetTileData().Tiles.GetCopy(newtype);

        if(tilenode.actorOnTile != null)
        {
            tilenode.actorOnTile.ReduceStats(tilenode.type.tileBonuses);
            tilenode.actorOnTile.AddStats(newType.tileBonuses);
        }

        Globals.GetBoardManager().currMap.tileBoard[x, y] = newtype;
        Globals.GetBoardManager().pathfinding.tiles[x, y].type = newType;

        tilenode.tileGO.GetComponent<SpriteRenderer>().sprite = Globals.GetSprite(FilePath.TileSetAtlas, 
            Globals.campaign.GetTileData().Tiles.GetData(newtype).spriteFilePath);

    }
    public float GetSCore(Actor ai, TileNode node)
    {
        return 0;
    }
    /*
public void ExecuteEffect(TileNode tilenode)
{
   int x = tilenode.data.posX;
   int y = tilenode.data.posY;

   List<TileTypes> t = Globals.GetBoardManager().campaign.contentLibrary.currentTileDatabase.Tiles;
   TileTypes newtype = null;

   foreach (TileTypes tile in t)
   {
       if(tile.TileName == tt)
       {
           newtype = tile;
       }
   }

   tilenode.type = newtype.Clone();

   tilenode.tileGO.GetComponent<SpriteRenderer>().sprite = Globals.GetSprite(FilePath.TileSetAtlas, newtype.spriteFilePath);
}
*/


}
