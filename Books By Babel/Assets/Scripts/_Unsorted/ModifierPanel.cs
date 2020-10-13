using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifierPanel : MonoBehaviour
{

    //Editor Hooks
    public Toggle toggleButton;
    public BonusRateSlider jp, xp, currency, relationsip;

    //
    private DifficultyModifier curr_mod;

    public void InitMOdiferPanel(DifficultyModifier modifier)
    {
        curr_mod = modifier;

        PrintMods();

        //gameObject.SetActive(true);
        
    }


    public void ApplyChanges()
    {
        curr_mod.permaDeath = toggleButton.isOn;

        curr_mod.jp_bonus = jp.GetValue();
        curr_mod.xp_bonus = xp.GetValue();
        curr_mod.currency_bonus = currency.GetValue();
        curr_mod.relationship_bonus = relationsip.GetValue();


    }

    public void TogglePanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void PrintMods()
    {
        toggleButton.isOn = curr_mod.permaDeath;

        jp.InitValue(curr_mod.jp_bonus);
        xp.InitValue(curr_mod.xp_bonus);
        currency.InitValue(curr_mod.currency_bonus);
        relationsip.InitValue(curr_mod.relationship_bonus);
    }
}
