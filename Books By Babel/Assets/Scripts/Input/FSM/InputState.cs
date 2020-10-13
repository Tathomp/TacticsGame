﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputState  {

    public InputFSM inputFSM;
    public InputHandler inputHandler;


    public abstract void ProcessInput();
    public abstract void EnterState();
    public abstract void ExitState();
}
