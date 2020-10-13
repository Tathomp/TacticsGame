using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUInputState : BoardInputState
{
    /// <summary>
    /// This could be replaced by the block input state
    /// </summary>
    /// <param name="boardManager"></param>
    /// 
    public CPUInputState(BoardManager boardManager) : base(boardManager)
    {

    }

    public override void EnterState()
    {
       // boardManager.CheckEventsAndCompletion();

    }

    public override void ExitState()
    {

    }

    public override void ProcessInput()
    {

    }
}
