using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIconPanel : MonoBehaviour
{
    /// We haven't tested anything
    ///
    ///
    public CharacterEditorPanel panel;

    public void TogglePartyMenu()
    {
        bool newstate = panel.gameObject.activeInHierarchy;

        if(newstate)
        {
            panel.ToggleOn();
        }
        else
        {
            panel.gameObject.SetActive(false);
        }
    }
}
