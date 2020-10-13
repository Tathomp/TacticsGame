using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarPanel : MonoBehaviour {

    public ToolbarButton toolbarButotnPrefab;
    public RectTransform skillSlots;
    public SkillDescriptPanel skillDescriptPanel;

    //Skill and Inventory Buttons
    public List<ToolbarButton> skillhotbar, inventoryhotbar;

    //Equipment Buttons
    public ToolbarButton headslot, weaponSlot;

    public BoardManager boardManager;


    public void GenerateDefaultHotBar(ActorData data)
    {
        int i = 0;

        data.toolbaar = new ToolBarData();

        foreach (Skill s in data.JobDataState.GetAllLearnedSkills(data.race, data.primaryJob, data.secondaryJob))
        {
            data.toolbaar.skills[i] = s.GetKey();
            i++;

            if(i == data.toolbaar.skills.Length -1)
            {
                //InitToolBar(data);
                break;
            }
        }

        string ss = data.Name + " ";

        foreach (string k in data.toolbaar.skills)
        {
            ss += k + " ";
        }


        InitToolBar(data);
    }

    /// Main Method that controls the display of all the hotbar components
    ///
    public void InitToolBar(ActorData data)
    {
        ////
        /// Currently we're scrapping the idea of tool bar in exchange for a context menu
        return;
        gameObject.SetActive(true);


        //equipment
        PopulateEquipment(data);

        //inventory
        PopulateInventory(data);


        //skills
        PopulateSkills(data);

        DisplayKeys();
    }


    public void Initbuttons(int index, List<ToolbarButton> buttons, IHotbar skill, ActorData data)
    {
        buttons[index].InitButton(data, skill, boardManager, skillDescriptPanel);

    }


    public void Initbuttons(ToolbarButton buttons, IHotbar skill, ActorData data)

    {
        buttons.InitButton(data, skill, boardManager, skillDescriptPanel);
    }


    public void PopulateSkills(ActorData data)
    {
        string[] keys = data.toolbaar.skills;
        List<Skill> skills = data.JobDataState.GetAllLearnedSkills(data.race, data.primaryJob, data.secondaryJob);

        for (int i = 0; i < keys.Length; i++)
        {

            if (keys[i] == "")
            {
                Initbuttons(i, skillhotbar, null, data);

            }
            else
            {
                for (int j = 0; j < skills.Count; j++)
                {
                    if (keys[i] == skills[j].GetKey())
                    {
                        Initbuttons(i, skillhotbar, skills[j], data);
                    }
                }

            }

        }
    }


    void PopulateInventory(ActorData data)
    {
        List<ItemContainer> items = data.inventory.ItemSlots;

        for (int i = 0; i < items.Count; i++)
        {
            if(items[i] != null)
            {
                Initbuttons(i, inventoryhotbar, Globals.campaign.GetItemData(items[i].itemKey), data);
            }
            else
            {
                Initbuttons(i, inventoryhotbar, null, data);
            }
        }
    }


    void PopulateEquipment(ActorData data)
    {
        Equipment e = data.equipment;

        if(e.GetHeadItem() != null)
        {
            Initbuttons(headslot, Globals.campaign.GetItemData(e.GetHeadItem()), data);
        }

        if(e.GetPrimaryWeapon() != null)
        {
            Initbuttons(weaponSlot, Globals.campaign.GetItemData(e.GetPrimaryWeapon()), data);
        }
    }


    void DisplayKeys()
    {
        KeyBindingHelper helper = new KeyBindingHelper();

        Dictionary<KeyBindingNames, KeyCode> k = boardManager.inputFSM.currentState.inputHandler.hotkeys.hotkeys;
        for (int i = 0; i < 10; i++)
        {
            DisplaySkillKeys(i, helper, k);

        }

        for (int i = 0; i < 5; i++)
        {
            DisplayInventoryKeys(i, helper, k);
        }

        DisplayEquipment(helper, k);
    }


    void DisplaySkillKeys(int i, KeyBindingHelper helper, Dictionary<KeyBindingNames, KeyCode> dict)
    {
        string s = "";
        s = helper.GetKEyName(dict[KeyBindingNames.SkillKey1 + i]);

        skillhotbar[i].SetHotKeyText(s);
    }


    void DisplayInventoryKeys(int i, KeyBindingHelper helper, Dictionary<KeyBindingNames, KeyCode> dict)
    {
        string s = helper.GetKEyName(dict[KeyBindingNames.InventoryKey1 + i]);
        inventoryhotbar[i].SetHotKeyText(s);
    }

    void DisplayEquipment(KeyBindingHelper helper, Dictionary<KeyBindingNames, KeyCode> dict)
    {
        string s = helper.GetKEyName(dict[KeyBindingNames.HeadSlot]);
        headslot.SetHotKeyText(s);

        s = helper.GetKEyName(dict[KeyBindingNames.WeaponSlot]);
        weaponSlot.SetHotKeyText(s);
    }

}
