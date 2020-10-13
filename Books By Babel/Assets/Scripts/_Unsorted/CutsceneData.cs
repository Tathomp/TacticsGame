using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneData
{
    public SavedFile currentFIle;
    public CutScene currentScene;
    public bool populatechanges; //lets the player set flags, make decisions etc

    
    public CutsceneData(CutScene cs, SavedFile file, bool populatechanges)
    {
        this.currentFIle = file;
        this.currentScene = cs;
        this.populatechanges = populatechanges;
    }
}
