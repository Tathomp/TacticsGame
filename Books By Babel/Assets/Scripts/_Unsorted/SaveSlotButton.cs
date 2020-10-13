using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotButton : TextButton
{
    public SaveGameSlotPanel panel;
    public string slotNumber;

    public void SaveOrLoad()
    {
        panel.SaveGame(slotNumber);
    }
}
