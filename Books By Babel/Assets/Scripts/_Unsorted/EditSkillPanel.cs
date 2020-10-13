using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditSkillPanel : MonoBehaviour
{
    public CreationSuiteManager manager;
    public ScrollListScaleableContent list;
    public TextButton prefab;

    public EditSkillInputFieldController inputFieldController;
    public EditTargetTypePanel targetPanel;

    [HideInInspector]
    public Skill currSkill;

    public void PopulateSkillList()
    {
        manager.SetCurrentActiveObject(this.gameObject);

        foreach (string item in manager.currentCampaign.contentLibrary.skillDatabase.DbKeys())
        {
            TextButton t = Instantiate<TextButton>(prefab, list.contentTransform);
            list.AddToList(t);

            t.ChangeText(item);

            t.button.onClick.AddListener(delegate { DisplaySkillInfo(item); });
        }
       


    }


    public void DisplaySkillInfo(string s)
    {
        

        Skill skill = manager.currentCampaign.contentLibrary.skillDatabase.GetCopy(s);

        inputFieldController.DisplayData(skill);
        targetPanel.DisplayTargetTypeData(skill.targetType);
    }


    public void SelectSkill(Skill s)
    {
        currSkill = s;


    }


}
