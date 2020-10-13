using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Reward
{
    public abstract void DistributeReward(BoardManager bm);

    public abstract string RewardString();

    public abstract Reward Copy();

}
