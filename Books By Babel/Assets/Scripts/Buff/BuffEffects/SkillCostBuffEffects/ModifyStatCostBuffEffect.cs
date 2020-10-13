using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModifyStatCostBuffEffect : BuffEffect
{
    public StatTypes typeToChange;
    public ModifyType modifier;


    public override void ModSkillCost(Actor source, Skill skillToMod)
    {
        foreach (SkillCost cost in skillToMod.skillCost)
        {
            modifier.Modify(cost, typeToChange);
            modifier.ChaangeType(cost);
        }
    }


    public override BuffEffect Copy()
    {
        ModifyStatCostBuffEffect e = new ModifyStatCostBuffEffect();
        e.typeToChange = typeToChange;
        e.modifier = modifier.Copy();


        return e;
    }


    public override string PrintNameOfEffect()
    {
        return "Modify Stat Cost";

    }
}

[System.Serializable]
public abstract class ModifyType
{
    protected bool changeType;
    protected StatTypes typeToChagneToo;

    public ModifyType()
    {
        changeType = false;

    }

    public ModifyType(StatTypes typeToChagneToo)
    {
        this.changeType = true;
        this.typeToChagneToo = typeToChagneToo;
    }

    public abstract void Modify(SkillCost cost, StatTypes typeToChange);

    public void ChaangeType(SkillCost cost)
    {
        if(changeType)
        {
            ((SkillCostStat)cost).type = typeToChagneToo;
        }
    }


    public abstract ModifyType Copy();
}


[System.Serializable]
public class ModifyScale : ModifyType
{
    private float multipler;

    public ModifyScale(float multipler)
    {
        this.multipler = multipler;
    }


    public override ModifyType Copy()
    {
        ModifyScale s = new ModifyScale(multipler);
        s.changeType = changeType;
        s.typeToChagneToo = typeToChagneToo;

        return s;
    }

    public override void Modify(SkillCost cost, StatTypes typeToChange)
    {
        if(cost is SkillCostStat)
        {
            SkillCostStat stat = cost as SkillCostStat;

            if(stat.type == typeToChange)
            {
                stat.cost = Mathf.RoundToInt(stat.cost * multipler);
            }
        }
    }

}

[System.Serializable]
public class ModifyIncrease: ModifyType
{
    private int baseIncrease;

    public ModifyIncrease(int baseIncrease)
    {
        this.baseIncrease = baseIncrease;
    }

    public ModifyIncrease(int baseIncrease, StatTypes typeToChange) : base(typeToChange)
    {
        this.baseIncrease = baseIncrease;
    }

    public override ModifyType Copy()
    {
        ModifyIncrease s = new ModifyIncrease(baseIncrease);
        s.changeType = changeType;
        s.typeToChagneToo = typeToChagneToo;

        return s;
    }

    public override void Modify(SkillCost cost, StatTypes typeToChange)
    {
        if (cost is SkillCostStat)
        {
            SkillCostStat stat = cost as SkillCostStat;

            if (stat.type == typeToChange)
            {
                stat.cost += baseIncrease;
            }
        }
    }
}


[System.Serializable]
public class ModifySet : ModifyType
{
    private int setTo;

    public ModifySet(int setTo)
    {
        this.setTo = setTo;
    }

    public override ModifyType Copy()
    {
        ModifySet s = new ModifySet(setTo);
        s.changeType = changeType;
        s.typeToChagneToo = typeToChagneToo;


        return s;
    }

    public override void Modify(SkillCost cost, StatTypes typeToChange)
    {
        if (cost is SkillCostStat)
        {
            SkillCostStat stat = cost as SkillCostStat;

            if (stat.type == typeToChange)
            {
                stat.cost = setTo;
            }
        }
    }
}

