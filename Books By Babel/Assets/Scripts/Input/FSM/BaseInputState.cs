using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInputState : InputState {

    protected BaseManager baseManager;

    public BaseInputState(BaseManager baseManager)
    {
        this.baseManager = baseManager;
        inputFSM = baseManager.inputFSM;
    }
}
