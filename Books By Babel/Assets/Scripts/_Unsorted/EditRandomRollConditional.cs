using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditRandomRollConditional : MonoBehaviour
{
    public BonusRateSlider slider;
    public EditEffectConditonals conditionalPanel;

    RandomRollConditional cond;

    
    public void Init(Conditional c)
    {
        conditionalPanel.SwitchContexts(this.gameObject);

        cond = c as RandomRollConditional;
        conditionalPanel.currConditional = c;



        PopulateDisplay();

    }


    void PopulateDisplay()
    {
        slider.InitValue(cond.threshold);
    }

    public void UpdateValue()
    {
        cond.threshold = (int)(slider.GetValue());
    }
}
