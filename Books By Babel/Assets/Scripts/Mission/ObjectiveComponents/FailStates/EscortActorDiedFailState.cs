using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortActorDiedFailState : AssassinateObjectiveComponent
{
    public EscortActorDiedFailState(string target) : base(target)
    {
    }

    public override string PrintProgress()
    {
        return "Protect: " + target;
    }
}
