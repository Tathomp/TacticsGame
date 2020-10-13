using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatBuff : BuffEffect
{

    public StatsContainer sc = new StatsContainer(); // we add these to the approiate container    
    public StatContainerType containerType;
    
    private StatsContainer sc_to_remove;
    private bool reverseChange_on_removal;

    public StatBuff(StatContainerType containerType, bool reverseChange_on_removal)
    {
        this.containerType = containerType;
        sc_to_remove = new StatsContainer();

        this.reverseChange_on_removal = reverseChange_on_removal;
    }

    public override BuffEffect Copy()
    {
        StatBuff sb = new StatBuff(containerType, reverseChange_on_removal);
        sb.sc = (StatsContainer) sc.Copy();

        return sb;
    }

    public override string GetHotbarDescription()
    {
        return "Changes in stats: " + sc.PrintStats();
    }

    public override void OnApply(ActorData actor, ActorData source)
    {
        if(containerType == StatContainerType.Current)
        {
            actor.currentStatCollection.AddStats(sc);

        }
        else if(containerType == StatContainerType.Max)
        {
            actor.maxStatCollection.AddStats(sc);

        }
        else if(containerType == StatContainerType.Both)
        {
            actor.maxStatCollection.AddStats(sc);
            actor.currentStatCollection.AddStats(sc);
        }

        sc_to_remove.AddStats(sc);
    }

    public override void OnRemove(ActorData actor)
    {
        if (reverseChange_on_removal)
        {
            if (containerType == StatContainerType.Current)
            {
                actor.currentStatCollection.RemoveStats(sc_to_remove);

            }
            else if (containerType == StatContainerType.Max)
            {
                actor.maxStatCollection.RemoveStats(sc_to_remove);

            }
            else if (containerType == StatContainerType.Both)
            {
                actor.maxStatCollection.RemoveStats(sc_to_remove);
                actor.currentStatCollection.RemoveStats(sc_to_remove);
            }
        }
    }

    public override string PrintNameOfEffect()
    {
        return "Stat Change (scaling maybe)";

    }
}
