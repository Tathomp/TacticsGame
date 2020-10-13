using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBuffCombatNode : CombatNode
{
    public enum RemoveType { RemoveBuff, RemoveDebuff, RemoveBoth }

    bool removetrait;
    string key, tag;
    bool removeall;

    RemoveType removeBuff;


    public RemoveBuffCombatNode(Actor source, TileNode targetedTile, bool removetrait, bool removeall, string key, string tag, RemoveType type) 
        : base(source, targetedTile)
    {
        this.removeall = removeall;
        this.removetrait = removetrait;
        this.removeBuff = type;

        this.key = key;
        this.tag = tag;
    }

    public override void ApplyEffect()
    {
        List<Buff> potentialBuffs = new List<Buff>();

        if (target != null)
        {
            if (key != "")
            {
                //look for the key
                foreach (Buff buff in target.actorData.buffContainer.buffList)
                {
                    if(buff.GetKey() == key)
                    {
                        if (ShouldAddBuff(buff))
                        {
                            potentialBuffs.Add(buff);

                        }

                    }
                }
            }
            else
            {
                //look for the tag
                foreach (Buff buff in target.actorData.buffContainer.buffList)
                {
                    if (buff.tags.Contains(key))
                    {
                        if(ShouldAddBuff(buff))
                        {
                            potentialBuffs.Add(buff);

                        }


                    }
                }
            }

            if (removeall)
            {
                foreach (Buff buff in potentialBuffs)
                {
                    target.actorData.buffContainer.RemoveBuff(target.actorData, buff);
                }
            }
            else //remove random buff
            {
                int index = Random.Range(0, potentialBuffs.Count - 1);
                target.actorData.buffContainer.RemoveBuff(target.actorData, potentialBuffs[index]);
            }

        }

    }

    private bool ShouldAddBuff(Buff buff)
    {

        if(removetrait == false)
        {
            return false;
        }

        switch(removeBuff)
        {
            case RemoveType.RemoveBuff:
                {
                    if(buff.IsBuff == false)
                    {
                        return false;
                    }
                    break;
                }
            case RemoveType.RemoveDebuff:
                {
                    if (buff.IsBuff == true)
                    {
                        return false;
                    }
                    break;
                }
        }

        return true;
    }

    public override void UpDatePreview(PreviewUIPanel panel)
    {
        panel.damageLabel.text = "Remove buff";
    }
}
