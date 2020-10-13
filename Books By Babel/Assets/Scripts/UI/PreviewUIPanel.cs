using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreviewUIPanel : MonoBehaviour
{

    public TMP_Text indexLabel, sourceLabel, damageLabel, targetLabel;

    public void InitText(CombatNode c)
    {
        sourceLabel.text = "Source" + "\n" + c.source.actorData.Name;
        damageLabel.text = "Damage: \n";
        c.UpDatePreview(this);

        if (c.target != null)
        {
            targetLabel.text = c.target.actorData.Name + "\n";
            targetLabel.text += c.target.GetCurrentStats(StatTypes.Health) + " / " + c.target.GetMaxStats(StatTypes.Health);
        }
    }

    public void UpdateIndex(int pos, int max)
    {
        indexLabel.text = pos + " / " + max;
    }
}

    
