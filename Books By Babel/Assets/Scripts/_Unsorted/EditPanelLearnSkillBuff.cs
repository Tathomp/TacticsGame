using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditPanelLearnSkillBuff : EffectMenuPanel
{
    public DropdownMenu jobSelect, skillSelect;

    LearnSkillBuffEffect skillEffect;

    public override void InitPanel(BuffEffect effect)
    {
        skillEffect = effect as LearnSkillBuffEffect;

        PopulateJobList(skillEffect.jobToChange, jobSelect);
        PopulateSkilJobList(skillEffect.skillToLearn, skillEffect.jobToChange, skillSelect);



    }

    protected override void CleanUp()
    {
        jobSelect.ClearListeners();
        skillSelect.ClearListeners();
    }

    protected override void Save()
    {
        skillEffect.jobToChange = jobSelect.GetValue();
        skillEffect.skillToLearn = skillSelect.GetValue();
    }
}
