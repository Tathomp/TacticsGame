using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGameSlotPanel : MonoBehaviour
{

    public enum SlotButtonType { SaveWorld, SaveBattle, Load }
    public SlotButtonType slotType;

    public ScrollListScaleableContent content;
    public SaveSlotButton prefab;


    public SaveSlotButton CreateButtn(string filePath, string label)
    {
        SaveSlotButton t = Instantiate<SaveSlotButton>(prefab, content.contentTransform);
        t.slotNumber = filePath;
        t.ChangeText(label);
        t.panel = this;
        content.AddToList(t);

        t.button.onClick.AddListener(delegate { SaveGame(filePath); });
        

        return t;
    }


    public void InitSlots()
    {
        gameObject.SetActive(true);
        content.CleanUp();
        
        if(slotType != SlotButtonType.Load)
        {
            CreateNewSaveButton();
        }

        foreach (string item in Directory.GetFiles(FilePath.SavedFolder))
        {
            int i = item.LastIndexOf('/') + 1;
            string s = item.Substring(i);

            if (s.EndsWith(FilePath.SaveExt))
            {
                 SaveSlotButton t = CreateButtn(item, s);
                
                // FilePath.SaveFolder + s should give us the absolute filepath to a save file
                // Maybe we'll just store that in a button


                //Here's where we can do load stuff or save stuff?
            }
            
        }

        content.AdjustContentLength();
    }


    public int GetLowetSaveSlotNumber(string currCampaign)
    {
        int slot = 0;
        List<int> used = new List<int>();

        // WE have to write a sortig algo 


        foreach (string item in Directory.GetFiles(FilePath.SavedFolder))
        {
            int i = item.LastIndexOf('/') + 1;
            string s = item.Substring(i);

            if (s.EndsWith(FilePath.SaveExt))
            {


                if(s.Contains(currCampaign))
                {
                    string j = s.Substring(0, s.IndexOf("-"));
                    //j should be the slot number now?
                    Debug.Log(j);
                    int potentialNewSLot;
                    if(int.TryParse(j, out potentialNewSLot))
                    {
                        used.Add(potentialNewSLot);

                    }



                }
                //Here's where we can do load stuff or save stuff?
            }

        }

        while(used.Contains(slot) == true)
        {
            slot++;
            if(slot == 100)
            {
                break;
            }
        }



        return slot;
    }


    public string BuildSavePath(string campaignName)
    {
        // we just need to build the failpath we want
        // how hard can that be

        return FilePath.SavedFolder + GetLowetSaveSlotNumber(campaignName) + "-" + campaignName + FilePath.SaveExt; //could move this method to the fliepath static
        
    }

    public void CreateNewSaveButton()
    {
        string fp = BuildSavePath(Globals.campaign.fileName);
        string label = fp; // we can change this later to be

        SaveSlotButton t = Instantiate<SaveSlotButton>(prefab,content.contentTransform);
        content.AddToList(t);
        t.ChangeText("-New Save-");

        t.button.onClick.AddListener(delegate { NewSave(); });

        //TODO actully save the file with the path
    }

    public void NewSave()
    {
        string fp = BuildSavePath(Globals.campaign.fileName);


        int i = fp.LastIndexOf('/') + 1;
        string s = fp.Substring(i);


        SaveSlotButton t = CreateButtn(BuildSavePath(Globals.campaign.fileName), s);
        t.button.onClick.Invoke();

    }

    public void SaveGame(string slotNumb)
    {
        SavedFile f = null; 


        if (slotType == SlotButtonType.SaveWorld)
        {
            f = new SaveStateWorldMap(Globals.campaign);
        }
        else if(slotType == SlotButtonType.SaveBattle)
        {
            f = Globals.GetBoardManager().SaveMission();

        }
        else
        {
            f = (SavedFile)SaveLoadManager.LoadFile(

            slotNumb

            );

            Globals.campaign = f.campaign;
            FilePath.CurrentSaveFilePath = slotNumb;
            f.SwitchScene();
            return;
        }

        SaveLoadManager.SaveCampaignProgress(f, slotNumb);
;   }
}
