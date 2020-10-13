using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTypeDataContainer : MonoBehaviour
{
    public EditSkillPanel skillPanel;
    public ITileTypeDataPanel currentPane;

    //Prefabs for the target types

    //
    private ITargetable currentTargetable;

    public void UpdateDisplay(ITargetable targetable)
    {
        currentTargetable = targetable;



        gameObject.SetActive(true);

    }

    
}
