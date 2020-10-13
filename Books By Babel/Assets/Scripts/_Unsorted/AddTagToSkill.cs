using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddTagToSkill : EffectMenuPanel
{
    public DropdownMenu tagDrop;

    BonusTagToSkillBuffEffect effe;

    public override void InitPanel(BuffEffect effect)
    {
        effe = effect as BonusTagToSkillBuffEffect;

        PopulateProperties(effe.tagToAdd.tagToAdd, tagDrop);
    }

    protected override void CleanUp()
    {
        tagDrop.ClearListeners();
    }

    protected override void Save()
    {
        effe.tagToAdd.tagToAdd = tagDrop.GetValue();
    }
}
