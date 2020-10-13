using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class ContentLibrary
{
    //the lib name should just be the name of the folder in the file system
    //that folder will have the individual content
    //
    // save files will contain a contentlibary class to point to the approiate content
    // for the file

    private readonly string libraryName;
    private readonly string lib_fp;

    //public TileDatabase currentTileDatabase;

    //public SavedDatabase<CutScene> cutsceneDatabase;
    //public SavedDatabase<Item> itemDB; check consturctor if re enabled
    //public MissionHandler missionHandler;
    //public SavedDatabase<MapDataModel> mapDB;
    //public SavedDatabase<MapEnchantment> MapEnchantmentsDB;


    public SavedDatabase<Skill> skillDatabase;
    //public SavedDatabase<TileEffect> effectDatabase;

    public SavedDatabase<ActorData> actorDB;

    public SavedDatabase<Buff> buffDatabase;
    public SavedDatabase<Discipline> disciplineDB;
    public SavedDatabase<Talent> TalentDB;


    //public SavedDatabase<Race> raceDB;
    //public SavedDatabase<Job> JobDB;
    //public SavedDatabase<Shop> ShopDB;

    public ContentLibrary(string libraryName)
    {
        this.libraryName = libraryName;
        this.lib_fp = libraryName + ".cont"; // move this to the filepath class

        actorDB = new SavedDatabase<ActorData>();
        skillDatabase = new SavedDatabase<Skill>();
        //effectDatabase = new SavedDatabase<TileEffect>();


        buffDatabase = new SavedDatabase<Buff>();

        //raceDB = new SavedDatabase<Race>();
        disciplineDB = new SavedDatabase<Discipline>();
        //JobDB = new SavedDatabase<Job>();
        TalentDB = new SavedDatabase<Talent>();


        // MapEnchantmentsDB = new SavedDatabase<MapEnchantment>();
        // mapDB = new SavedDatabase<MapDataModel>();
        // currentTileDatabase = new TileDatabase();
        // itemDB = new SavedDatabase<Item>();

        // tileEffectMap = new PropertyTagMap<int, TileEffect>();
        // skillEffectMap = new PropertyTagMap<float, ResistanceLevel>();
        // missionHandler = new MissionHandler();
        // ShopDB = new SavedDatabase<Shop>();
    }

    public void SaveFile()
    {
        //SaveLoadManager.SaveFile(FilePath.LibraryFolder + lib_fp, this);
    }

}
