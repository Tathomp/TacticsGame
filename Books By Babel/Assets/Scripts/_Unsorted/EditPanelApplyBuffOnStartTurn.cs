using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditPanelApplyBuffOnStartTurn : EffectMenuPanel
{
    public DropdownMenu dropDown;

    ApplyBuffOnTurnStartEffect effect;

    public override void InitPanel(BuffEffect e)
    {
        this.effect = e as ApplyBuffOnTurnStartEffect;

        PopulateBuffSelections(effect.buffToApply, dropDown);



        dropDown.droptDown.onValueChanged.AddListener( delegate { Save(); } );

    }

    protected override void CleanUp()
    {
        dropDown.droptDown.onValueChanged.RemoveAllListeners();
    }

    protected override void Save()
    {
        effect.buffToApply = dropDown.GetValue();
    }

  
}


