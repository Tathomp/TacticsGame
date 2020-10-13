using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager
{  
    #region Generic Save Load Methods
    public static object LoadFile(string filepath)
    {


        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(filepath, FileMode.Open);
        

        object data = bf.Deserialize(file);


        file.Close();


        
        return data;

    }

    private static string SaveFile(string filepath, object data)
    {
    

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(filepath, FileMode.Create);

        bf.Serialize(stream, data);


        stream.Close();
        return filepath;
    }
    #endregion


    public static bool IsSlotUsed(string campaignName, string slot ="Autosave-")
    {
        return File.Exists(GenerateSaveStateFilePath(campaignName, slot));
    }

    //TODO
    //We should make this private at some point i think to force use of the save slot menu
    public static void SaveCampaignProgress(SavedFile file, string fileFpath)
    {
        SaveFile(fileFpath, file);
    }

    public static string AutoSaveCampaignProgress(SavedFile file)
    {
         
        return SaveFile( GenerateSaveStateFilePath(file.campaign.GetFileName()), file);
    }

    public SavedFile LoadAutoSaveProgress(string s)
    {
        return LoadFile( GenerateSaveStateFilePath(s)) as SavedFile; ///All this is untested and probably unused
    }
    //check this
    public static void Savecampaign(Campaign c)
    {
        SaveFile(GeneerateCampaignSaveFilePath(c.GetFileName()), c);
    }


    public static Campaign LoadCampaign(string filepath)
    {
        return LoadFile(FilePath.CampaignFolder + filepath) as Campaign;
    }
    

    public static string GenerateSaveStateFilePath(string campaignname, string slot = "Autosave-")
    {
        return FilePath.SavedFolder + slot + campaignname + FilePath.SaveExt;
    }

    public static string GeneerateCampaignSaveFilePath(string campaiignname)
    {
        return FilePath.CampaignFolder + campaiignname + FilePath.CampExt;
    }
}