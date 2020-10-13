using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StealCreditsCombatNode : CombatNode
{


    public StealCreditsCombatNode(Actor source, TileNode targetedTile) 
        : base(source, targetedTile)
    {

    }

    public override void ApplyEffect()
    {
        Debug.Log("Before: " + Globals.campaign.currentparty.Credits);


        if (target != null)
        {
            int x = Random.Range(15, 60);

            if(target.actorData.controller.PlayerControlled())
            {
                //subtrace from party
                Globals.campaign.currentparty.Credits -= x;
            }
            else
            {
                //add to party
                Globals.campaign.currentparty.Credits += x;

            }
        }

        Debug.Log("After: " + Globals.campaign.currentparty.Credits);
    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        panel.damageLabel.text = source.actorData.Name + " stealing money from " + target.actorData.Name;
    }
}
