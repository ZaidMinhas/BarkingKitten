using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private State _currentState;
    [SerializeField] private float speed;

    private void Start()
    {
        _currentState = new IdleState();

        _currentState.Start(this);
    }
    
    private void Update()
    {
        _currentState.Update(this);
    }

    public void Move(float direction)
    {
        transform.Translate(direction * speed * Time.deltaTime, 0, 0);
    }

    public void SwitchState(State state)
    {
        _currentState.End(this);
        _currentState = state;
        _currentState.Start(this);
        
    }
}
