using System.Collections;
using System.Collections.Generic;
using KittenState;
using UnityEngine;

public class BarkingKitten : MonoBehaviour
{

    public Player player;
    CatState _currentState;
    void Start()
    {
        _currentState = new IdleCatState();
        _currentState.Start(this);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.Update(this);
    }

    public void SwitchState(CatState newState)
    {
        _currentState.End(this);
        _currentState = newState;
        _currentState.Start(this);
    }
    
    
}
