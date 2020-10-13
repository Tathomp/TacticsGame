using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEditPanel : MonoBehaviour
{
    public EditEffectConditonals conditionalPanel;

    public EditPanelApplyBuffOnStartTurn buffOnTurnStartPanel;
    public EditPanelAuraBuffEffect aurapanel;
    public AddTagToSkill tagToSkillPanel;
    public EditPanelLearnSkillBuff learnSkillPanel;
    public EditPanelStatChangeBuffEffect statChangePanel;
    public EditPanelCastOnDeath ondeathPanel;

    private BuffEffect currentBuffeffect;
    private EffectMenuPanel currentOpenPanel;


    public void InitEffect(BuffEffect effect)
    {

        currentBuffeffect = effect;

        InitEffectPanel(effect);


    }

    public void SwitchPanels(EffectMenuPanel newPanel)
    {
        newPanel.InitPanel(currentBuffeffect);

        if(currentOpenPanel != null)
        {
            currentOpenPanel.gameObject.SetActive(false);
        }

        newPanel.gameObject.SetActive(true);
        currentOpenPanel = newPanel;
    }

    public void InitEffectPanel(BuffEffect buffEffect)
    {
        if(buffEffect is AddSkillCostBuffEffect)
        {

        }

        else if (buffEffect is ChangeTargetTypeBuffEffect)
        {

        }
        else if (buffEffect is ModifyStatCostBuffEffect)
        {

        }
        else if (buffEffect is AddSkillEffectBuffEffect)
        {

        }
        else if (buffEffect is ApplyLinkedBuffEffect)
        {

        }
        else if (buffEffect is ApplyBuffOnTurnStartEffect)
        {
            SwitchPanels(buffOnTurnStartPanel);

        }

        else if (buffEffect is AuraBuffEffect)
        {
            SwitchPanels(aurapanel);
        }
        else if (buffEffect is BanishEffect)
        {

        }
        else if (buffEffect is BlockAttackEffect)
        {

        }
        else if (buffEffect is BlockMoveBuffEffect)
        {

        }
        else if (buffEffect is BonusDamageOnAttack)
        {

        }
        else if (buffEffect is BonusTagToSkillBuffEffect)
        {
            SwitchPanels(tagToSkillPanel);
        }
        else if (buffEffect is ChangeMovementType)
        {

        }
        else if (buffEffect is ChangeSpriteBuffEffect)
        {

        }
        else if (buffEffect is FlatStatBuffEffectPerTurn)
        {

        }
        else if (buffEffect is LearnSkillBuffEffect)
        {
            SwitchPanels(learnSkillPanel);
        }
        else if (buffEffect is LinkBuffEffect)
        {

        }
        else if (buffEffect is RedirectCombatBuffEffect)
        {

        }
        else if (buffEffect is ScalingStatBuffEffect)
        {
            SwitchPanels(statChangePanel);
        }
        else if (buffEffect is SkillOnDeathBuffEffect)
        {
            SwitchPanels(ondeathPanel);
        }
        else if (buffEffect is StanceBuffEffect)
        {

        }
        else if (buffEffect is StatBuff)
        {

        }
        else if (buffEffect is SwitchJobsEffect)
        {

        }
        else if (buffEffect is TransformBuffEffect)
        {

        }




    }

    public void InitConditionalPanel()
    {
        conditionalPanel.InitConditionalEdit(currentBuffeffect.conditionalsRequired);
        ToggleOff();
       
    }


    public void InitEditPanel()
    {
        conditionalPanel.ToggleOff();
        gameObject.SetActive(true);
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);

    }
}
