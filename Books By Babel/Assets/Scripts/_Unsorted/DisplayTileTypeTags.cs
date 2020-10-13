using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTileTypeTags : MonoBehaviour
{
    private List<TileTageDataObject> tag_buttons = new List<TileTageDataObject>();
    private TextButton newtag_button_instance;
    public ScrollListScaleableContent panel;
    private EditTileTypesPanel editPanel;
    // Editor
    public TextButton button_prefab;
    public TileTageDataObject tiletag_prefab;
    

    public void InitDisplay(EditTileTypesPanel editTileTypesPanel)
    {
        gameObject.SetActive(true);

        this.editPanel = editTileTypesPanel;

        ClearButtons();


        foreach (string ket in editPanel.GetCurrentTileType().attributes)
        {
            TileTageDataObject obj = NewTileTageDataObject(ket);
            tag_buttons.Add(obj);
        }

        PrintNewTileButton();
    }

    public void PrintNewTileButton()
    {
        if(newtag_button_instance != null)
        {
            Destroy(newtag_button_instance.gameObject);
        }

        newtag_button_instance = Instantiate<TextButton>(button_prefab, panel.contentTransform);
        newtag_button_instance.ChangeText("Add Property Tag");
        newtag_button_instance.button.onClick.AddListener(delegate { OnNewTagClicked(); });
    }

    public void OnNewTagClicked()
    {
        // delete new button
        if (newtag_button_instance != null)
            Destroy(newtag_button_instance.gameObject);

        //create tew tile tag objects
        TileTageDataObject obj = NewTileTageDataObject("");
        tag_buttons.Add(obj);

        // create new button
        PrintNewTileButton();
    }


    public TileTageDataObject NewTileTageDataObject(string currentSelectedTile)
    {
        TileTageDataObject t = Instantiate<TileTageDataObject>(tiletag_prefab, panel.contentTransform);
        t.InitTagObj(currentSelectedTile, editPanel);

        t.dropdown.ClearOptions();

        int x = 0;
        // add all options here
        // select the approriate option


        for (int i = 0; i < editPanel.creationManager.currentCampaign.properties.Count; i++)
        {
            t.dropdown.options.Add(
                new TMPro.TMP_Dropdown.OptionData(editPanel.creationManager.currentCampaign.properties[i]));
        }

        t.dropdown.value = 0;
        

        TMP_Dropdown.OptionData[] temp =  t.dropdown.options.ToArray();

        //for

        t.dropdown.RefreshShownValue();

        return t;
    }

    public void ClearButtons()
    {
        int x = tag_buttons.Count - 1;

        for (int i = x; i >= 0; i--)
        {
            Destroy(tag_buttons[i].gameObject);
        }

        tag_buttons = new List<TileTageDataObject>();
    }

    public void SaveTagData()
    {
        foreach (TileTageDataObject item in tag_buttons)
        {
            item.Save();
        }
    }
    
    public void DestoryBUtton(TileTageDataObject obj)
    {
        tag_buttons.Remove(obj);
        
        Destroy(obj.gameObject);
    }
}
