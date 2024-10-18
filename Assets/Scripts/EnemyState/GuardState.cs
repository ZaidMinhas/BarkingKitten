using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuardState
{
    protected Guard guard;
    protected GuardState(Guard guard)
    {
        this.guard = guard;
    }
    
    public abstract void Start();
    public abstract void Update();
    public abstract void End();
}
