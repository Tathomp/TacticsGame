using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceModel
{
    int levelCap;
    int[] xpThreasholds;

	public ExperienceModel()
    {
        levelCap = 3;
        xpThreasholds = new int[levelCap + 1];

        PopulateXPThresholds();
    }

    public bool LeveledUp(ActorData data)
    {
        if(data.Level == levelCap)
        {
            data.XP = xpThreasholds[levelCap];

            return false;
        }

        if(data.XP >= xpThreasholds[data.Level])
        {
            data.XP -= xpThreasholds[data.Level];
            return true;
        }
        else
        {
            return false;
        }


    }

    void PopulateXPThresholds()
    {
        xpThreasholds[0] = 0;
        xpThreasholds[1] = 100;
        xpThreasholds[2] = 225;
        xpThreasholds[3] = 500;
    }
}
