using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class JobReq
{
    public abstract bool ReqMet(ActorData data);
    public abstract JobReq Copy();

}
