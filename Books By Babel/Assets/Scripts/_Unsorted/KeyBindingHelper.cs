using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindingHelper
{
    Dictionary<KeyCode, string> keyNames;
    Dictionary<KeyBindingNames, string> bindingNames;

    public KeyBindingHelper()
    {
        DefaultKeyNames();
        DefaultBindingNames();
    }

    public bool CheckIfKeyHasName(KeyCode k)
    {
        return keyNames.ContainsKey(k);
    }

    
    public string GetKEyName(KeyCode k)
    {
        if(keyNames.ContainsKey(k))
        {
            return keyNames[k];
        }
        else
        {
            return k.ToString();
        }
    }


    public string GetBindingNames(KeyBindingNames k)
    {
        return bindingNames[k];
    }


    void DefaultKeyNames()
    {
        keyNames = new Dictionary<KeyCode, string>();
        keyNames.Add(KeyCode.Alpha0, "0");
        keyNames.Add(KeyCode.Alpha1, "1");
        keyNames.Add(KeyCode.Alpha2, "2");
        keyNames.Add(KeyCode.Alpha3, "3");
        keyNames.Add(KeyCode.Alpha4, "4");
        keyNames.Add(KeyCode.Alpha5, "5");
        keyNames.Add(KeyCode.Alpha6, "6");
        keyNames.Add(KeyCode.Alpha7, "7");
        keyNames.Add(KeyCode.Alpha8, "8");
        keyNames.Add(KeyCode.Alpha9, "9");
    }

    void DefaultBindingNames()
    {
        bindingNames = new Dictionary<KeyBindingNames, string>();
        bindingNames.Add(KeyBindingNames.Up, "Move Up");
        bindingNames.Add(KeyBindingNames.Right, "Move Right");
        bindingNames.Add(KeyBindingNames.Down, "Move Down");
        bindingNames.Add(KeyBindingNames.Left, "Move Left");
        bindingNames.Add(KeyBindingNames.Select, "Select");
        bindingNames.Add(KeyBindingNames.Cancel, "Cancel");

        bindingNames.Add(KeyBindingNames.SkillKey1, "Skill 1");
        bindingNames.Add(KeyBindingNames.SkillKey2, "Skill 2");
        bindingNames.Add(KeyBindingNames.SkillKey3, "Skill 3");
        bindingNames.Add(KeyBindingNames.SkillKey4, "Skill 4");
        bindingNames.Add(KeyBindingNames.SkillKey5, "Skill 5");
        bindingNames.Add(KeyBindingNames.SkillKey6, "Skill 6");
        bindingNames.Add(KeyBindingNames.SkillKey7, "Skill 7");
        bindingNames.Add(KeyBindingNames.SkillKey8, "Skill 8");
        bindingNames.Add(KeyBindingNames.SkillKey9, "Skill 9");
        bindingNames.Add(KeyBindingNames.SkillKey10, "Skill 10");

        bindingNames.Add(KeyBindingNames.InventoryKey1, "Inventory 1");
        bindingNames.Add(KeyBindingNames.InventoryKey2, "Inventory 2");
        bindingNames.Add(KeyBindingNames.InventoryKey3, "Inventory 3");
        bindingNames.Add(KeyBindingNames.InventoryKey4, "Inventory 4");
        bindingNames.Add(KeyBindingNames.InventoryKey5, "Inventory 5");

        bindingNames.Add(KeyBindingNames.HeadSlot, "Use Head Slot");
        bindingNames.Add(KeyBindingNames.WeaponSlot, "Use Weapon Slot");


        bindingNames.Add(KeyBindingNames.MovementHotKey, "Move");
        bindingNames.Add(KeyBindingNames.WaitHotKey, "Wait");

    }
}
