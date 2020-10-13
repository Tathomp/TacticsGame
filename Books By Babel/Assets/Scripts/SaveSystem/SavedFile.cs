using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SavedFile
{
    protected string fileName;
    

    public Campaign campaign;

    public SavedFile(Campaign campaign)
    {
        this.fileName = campaign.GetFileName();
        this.campaign = campaign;
    }

    public abstract void SwitchScene();
        
    
}
