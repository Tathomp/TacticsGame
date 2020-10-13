using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MusicManager
{
    public static BGMController musicInstance;



    public override void InitializePlayer()
    {
        if (musicInstance == null)
        {
            musicInstance = this;
            DontDestroyOnLoad(this);


        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
