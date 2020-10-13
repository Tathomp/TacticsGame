using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FilePath
{
    /// Files
    /// 
    public static string ApplicationRoot = Application.dataPath;
    public static string SavedFolder = Application.dataPath + "/SavedGames/";
    public static string ContentFolder = Application.dataPath + "/Content/";
    
    public static string CampaignFolder = ContentFolder + "Campaigns/";
    public static string LibraryFolder = ContentFolder + "Libraries/";
        
    public static string DefaultSaveFile = SavedFolder + "default_save" + SaveExt;


    public static string CurrentSaveFilePath = "";

    /// Extensions 
    ///     
    public static string CampExt = ".cmp";
    public static string SaveExt = ".save";
    public static string ContLibExt = ".clib";

    /// Resources
    /// 
    public static string TilePrefab = "BaseObjects/Tile";

    ///Sprite Atlass
    ///
    public static string TileSetAtlas = "TileSprites";
    public static string ActorSpriteAtlas = "ActorSprites";
    public static string CampaignThumbnailAtlas = "CampaignThumbnails";
    public static string UISprite = "";

    public static string IconSpriteAtlas = "IconSprites";

    public static void CreateDefaultPaths()
    {
        Directory.CreateDirectory(Path.Combine(ApplicationRoot, ContentFolder));

        Directory.CreateDirectory(Path.Combine(ContentFolder, CampaignFolder));
        Directory.CreateDirectory(Path.Combine(ContentFolder, LibraryFolder));
        Directory.CreateDirectory(Path.Combine(ContentFolder, SavedFolder));

    }


}
