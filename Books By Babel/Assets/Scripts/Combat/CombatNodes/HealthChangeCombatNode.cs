using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChangeCombatNode : CombatNode
{
    public int ChangeHealth;


    public HealthChangeCombatNode(Actor source, TileNode targetedTile, Actor target, int deltaHealth)
        : base(source, targetedTile)
    {
        ChangeHealth = deltaHealth;

    }


    public override void ApplyEffect()
    {
        target.ChangeHealth(ChangeHealth, source.actorData);
    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        if(ChangeHealth > 0)
        {
            panel.damageLabel.text = "Heal for: " + ChangeHealth.ToString();

        }
        else
        {
            panel.damageLabel.text = "Damage for: " + Mathf.Abs(ChangeHealth).ToString();

        }
    }
}
