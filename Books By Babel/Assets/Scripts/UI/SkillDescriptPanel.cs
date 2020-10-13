using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDescriptPanel : MonoBehaviour {

    public Text skillDescript;

	public void InitDescript(IHotbar skill)
    {
        string s = "";

        s = skill.GetName();
        s += "\n" + skill.GetHotbarDescription();


        skillDescript.text = s;
    }
}
