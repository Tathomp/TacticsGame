using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formulas
{
    public static int CalculateDamageChange(Actor source, Actor target, List<DamageObject> dmgObjs)
    {

        int dm = 0;

        int dmgValue = 0, ressitValue = 0;

        for (int i = 0; i < dmgObjs.Count; i++)
        {
            dmgValue += BaseDamageFormulat(dmgObjs[i].baseValue, source.GetCurrentStats(dmgObjs[i].typeBonus), dmgObjs[i].bonusRate);
            ressitValue += BaseDamageFormulat(0, target.GetCurrentStats(dmgObjs[i].resistPenalty), dmgObjs[i].penaltyRate);


        }


        dm = dmgValue - ressitValue;

        if( dm < 0 )
        {
            dm = 0;
        }

        return dm;
        
    }


    public static int TileEffectDamage(int amt)
    {
        return amt;
    }


    public static int BaseDamageFormulat(int baseValue, int bonusStat, float rate)
    {
        return Mathf.RoundToInt(baseValue + (bonusStat * rate));


    }

    public static int SellValue(string key)
    {
        return (Globals.campaign.GetItemData(key).cost / 2);

    }
}



