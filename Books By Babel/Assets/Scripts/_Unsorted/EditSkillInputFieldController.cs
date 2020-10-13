using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EditSkillInputFieldController : MonoBehaviour
{
    //Input field
    public EditSkillPanel panel;
    public TMP_InputField skillname, description, minRange, maxRange, cooldown;
    public Toggle useWep;

    //TargetType Data

    //Skill effects data
    public Transform skilleffect_contianer;

    private Skill currSkill;

    public void DisplayData(Skill s)
    {
        currSkill = s;

        skillname.text = currSkill.skillName;
        description.text = panel.currSkill.descript;

        minRange.text = currSkill.minRange + "";
        maxRange.text = currSkill.maxRange + "";
        cooldown.text = currSkill.cooldown + "";

        useWep.isOn = currSkill.UseWepon;
    }

    public void SaveInputFieldData()
    {

        currSkill.skillName = skillname.text;
        currSkill.descript = description.text;

        currSkill.minRange = int.Parse(minRange.text);
        currSkill.maxRange = int.Parse(maxRange.text);
        currSkill.cooldown = int.Parse(cooldown.text);

        currSkill.UseWepon = useWep.isOn;
    }

}
