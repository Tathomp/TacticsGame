using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageObject
{
    public StatTypes typeBonus, resistPenalty;
    public float bonusRate, penaltyRate;

    public string tag;

    public int baseValue;

    public DamageObject()
    {
        tag = "";
        bonusRate = 1f;
        penaltyRate = .2f;
    }


    public DamageObject(StatTypes bonus, StatTypes resist, float bonusRate, float penaltyRate,
        int baseValue, string tag = "")
    {
        this.typeBonus = bonus;
        this.resistPenalty = resist;
        this.bonusRate = bonusRate;
        this.penaltyRate = penaltyRate;

        this.baseValue = baseValue;
        this.tag = tag;
    }


    public DamageObject Copy()
    {
        return new DamageObject(typeBonus, resistPenalty, bonusRate, penaltyRate, baseValue, tag);
    }
}
