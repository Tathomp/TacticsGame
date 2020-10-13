using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MusicManager
{
    public static SFXController sfxInstance;


    public override void InitializePlayer()
    {
        if (sfxInstance == null)
        {
            sfxInstance = this;
            DontDestroyOnLoad(this);


        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
