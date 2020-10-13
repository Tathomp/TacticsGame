using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class HotKeys
{
    public Dictionary<KeyBindingNames, KeyCode> hotkeys;
	
    public HotKeys()
    {
        hotkeys = new Dictionary<KeyBindingNames, KeyCode>();
    }


    public void GenerateDefaultKeys()
    {
        Debug.Log("DEFAULT KEYS GENERATED");

        hotkeys = new Dictionary<KeyBindingNames, KeyCode>
        {

            { KeyBindingNames.Up, KeyCode.UpArrow },
            { KeyBindingNames.Right, KeyCode.RightArrow },
            { KeyBindingNames.Down, KeyCode.DownArrow },
            { KeyBindingNames.Left, KeyCode.LeftArrow },
            { KeyBindingNames.Select, KeyCode.Return },
            { KeyBindingNames.Cancel, KeyCode.Escape },


            { KeyBindingNames.SkillKey1, KeyCode.Alpha1 },
            { KeyBindingNames.SkillKey2, KeyCode.Alpha2 },
            { KeyBindingNames.SkillKey3, KeyCode.Alpha3 },
            { KeyBindingNames.SkillKey4, KeyCode.Alpha4 },
            { KeyBindingNames.SkillKey5, KeyCode.Alpha5 },
            { KeyBindingNames.SkillKey6, KeyCode.Alpha6 },
            { KeyBindingNames.SkillKey7, KeyCode.Alpha7 },
            { KeyBindingNames.SkillKey8, KeyCode.Alpha8 },
            { KeyBindingNames.SkillKey9, KeyCode.Alpha9 },
            { KeyBindingNames.SkillKey10, KeyCode.Alpha0 },


            { KeyBindingNames.InventoryKey1, KeyCode.Q },
            { KeyBindingNames.InventoryKey2, KeyCode.W },
            { KeyBindingNames.InventoryKey3, KeyCode.E },
            { KeyBindingNames.InventoryKey4, KeyCode.R },
            { KeyBindingNames.InventoryKey5, KeyCode.T },


            { KeyBindingNames.HeadSlot, KeyCode.A },
            { KeyBindingNames.WeaponSlot, KeyCode.S },

            { KeyBindingNames.PlaceUnit, KeyCode.A },
            { KeyBindingNames.RemoveUnit, KeyCode.D },

            { KeyBindingNames.MovementHotKey, KeyCode.M },
            { KeyBindingNames.WaitHotKey, KeyCode.T },



            { KeyBindingNames.CycleLeft, KeyCode.Q },
            { KeyBindingNames.CycleRight, KeyCode.E },

        };
    }

    public KeyBindingNames[] GetKeys()
    {
        return hotkeys.Keys.ToArray();
    }
}
