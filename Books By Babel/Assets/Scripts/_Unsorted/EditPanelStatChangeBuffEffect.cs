using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EditPanelStatChangeBuffEffect : EffectMenuPanel
{
    public DropdownMenu containterTypeBasis, containerToCHange, stattypeCHange, statTypeBasis;

    ScalingStatBuffEffect eff;

    public override void InitPanel(BuffEffect effect)
    {
        eff = effect as ScalingStatBuffEffect;

        PopulateStatsContainerType(eff.containerToChange.ToString(), containerToCHange);
        PopulateStatsContainerType(eff.containerTypeBasis.ToString(), containterTypeBasis);

        POpulateStatTypes(eff.statTypeToChange.ToString(), stattypeCHange);
        POpulateStatTypes(eff.statTypeBasis.ToString(), statTypeBasis);


    }

    protected override void CleanUp()
    {
        stattypeCHange.ClearListeners();
    }

    protected override void Save()
    {
        eff.containerTypeBasis = (StatContainerType)Enum.Parse(typeof(StatContainerType), containterTypeBasis.GetValue());
        eff.containerToChange = (StatContainerType)Enum.Parse(typeof(StatContainerType), containerToCHange.GetValue());

        eff.statTypeToChange = (StatTypes)Enum.Parse(typeof(StatTypes), stattypeCHange.GetValue());
        eff.statTypeBasis = (StatTypes)Enum.Parse(typeof(StatTypes), statTypeBasis.GetValue());
    }
}
