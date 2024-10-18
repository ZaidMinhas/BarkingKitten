using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    private GuardState _currentState;
    
    void Start()
    {
        _currentState = new IdleGuardState(this);
        _currentState.Start();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.Update();
    }

    public void SwitchState(GuardState newState)
    {
        _currentState.End();
        _currentState = newState;
        _currentState.Start();
    }
}
