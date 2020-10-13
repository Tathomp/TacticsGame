using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationInputState : PlayingAnimationInputState
{
    public AIAnimationInputState(BoardManager boardManager, Combat combat) 
        : base(boardManager, combat)
    {

    }
}
