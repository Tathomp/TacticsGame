using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour {

    public BoardManager boardManager;
    public SkillButton abilityButton;
    public SkillTooltipPanel tooltippanel;
    public ScrollListScaleableContent contentTransform;

   // List<SkillButton> skillButtons = new List<SkillButton>();
    int currIndex, maxIndex;

    public void GenerateAbilityList(Actor actor, List<IUseable> skills)
    {
        this.gameObject.SetActive(true);

        foreach (IUseable skill in skills)
        {
      
                SkillButton temp = GameObject.Instantiate<SkillButton>(abilityButton, contentTransform.contentTransform);
                temp.InitButton(skill, tooltippanel);
                temp.ChangeText( skill.GetName() );
                temp.button.onClick.AddListener(delegate { SkillClicked(skill, actor); });
                contentTransform.AddToList(temp);

                temp.button.interactable = (skill.CanPayCost(actor) 
                    && actor.actorData.cooldownMap.IsSKillOnCooldown(skill.GetKey()) == false);
            
        }

        maxIndex = skills.Count - 1;

        if(maxIndex > 0)
        {
            AdjustMenu(0);

        }
    }

    void SkillClicked(IUseable currentSkill, Actor currentActor)
    {
        boardManager.inputFSM.SwitchState(new AbilityInUseState(boardManager, currentActor, boardManager.pathfinding.GetTileNode(currentActor.GetPosX(), currentActor.GetPosY()),currentSkill));
        ClearSkillListeners();

    }

    public void ClearSkillListeners()
    {
        contentTransform.CleanUp();

    }

    public void AdjustMenu(int x)
    {
        currIndex += x;

        if (currIndex < 0)
        {
            currIndex = maxIndex;
        }
        else if (currIndex > maxIndex)
        {
            currIndex = 0;
        }


        // skillButtons[currIndex].Select();
    }

    public void Selection()
    {
        contentTransform.buttonConatiner.SelectButton(currIndex);
    }

}
