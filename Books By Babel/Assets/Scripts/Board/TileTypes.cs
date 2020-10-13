using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class TileTypes : DatabaseEntry
{
    public string TileName;
    public string spriteFilePath;

    public Dictionary<string, int> MovementTypeCostMap; //(MovementType, TraverseCost)
    public StatsContainer tileBonuses;

    public List<string> attributes;
    //public List<TileEffect> tileEffects;

    public TileTypes(string key) : base(key)
    {
        attributes = new List<string>();
        tileBonuses = new StatsContainer();
        //allowedMovement = new List<MovementType>();
        //tileEffects = new List<TileEffect>();

        MovementTypeCostMap = new Dictionary<string, int>();
    }

    public bool UnitCanTravelHere(string movementTypes)
    {
        if(MovementTypeCostMap.ContainsKey(movementTypes))
        {
            return true;
        }
 
        return false;
    }



    public List<string> GetMovementTypes()
    {

       return MovementTypeCostMap.Keys.ToArray().ToList();
    }

    public override DatabaseEntry Copy()
    {
        TileTypes type = new TileTypes(key)
        {
            TileName = TileName,
            spriteFilePath = spriteFilePath,
            
        };

        foreach (string s in MovementTypeCostMap.Keys.ToArray())
        {
            type.MovementTypeCostMap.Add(s, MovementTypeCostMap[s]);
        }

        foreach (string atrib in attributes)
        {
            type.attributes.Add(atrib);
        }


        type.tileBonuses = (StatsContainer) tileBonuses.Copy();


        return type;
    }

}


