using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoPanel : MonoBehaviour {

    public TMP_Text actorName, statsText;
    public Transform itemRegion, equipmentRegion, skillRegion, statsRegion;
    public ScrollListScaleableContent buffRegion;
    public CharacterInfoPreviewPanel characterInfoPreviewPanel;

    public BuffDisplayWrapper buffPrefab;
    public Image portrait;

    ActorData data;

    List<BuffDisplayWrapper> buffDisplay;

    public void QUitScreen()
    {
        gameObject.SetActive(false);
        Globals.GetBoardManager().inputFSM.SwitchState(new UsersTurnState(Globals.GetBoardManager()));
    }

	public void InitPanel(ActorData data)
    {
        gameObject.SetActive(true);
        this.data = data;
        actorName.text = data.Name;
        portrait.sprite = Globals.GetPortrait(data.portraitFilePath);

        PrintBuffs();
        PrintEquipment();
        PrintSkills();
        PrintStats();

    }


    void PrintBuffs()
    {
        List<Buff> buffs = data.buffContainer.buffList;
        List<ItemContainer> items = data.inventory.ItemSlots;

        buffDisplay = new List<BuffDisplayWrapper>();

        foreach (Buff b in buffs)
        {
            CreateLabel(b, buffRegion.contentTransform);
        }

        foreach (ItemContainer i in items)
        {
            {
                Item temp = Globals.campaign.GetItemCopy(i.itemKey);
                CreateLabel(temp, itemRegion);
            }
        }

    }


    void PrintEquipment()
    {
        

        if (data.equipment.HasHeadItem())
        {
            CreateLabel(Globals.campaign.GetItemCopy(data.equipment.GetHeadItem()), equipmentRegion);
        }

        if (data.equipment.HasWep())
        {
            CreateLabel(Globals.campaign.GetItemCopy(data.equipment.GetPrimaryWeapon()), equipmentRegion);
        }
    }


    void PrintSkills()
    {
        List<Skill> skills = data.JobDataState.GetAllLearnedSkills(data.race, data.primaryJob, data.secondaryJob);

        foreach (Skill s in skills)
        {
            CreateLabel(s, skillRegion);
        }
    }


    void PrintStats()
    {


        foreach (StatTypes st in data.currentStatCollection.GetKeys())
        {
          
            statsText.text += st.ToString() + ":  " + data.currentStatCollection.GetValue(st) +"\n";
        }
    }

    void CreateLabel(IDisplayInfo item, Transform region)
    {
        BuffDisplayWrapper i = Instantiate<BuffDisplayWrapper>(buffPrefab, region);
        i.InitPreview(characterInfoPreviewPanel, item);
        buffDisplay.Add(i);
    }


    #region Clean Up
    void ClearLabels()
    {
        
        if(buffDisplay == null)
        {
            return;
        }
        

        for (int i = buffDisplay.Count - 1; i >= 0; i--)
        {
            Destroy(buffDisplay[i].gameObject);
            Destroy(buffDisplay[i]);
        }
    }   


    public void ToggleOff()
    {
        ClearLabels();

        gameObject.SetActive(false);
        characterInfoPreviewPanel.ToggleOff();
    }
    #endregion
}
