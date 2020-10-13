using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPanelCastOnDeath : EffectMenuPanel
{
    public Toggle useOnKiller;
    public DropdownMenu dropdown;

    SkillOnDeathBuffEffect ondeath;

    public override void InitPanel(BuffEffect effect)
    {
        ondeath = effect as SkillOnDeathBuffEffect;

        PopulateSkillList(ondeath.skillID, dropdown);


        useOnKiller.onValueChanged.AddListener
            (
                delegate { Save(); }
            );
    }

    protected override void CleanUp()
    {
        dropdown.ClearListeners();

        useOnKiller.onValueChanged.RemoveAllListeners();
    }

    protected override void Save()
    {
        ondeath.skillID = dropdown.GetValue();
        ondeath.useOnKiller = useOnKiller.isOn;
    }

}
