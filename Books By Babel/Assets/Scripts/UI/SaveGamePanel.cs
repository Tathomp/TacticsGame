using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveGamePanel : MonoBehaviour
{
    //prefab
    public TextButton buttonPrefab;

    //display
    public TMP_Text text;
    public Transform ButtonContainer;

    //vars
    private string selectedSave;
    private List<TextButton> saveButtons = new List<TextButton>();


    public void InitSaveGameSelection()
    {
        gameObject.SetActive(true);

        DirectoryInfo dir = new DirectoryInfo(FilePath.SavedFolder);

        foreach (string item in Directory.GetFiles(FilePath.SavedFolder))
        {
            int i = item.LastIndexOf('/') + 1;
            string s = item.Substring(i);
            if (s.EndsWith(FilePath.SaveExt))
            {
                // Here we'll instantiate a button
                TextButton b = Instantiate<TextButton>(buttonPrefab, ButtonContainer);
                Campaign c = ((SavedFile)SaveLoadManager.LoadFile(FilePath.SavedFolder + s)).campaign;
                b.ChangeText(c.CampaignName);
                saveButtons.Add(b);
                b.button.onClick.AddListener(delegate { ButtonClicked(s, item); });
            }
        }

        if(saveButtons.Count > 0)
        {
            saveButtons[0].button.onClick.Invoke();
        }
    }


    private void ButtonClicked(string s, string fullFilePath)
    {
        selectedSave = s;
        text.text = s;

    }

    private void ClearButtons()
    {
        for (int i = saveButtons.Count - 1; i >= 0; i--)
        {
            saveButtons[i].button.onClick.RemoveAllListeners();
            GameObject.Destroy(saveButtons[i].gameObject);
            GameObject.Destroy(saveButtons[i]);
        }

        saveButtons = new List<TextButton>();
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ClearButtons();
    }


    public void StartSavedGame()
    {
      

        SavedFile f = (SavedFile)SaveLoadManager.LoadFile(FilePath.SavedFolder + selectedSave);       

        

        FilePath.CurrentSaveFilePath = FilePath.SavedFolder + selectedSave;




        Globals.campaign = f.campaign;

        f.SwitchScene();
    }


 
}
