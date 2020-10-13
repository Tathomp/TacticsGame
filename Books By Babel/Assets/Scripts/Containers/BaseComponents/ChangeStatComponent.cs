using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChangeStatComponent 
{

    private int amt;
    private StatTypes typeToChange;
    private StatContainerType containerType;

    public ChangeStatComponent(int amt, StatTypes typeToChange, StatContainerType containerType)
    {
        this.amt = amt;
        this.typeToChange = typeToChange;
        this.containerType = containerType;
    }



    public void ApplyChange(Actor actor)
    {
        if(containerType == StatContainerType.Both || containerType == StatContainerType.Current)
        {
            actor.UpdateCurrentStat(typeToChange, amt);
        }

        if(containerType == StatContainerType.Both || containerType == StatContainerType.Max)
        {
            actor.UpdateMaxStats(typeToChange, amt);

        }
    }

    public void RemoveChange(Actor actor)
    {
        if (containerType == StatContainerType.Both || containerType == StatContainerType.Current)
        {
            actor.UpdateCurrentStat(typeToChange, -amt);
        }

        if (containerType == StatContainerType.Both || containerType == StatContainerType.Max)
        {
            actor.UpdateMaxStats(typeToChange, -amt);

        }
    }


    public ChangeStatComponent Copy()
    {
        return new ChangeStatComponent(amt, typeToChange, containerType);
    }
}
