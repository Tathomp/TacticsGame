using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditPanelAuraBuffEffect : EffectMenuPanel
{
    public DropdownMenu dropDown;
    public TMP_InputField range;

    AuraBuffEffect effect;

    public override void InitPanel(BuffEffect e)
    {
        effect = e as AuraBuffEffect;



        PopulateBuffSelections(effect.buffToApply, dropDown);
        range.text = effect.range + "";



        dropDown.droptDown.onValueChanged.AddListener(delegate { Save(); });
        range.onValueChanged.AddListener(delegate { Save(); });


    }

    protected override void CleanUp()
    {
        dropDown.droptDown.onValueChanged.RemoveAllListeners();
        range.onValueChanged.RemoveAllListeners();
    }


    protected override void Save()
    {
        effect.range = int.Parse(range.text);
        effect.buffToApply = dropDown.GetValue();

    }
}
