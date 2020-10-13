using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolbarButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text hotkey;
    public Image icon;

    ActorData data;
    SkillDescriptPanel skillDescript;
    BoardManager ifsm;
    public IHotbar content;
    public IUseable useable;

    public void InitButton(ActorData data, IHotbar content, BoardManager ifsm, SkillDescriptPanel panel)
    {
        this.data = data;
        this.content = content;
        this.ifsm = ifsm;
        this.skillDescript = panel;

        if (content != null)
        {
            icon.sprite = Globals.GetSprite(FilePath.IconSpriteAtlas, content.GetIconFilePath());

            if (content is IUseable)
            {
                useable = (IUseable)content;
            }
            else if (content is Item )
            {
                Item t = (Item)content;
                if(t.HasConsumableEFfect())
                {
                    useable = t.GetConsumableEffect();
                }
                else if(t.HasActivationEffect())
                {
                    useable = t.GetActivateableEffect();
                }
                else
                {
                    useable = null;
                }
            }
            else
            {
                useable = null;
            }
            
        }
        else
        {
            icon.sprite = Globals.GetSprite(FilePath.IconSpriteAtlas, "unkown");

            useable = null;
        }
    }


    public void SkillClicked()
    {
        if (useable != null /*&& data.CanAttack*/)
        {            
            ifsm.inputFSM.SwitchState(new AbilityInUseState(ifsm,
                ifsm.turnManager.currFastest, 
                ifsm.pathfinding.GetTileNode(data.gridPosX, data.gridPosY),
                (useable)));
        }
    }


    public void SetHotKeyText(string s)
    {
        hotkey.text = s;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (content != null)
        {
            skillDescript.InitDescript(content);
            skillDescript.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (content != null)
        {
            skillDescript.gameObject.SetActive(false);
        }
    }
}
