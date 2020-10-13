using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadPanel : MonoBehaviour
{
    public SaveGameSlotPanel savePanel, loadPanel;



    public void TurnOnLoadPanel()
    {
        
        savePanel.gameObject.SetActive(false);
        loadPanel.InitSlots();
    }

    public void TurnOnSavePanel()
    {
        loadPanel.gameObject.SetActive(false);
        savePanel.InitSlots();
    }
}
